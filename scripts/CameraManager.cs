using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    CharacterController character;
    public Animator animator;
    float speed = 10f;
    private bool openState;
    bool doorLock;

    void Start()
    {
        character = GetComponent<CharacterController>();
        openState = true;
        doorLock = false ; //true
        animator.SetTrigger("open");
    }

    // Update is called once per frame
    void Update()
    {
        move();

    }
    private void move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, 0, moveZ);

        character.Move(transform.TransformDirection(move) * Time.deltaTime * speed);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("door"))
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                if(!doorLock)
                {
                    if (openState) { print("close");  animator.SetTrigger("close"); }
                    else { print("open"); animator.SetTrigger("open"); }
                    openState = !openState;
                    print(openState);
                    doorLock = true;
                }
                
            }
        }
        
    }
}
