using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorEffect : MonoBehaviour
{
    private Vector3 originPos;
    private float amount = 0.5f;
    private float shake_time = 3f;
    void Start()
    {
        originPos = transform.localPosition;
    }

    public void shakeElevator()
    {
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        print("shake");
        float start_time = Time.time;
        while (Time.time - start_time < shake_time)
        {
            transform.localPosition = new Vector3(amount, originPos.y, originPos.z);
            yield return new WaitForSeconds(0.2f);
            transform.localPosition = originPos;
            amount *= -1;
        }
    }
}
