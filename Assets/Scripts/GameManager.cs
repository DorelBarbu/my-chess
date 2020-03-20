using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] ColorsEnum atMove;
    [SerializeField] List<Piece> blackPieces;
    [SerializeField] List<Piece> whitePieces;
    public ColorsEnum AtMove
    {
        get { return atMove; }
        set { atMove = value; }
    }

    void Start()
    {
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

    void HandleEndTurnEvent(ColorsEnum colorAtMove, bool isCheck)
    {
        Utils.ToggleDraggableForPieces(FindObjectsOfType<Piece>());
        ToggleAtMove();
        // HandlePossibleChess(colorAtMove);
    }

    void ToggleDraggableForPieces()
    {
        Piece[] pieces = FindObjectsOfType<Piece>();
        foreach(Piece piece in pieces)
        {
            piece.GetComponent<Draggable>().DraggingEnabled = !piece.GetComponent<Draggable>().DraggingEnabled;
        }
    }

    public List<Piece> getPiecesOfColor(ColorsEnum color)
    {
        return color == ColorsEnum.BLACK ? blackPieces : whitePieces;
    }

    public void AddPiece(Piece piece)
    {
        if(piece.GetColor() == ColorsEnum.BLACK)
        {
            blackPieces.Add(piece);
        }
        else
        {
            whitePieces.Add(piece);
        }
    }
}
