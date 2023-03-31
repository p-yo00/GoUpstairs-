using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackButton : MonoBehaviour, IPointerDownHandler
{
    public Shooter shooter;
    public float coolTime=2;
    private bool isCool = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        attack();
    }

    public void attack()
    {
        if (!isCool)
        {
            shooter.Shoot();
            isCool = true;
            StartCoroutine("waitCool");
        }
    }

    IEnumerator waitCool()
    {
        yield return new WaitForSeconds(coolTime);
        isCool = false;
    }
}
