using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fence : Damagable
{
    public GameObject hp_barObj;
    public Slider hp_bar;
    public GameObject hp_pos;
    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        startingHealth = 70f;
        health = 70f;
        onDeath += () => gameObject.SetActive(false);
        onDeath += () => hp_bar.gameObject.SetActive(false);
        hp_bar = Instantiate(hp_barObj, GameObject.Find("enemy-canvas").transform).GetComponent<Slider>();
        camera = GameObject.Find("Camera").GetComponent<Camera>();
        hp_bar.gameObject.SetActive(false);
    }
    private void Update()
    {
        hp_bar.value = health / startingHealth;
        Vector3 pos = camera.WorldToScreenPoint(hp_pos.transform.position);
        hp_bar.transform.position = new Vector3(pos.x, pos.y, pos.z);
    }

    public void restart()
    {
        gameObject.SetActive(true);
    }

    public override void OnDamage(float damage, Transform trans)
    {
        base.OnDamage(damage, transform);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("sight"))
        {
            hp_bar.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("sight"))
        {
            hp_bar.gameObject.SetActive(false);
        }
    }
}
