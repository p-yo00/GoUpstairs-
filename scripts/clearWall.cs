using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearWall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("sight"))
        {
            transform.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("sight"))
        {
            transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
