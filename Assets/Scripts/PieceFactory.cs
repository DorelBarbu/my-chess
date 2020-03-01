﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceFactory
{
    public static IPiece createInstance(PieceControllerType pieceType)
    {
        IPiece piece;
        switch(pieceType)
        {
            case PieceControllerType.QUEEN:
                piece = new Queen();
                break;
            default:
                piece = null;
                break;
        }
        return piece;
    }
}