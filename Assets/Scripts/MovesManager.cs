using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesManager
{
    private static MovesManager movesManagerInstance;

    public static MovesManager Instance
    {
        get
        {
            if(movesManagerInstance == null)
            {
                movesManagerInstance = new MovesManager();
            }
            return movesManagerInstance;
        }
    }

    public List<string> GetNextPossiblePositionsForPieceAtSquare(string piecePosition)
    {
        SquareConfiguration squareConfiguration = BoardConfiguration.Instance.GetPieceAtSquare(piecePosition);
        List<Vector2> allowedMovesDeltas = MovesList.Instance.AllowedMovesIndexes[squareConfiguration.Piece];
        Vector2 origin = Utils.ConvertToCartesian(piecePosition[1], piecePosition[0]);
        List<string> nextPossiblePositions = new List<string>();

        foreach(Vector2 delta in allowedMovesDeltas)
        {
            Vector2 nextPosition = origin + delta;
            if(Utils.IsInsideBoard((int)nextPosition.x, (int)nextPosition.y))
            {
                string nextSquare = Utils.ConverToAlgebraicNotation((int)nextPosition.x, (int)nextPosition.y);
                SquareConfiguration nextSquareConfiguration = BoardConfiguration.Instance.GetPieceAtSquare(nextSquare);

                if (nextSquareConfiguration == null || nextSquareConfiguration.Color != squareConfiguration.Color)
                {
                    nextPossiblePositions.Add(nextSquare);
                }
            }
        }

        return nextPossiblePositions;
    }

    public List<char> GetPiecesAttackingSquare(string square, bool color)
    {
        List<char> piecesAttackingSquare = new List<char>();
        Dictionary<string, SquareConfiguration> boardConfiguration = BoardConfiguration.Config;

        foreach(KeyValuePair<string, SquareConfiguration> entry in boardConfiguration)
        {
            if(entry.Value != null && entry.Value.Color == color && GetNextPossiblePositionsForPieceAtSquare(entry.Key).Contains(square))
            {
                piecesAttackingSquare.Add(entry.Value.Piece);
            }
        }


        return piecesAttackingSquare;
    }

    public bool IsCheckForPlayer(bool playerColor)
    {
        string kingSquare = BoardConfiguration.Instance.GetPiecePosition('K', playerColor);
        Dictionary<string, SquareConfiguration> boardConfiguration = BoardConfiguration.Config;

        bool isCheck = false;

        foreach (KeyValuePair<string, SquareConfiguration> entry in boardConfiguration)
        {
            if (entry.Value != null && entry.Value.Color == !playerColor && GetNextPossiblePositionsForPieceAtSquare(entry.Key).Contains(kingSquare))
            {
                isCheck = true;
                break;
            }
        }

        return isCheck;
    }

    public bool IsCheckMateForPlayer(bool playerColor)
    {
        //Test if the player is in check
        if (IsCheckForPlayer(playerColor) == false)
        {
            Debug.Log("The king is not in check");
            return false;
        }

        //Test if there is one move that could prevent the checkmate
        foreach (string squarePosition in BoardConfiguration.SquareAlgebraicNotations)
        {
            SquareConfiguration value = BoardConfiguration.Instance.GetPieceAtSquare(squarePosition);

            if (value != null && value.Color == playerColor)
            {
                List<string> possibleMovesForCurrentPiece = GetNextPossiblePositionsForPieceAtSquare(squarePosition);

                foreach(string possibleMove in possibleMovesForCurrentPiece)
                {
                    BoardConfiguration.Instance.MovePiece(squarePosition, possibleMove);
                    if(IsCheckForPlayer(playerColor) == false)
                    {
                        Debug.Log("King can escape by moving to: " + possibleMove);
                        BoardConfiguration.Instance.MovePiece(possibleMove, squarePosition);
                        return false;
                    }
                    BoardConfiguration.Instance.MovePiece(possibleMove, squarePosition);
                }
            }
        }
        
        return true;
    }

    public bool IsSquareUnderAttackByPlayer(string square, bool color)
    {
        return GetPiecesAttackingSquare(square, color).Count > 0;
    }
}
