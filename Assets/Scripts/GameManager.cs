using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] ColorsEnum atMove;
    public ColorsEnum AtMove
    {
        get { return atMove; }
        set { atMove = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game started");
        PieceDraggableHandler.EndTurnEvent += HandleEndTurnEvent;
    }

    void ToggleAtMove()
    {
        if(AtMove == ColorsEnum.BLACK)
        {
            AtMove = ColorsEnum.WHITE;
        }
        else
        {
            AtMove = ColorsEnum.BLACK;
        }
    }

    void HandleEndTurnEvent(ColorsEnum colorAtMove)
    {
        ToggleDraggableForPieces();
        ToggleAtMove();
    }

    void ToggleDraggableForPieces()
    {
        Piece[] pieces = FindObjectsOfType<Piece>();
        foreach(Piece piece in pieces)
        {
            piece.GetComponent<Draggable>().DraggingEnabled = !piece.GetComponent<Draggable>().DraggingEnabled;
        }
    }
}
