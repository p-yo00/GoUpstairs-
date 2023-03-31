using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSkill : MonoBehaviour
{
    public Player player;
    public OverZone overzone;
    private EnemySpawner enemySpawner;

    public void use(int id)
    {
        print("id=" + id);
        switch (id)
        {
            case 1:
                powerUp();
                break;
            case 2:
                timeDown();
                break;
            case 3:
                healthUp();
                break;
            case 4:
                healUp();
                break;
        }
    }

    private void powerUp()
    {
        player.power += 10;
    }

    private void healthUp()
    {
        player.startingHealth += 10;
    }

    private void timeDown()
    {
        enemySpawner = gameObject.GetComponent<EnemySpawner>();
        if(enemySpawner.limitTime>=10f)
            enemySpawner.limitTime -= 10f;
    }

    private void healUp()
    {
        overzone.heal += 1;
    }
}
