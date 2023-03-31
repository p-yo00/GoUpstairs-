using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Animator weaponAnimator;
    public Collider attackRange;
    private AudioSource audio;
    public AudioClip attackClip;

    void Start()
    {
        weaponAnimator.speed = 3f;
        audio = transform.GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        weaponAnimator.SetTrigger("attack");
        StartCoroutine("waitAnimation");
    }

    IEnumerator waitAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        attack();
    }
    IEnumerator idle()
    {
        yield return new WaitForSeconds(2f);
        
        attackRange.enabled = false;
    }

    void attack()
    {
        audio.PlayOneShot(attackClip);
        attackRange.enabled = true;
        StartCoroutine("idle");
    }
}
