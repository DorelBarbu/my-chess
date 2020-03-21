using System.Collections;
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
            case PieceControllerType.KING:
                piece = new King();
                break;
            case PieceControllerType.ROOK:
                piece = new Rook();
                break;
            case PieceControllerType.KNIGHT:
                piece = new Knight();
                break;
            case PieceControllerType.BISHOP:
                piece = new Bishop();
                break;
            default:
                piece = null;
                break;
        }
        return piece;
    }
}
