using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subCamera : MonoBehaviour
{
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = camera.transform.position;
        transform.rotation = camera.transform.rotation;
    }
}
