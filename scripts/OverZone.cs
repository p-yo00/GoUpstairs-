using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverZone : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    public GameObject gameoverText;
    public GameObject moveUI;
    public GameObject attackUI;
    public GameObject[] Fences;
    public GameObject playerObj;
    private int wave;
    private Player player;
    private bool isCool;
    public float heal;

    private void Start()
    {
        player = playerObj.GetComponent<Player>();
        isCool = false;
        heal = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            moveUI.SetActive(false);
            attackUI.SetActive(false);
            wave = enemySpawner.wave;
            enemySpawner.isClear = true;
            enemySpawner.resetEnemy();
            GameOver();
        }
    }

    public void GameOver()
    {
        gameoverText.SetActive(true);
        gameoverText.transform.Find("gameOverText").GetComponent<Text>().text = "Game Over";
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isCool)
            {
                StartCoroutine("waitRestore");
            }
        }
    }

    IEnumerator waitRestore()
    {
        isCool = true;
        yield return new WaitForSeconds(1);
        player.restore(heal);
        isCool = false;
    }


    public void clickRestart()
    {
        moveUI.SetActive(true);
        attackUI.SetActive(true);
        gameoverText.SetActive(false);
        enemySpawner.restart(enemySpawner.wave);
        player.health = player.startingHealth;

        for (int i = 0; i < Fences.Length; i++)
        {
            Fences[i].SetActive(true);
        }
    }

}
