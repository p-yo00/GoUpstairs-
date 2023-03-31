using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    // ���� ����
    public List<Enemy> enemyPrefab;
    public Transform[] spawnPoints;
    public Text floorText;
    public Text timerText;
    public GameObject gameClear;
    public Text gameClearText;
    public GameObject[] Fences;

    private List<Enemy> enemies = new List<Enemy>(); // ������ ������ ��� ����Ʈ
    public int wave; // ���� ���̺�

    public float limitTime = 1 * 6f; //1�� 
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
        // �ð��� ���� ��� ���� ���� ����
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
    { //������������ ������ ���� ����
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

    // ���� �����ϰ� ������ ������ ������ ����� �Ҵ�
    private void CreateEnemy()
    {
        // ������ ��ġ�� �������� ����
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // �� ���������κ��� �� ����
        Enemy enemy = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Count)], spawnPoint.position, spawnPoint.rotation);
        enemy.setHealth(initHealth);
        // ������ ���� ����Ʈ�� �߰�
        enemies.Add(enemy);

        // ���� onDeath �̺�Ʈ�� �͸� �޼��� ���
        // ����� ���� ����Ʈ���� ����
        enemy.onDeath += () => enemies.Remove(enemy);
        // ����� ���� 3 �� �ڿ� �ı�
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
