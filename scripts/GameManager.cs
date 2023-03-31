using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    public AudioClip eleRunClip;
    public AudioClip eleStopClip;
    public AudioClip eleOpenClip;
    public bool openState;
    public bool doorLock;
    private EnemySpawner enemySpawner;
    public Text gameText;
    public ElevatorEffect elevator;
    public GameObject skillObject;

    private AudioSource bgmAudio;
    public List<AudioClip> bgmList;

    void Start()
    {
        enemySpawner = transform.GetComponent<EnemySpawner>();
        openState = true;
        doorLock = true;
        audioSource = elevator.gameObject.GetComponent<AudioSource>();
        bgmAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        //시작 시나리오 (1 floor 전)
        wave1();
    }

    public void nextFloor(int wave)
    {
        gameText.text = "엘리베이터로 돌아가 문을 닫아주세요.";
        gameText.gameObject.SetActive(true);
        StartCoroutine("waitText");
        doorLock = false;

        bgmAudio.Stop();
        //bgmAudio.clip = bgmList[0];
        //bgmAudio.Play();

        StartCoroutine(waitCloseDoor(wave));
    }

    IEnumerator waitText()
    {
        yield return new WaitForSeconds(3);
        gameText.gameObject.SetActive(false);
    }

    private void wave1()
    {
        openElevator();
        bgmAudio.clip = bgmList[0];
        bgmAudio.Play();
    }
    private void wave2()
    {

    }
    private void wave3()
    {
        enemySpawner.initHealth = 40f;
    }
    private void wave4()
    {

    }
    private void wave5()
    {
        enemySpawner.initHealth = 50f;
    }

    public void openElevator()
    {
        animator.SetTrigger("open");
        audioSource.PlayOneShot(eleOpenClip);
    }
    public void closeElevator()
    {
        animator.SetTrigger("close");
        audioSource.PlayOneShot(eleOpenClip);
    }

    IEnumerator waitCloseDoor(int wave)
    {
        if (openState)
        {
            yield return null;
            StartCoroutine("waitCloseDoor", wave);
        }
        else
        {
            selectSkill();
            switch (wave)
            {
                case 2:
                    wave2();
                    break;
                case 3:
                    wave3();
                    break;
                case 4:
                    wave4();
                    break;
                case 5:
                    wave5();
                    break;
            }
            yield return new WaitForSeconds(1f);
            doorLock = true;
            elevator.shakeElevator();
            audioSource.clip = eleRunClip;
            audioSource.Play();
            yield return new WaitForSeconds(3f);
            audioSource.Stop();
            audioSource.PlayOneShot(eleStopClip);
            doorLock = false;
            StartCoroutine(waitOpenDoor(wave));
        }
        
    }
    IEnumerator waitOpenDoor(int wave)
    {
        if (!openState)
        {
            yield return null;
            StartCoroutine("waitOpenDoor", wave);
        }
        else
        {
            doorLock = true;
            skillObject.SetActive(false);
            enemySpawner.setNextStage();
            playBGM(wave-1);
        }
        
    }
    private void playBGM(int wave)
    {
        bgmAudio.clip = bgmList[wave];
        bgmAudio.Play();
    }

    public void bgmOff()
    {
        bgmAudio.Stop();
    }

    private void selectSkill()
    {
        skillObject.SetActive(true);
        skillObject.GetComponent<skillcardCreator>().createCard();
    }

}
