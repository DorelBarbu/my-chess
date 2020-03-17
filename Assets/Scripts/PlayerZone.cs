using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerZone : MonoBehaviour
{
    [SerializeField] TextMeshPro playerStatus;
    [SerializeField] ColorsEnum playerColor;
    [SerializeField] GameManager gameManager;
    TextMeshPro PlayerStatus
    {
        get { return playerStatus; }
        set { playerStatus = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        PieceDraggableHandler.EndTurnEvent += HandleEndTurnEvent;
        DetermineIfFirstToMove();
    }

    void HandleEndTurnEvent(ColorsEnum colorAtMove, bool isCheck)
    {
        if(colorAtMove != playerColor)
        {
            if(isCheck == true)
            {
                playerStatus.text = PlayerStatusConstants.IN_CHECK;
                playerStatus.color = Color.red;
            }
            else
            {
                playerStatus.text = PlayerStatusConstants.THINKING;
            }
        }
        else
        {
            playerStatus.text = PlayerStatusConstants.NO_STATUS;
        }
    }

    void DetermineIfFirstToMove()
    {
        if(FindObjectOfType<GameManager>().AtMove == playerColor)
        {
            playerStatus.text = PlayerStatusConstants.THINKING;
        }
    }
}
