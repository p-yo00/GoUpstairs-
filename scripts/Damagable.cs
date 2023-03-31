using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Damagable : MonoBehaviour
{
    public float startingHealth; // ���� ü��
    public float health { get; set; } // ���� ü��
    public bool dead { get; protected set; } // ��� ����
    public event Action onDeath; // ����� �ߵ��� �̺�Ʈ
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

        //print("ü��" + health);
        if(health<=0)
        {
            print("���" + health);
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
