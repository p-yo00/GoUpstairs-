using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Cinemachine;

public class Enemy : Damagable
{

    private NavMeshAgent pathFinder;
    private Transform destination;
    private Collider attackRange;
    private Color color;
    private Player player;
    private Fence fence_left;
    private Fence fence_right;
    private Fence fence_front;
    float attackSpeed = 2f;
    bool isCool = false;
    private bool aimToPlayer = false;

    public GameObject hp_barObj;
    public Slider hp_bar;
    public GameObject hp_pos;
    private Camera camera;
    

    private AudioSource audioSource;
    public AudioClip attackClip;

    // Start is called before the first frame update
    void Start()
    {
        startingHealth = 30f;
        health = 30f;
        power = 10f;
        destination = GameObject.Find("destination").transform;
        GameObject attackRangeObj = GameObject.Find("attackRange");
        attackRange = attackRangeObj.GetComponent<Collider>();
        GameObject playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<Player>();
        GameObject fence_left_obj = GameObject.Find("fence").transform.Find("fence-left").gameObject;
        GameObject fence_right_obj = GameObject.Find("fence").transform.Find("fence-right").gameObject;
        GameObject fence_front_obj = GameObject.Find("fence").transform.Find("fence-front").gameObject;
        fence_left = fence_left_obj.GetComponent<Fence>();
        fence_right = fence_right_obj.GetComponent<Fence>();
        fence_front = fence_front_obj.GetComponent<Fence>();
        audioSource = transform.GetComponent<AudioSource>();

        hp_bar = Instantiate(hp_barObj, GameObject.Find("enemy-canvas").transform).GetComponent<Slider>();
        //hp_bar = hp_barObj.GetComponent<Slider>();
        //ColorUtility.TryParseHtmlString("#A3E74D", out color);
        camera = GameObject.Find("Camera").GetComponent<Camera>();
        pathFinder = GetComponent<NavMeshAgent>();
        hp_bar.gameObject.SetActive(false);

        
    }
    void Update()
    {
        hp_bar.value = health / startingHealth;
        Vector3 pos = camera.WorldToScreenPoint(hp_pos.transform.position);
        hp_bar.transform.position = new Vector3(pos.x, pos.y, 0);

        if (aimToPlayer)
        {
            destination = player.transform;
        }

        Vector3 destVec = new Vector3(
            destination.position.x, -7, destination.position.z);
        if (pathFinder.enabled)
        {
            pathFinder.SetDestination(destVec);
        }
            
    }

    public void setHealth(float health)
    {
        startingHealth = this.health = health;
    }

    public override void OnDamage(float damage, Transform transform)
    {
        pathFinder.enabled = false;
        //GetComponent<MeshRenderer>().materials[0].color = Color.red;
        base.OnDamage(damage, transform);
        StartCoroutine("colorReset");
    }

    IEnumerator colorReset()
    {
        yield return new WaitForSeconds(2.0f);
        pathFinder.enabled = true;
        //GetComponent<MeshRenderer>().materials[0].color = color;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("attackRange"))
        {
            OnDamage(player.power, transform);
            attackRange.enabled = false;
        }
        if (other.CompareTag("sight"))
        {
            aimToPlayer = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Fence-f") && !isCool)
        {
            print(fence_front.health);
            StartCoroutine("waitAttack_fence", fence_front);
        }
        if (other.CompareTag("Fence-l") && !isCool)
        {
            print(fence_left.health);
            StartCoroutine("waitAttack_fence", fence_left);
        }
        if (other.CompareTag("Fence-r") && !isCool)
        {
            print(fence_right.health);
            StartCoroutine("waitAttack_fence", fence_right);
        }
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
            aimToPlayer = false;
            destination = GameObject.Find("destination").transform;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Player") && !isCool)
        {
            StartCoroutine("waitAttack");
        }
    }

    IEnumerator waitAttack()
    {
        isCool = true;
        audioSource.PlayOneShot(attackClip);
        player.OnDamage(power, null);
        yield return new WaitForSeconds(attackSpeed);
        isCool = false;
    }
    IEnumerator waitAttack_fence(Fence fence)
    {
        isCool = true;
        audioSource.PlayOneShot(attackClip);
        fence.OnDamage(power, transform);
        yield return new WaitForSeconds(attackSpeed);
        isCool = false;
    }
}
