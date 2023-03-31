using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class openFenceButton : MonoBehaviour, IPointerDownHandler
{
    public Transform fence;
    public AudioClip openDoorSound;
    private AudioSource fenceAudio;
    bool isOpen = false;


    public void OnPointerDown(PointerEventData eventData)
    {
        openFence();

    }

    public void openFence()
    {
        if (isOpen)
        {
            fence.rotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            fence.rotation = Quaternion.Euler(0, 180, 0);
        }
        fenceAudio.PlayOneShot(openDoorSound);
        isOpen = !isOpen;
    }

    void Start()
    {
        fenceAudio = fence.gameObject.GetComponent<AudioSource>();

    }
}
