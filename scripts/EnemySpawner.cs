using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    // 현재 게임
    public List<Enemy> enemyPrefab;
    public Transform[] spawnPoints;
    public Text floorText;
    public Text timerText;
    public GameObject gameClear;
    public Text gameClearText;
    public GameObject[] Fences;

    private List<Enemy> enemies = new List<Enemy>(); // 생성된 적들을 담는 리스트
    public int wave; // 현재 웨이브

    public float limitTime = 1 * 6f; //1분 
    private float startTime;
    private GameManager gameManager;
    public bool isClear;
    public float initHealth = 30f;

    private void Start()
    {
        wave = 1;
        floorText.text = "1F";
        StartCoroutine("RepeatCreate");
        gameManager = GetComponent<GameManager>();
        isClear = false;
    }

    private void Update()
    {
        // 시간이 끝난 경우 다음 스폰 실행
        if (!isClear && (limitTime <= (Time.time - startTime)))
        {
            isClear = true;
            resetEnemy();
            if (wave == 5)
            {
                gameClear.SetActive(true);
                gameClearText.text = "Game Clear";
                wave = 1;
                gameManager.bgmOff();
            }
            else
            {
                wave++;
                gameManager.nextFloor(wave);
            }
            
        }
        if (!isClear)
        {
            setTimer();
        }
    }

    public void setNextStage()
    { //다음스테이지 시작을 위해 실행
        floorText.text = wave + "F";
        startTime = Time.time;
        isClear = false;
        for (int i = 0; i < Fences.Length; i++)
        {
            Fences[i].SetActive(true);
        }
        StartCoroutine("RepeatCreate");
    }

    private void setTimer()
    {
        float remainTime = limitTime - (Time.time - startTime);
        int minute = (int)(remainTime / 60);
        int second = (int)(remainTime % 60);
        timerText.text = minute + " : " + second;
    }


    IEnumerator RepeatCreate()
    {
        if ((limitTime > (Time.time - startTime))&&!isClear)
        {
            CreateEnemy();
            yield return new WaitForSeconds(10f/wave+5);
            if (!isClear)
            {
                StartCoroutine("RepeatCreate");
            }
        }
    }

    // 적을 생성하고 생성한 적에게 추적할 대상을 할당
    private void CreateEnemy()
    {
        // 생성할 위치를 랜덤으로 결정
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // 적 프리팹으로부터 적 생성
        Enemy enemy = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Count)], spawnPoint.position, spawnPoint.rotation);
        enemy.setHealth(initHealth);
        // 생성된 적을 리스트에 추가
        enemies.Add(enemy);

        // 적의 onDeath 이벤트에 익명 메서드 등록
        // 사망한 적을 리스트에서 제거
        enemy.onDeath += () => enemies.Remove(enemy);
        // 사망한 적을 3 초 뒤에 파괴
        enemy.onDeath += () => Destroy(enemy.hp_bar.gameObject, 3f);
        enemy.onDeath += () => Destroy(enemy.gameObject, 3f);
    }

    public void resetEnemy()
    {
        isClear = true;
        for (int i = 0; i < enemies.Count; i++)
        {
            Destroy(enemies[i].hp_bar.gameObject);
            Destroy(enemies[i].gameObject);
        }
        enemies.Clear();
    }

    public void restart(int wave)
    {
        this.wave = wave ;
        floorText.text = wave + "F";
        startTime = Time.time;
        isClear = false;
        StartCoroutine("RepeatCreate");


    }
}
