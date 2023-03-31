using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Damagable
{
    public Collider attackRange;
    public Text health_bar;
    public Slider health_slider;
    public OverZone gameover;

    void Start()
    {
        attackRange.enabled = false;
        startingHealth = health = 100;
        power = 10;
        onDeath +=() => gameover.GameOver();
        
    }

    void Update()
    {
        health_bar.text = health.ToString()+"/"+startingHealth.ToString();
        health_slider.value = health / startingHealth;
    }
    public override void OnDamage(float damage, Transform trans)
    {
        base.OnDamage(damage, transform);
    }

    public void restore(float health)
    {
        if (this.health < startingHealth)
        {
            this.health += health;
            print(this.health);
        }

    }


}
