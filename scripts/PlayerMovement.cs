using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float speed = 10f;
    float mouseX = 0;
    float mouseY = 0;
    public GameObject camera;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * speed;
        mouseY += Input.GetAxis("Mouse Y") * speed;
        mouseY = Mathf.Clamp(mouseY, -55.0f, 55.0f);

        camera.transform.eulerAngles = new Vector3(0, mouseX, 0);
        transform.eulerAngles = new Vector3(-mouseY, mouseX, 0);
        //transform.localEulerAngles = new Vector3(-mouseY, 0, 0);
    }
}
