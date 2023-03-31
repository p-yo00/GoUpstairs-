using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Damagable : MonoBehaviour
{
    public float startingHealth; // 시작 체력
    public float health { get; set; } // 현재 체력
    public bool dead { get; protected set; } // 사망 상태
    public event Action onDeath; // 사망시 발동할 이벤트
    public float power=10f;

    private void OnEnable()
    {
        health = startingHealth=100f;
    }

    private void Start()
    {
        
    }

    public virtual void OnDamage(float damage, Transform transform)
    {
        health -= damage;

        //print("체력" + health);
        if(health<=0)
        {
            print("사망" + health);
            Die();
        }
    }

    public virtual void Die()
    {
        if (onDeath != null)
        {
            onDeath();
        }
    }
}
