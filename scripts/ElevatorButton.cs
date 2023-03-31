using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ElevatorButton : MonoBehaviour, IPointerDownHandler
{
    public GameManager gameManager;
    public void OnPointerDown(PointerEventData eventData)
    {
        openElevator();
    }

    public void openElevator()
    {
        if (!gameManager.doorLock)
        {
            if (gameManager.openState)
                gameManager.closeElevator();
            else
                gameManager.openElevator();
            gameManager.openState = !gameManager.openState;
            print(gameManager.openState);
        }
    }
}
