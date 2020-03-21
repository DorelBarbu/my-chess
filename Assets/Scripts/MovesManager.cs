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
        string kingSquare = BoardConfiguration.Instance.GetPiecePosition('K', playerColor);
        Dictionary<string, SquareConfiguration> boardConfiguration = BoardConfiguration.Config;

        //Test if the player is in check

        if (IsCheckForPlayer(playerColor) == false)
        {
            return false;
        }

        //Test if the king can move to an available position

        List<string> availableMovesForKing = GetNextPossiblePositionsForPieceAtSquare(kingSquare);

        foreach(string availableMove in availableMovesForKing)
        {
            if(IsSquareUnderAttackByPlayer(availableMove, !playerColor) == false)
            {
                return false;
            }
        }

        //Test to see if the king is under attack by only one piece and that piece can be captured by the player at move
        List<char> piecesAttackingKing = GetPiecesAttackingSquare(kingSquare, !playerColor);

        if(piecesAttackingKing.Count == 1)
        {
            string attackingPieceSquare = BoardConfiguration.Instance.GetPiecePosition(piecesAttackingKing[0], !playerColor);
            if(IsSquareUnderAttackByPlayer(attackingPieceSquare, playerColor) == true)
            {
                return false;
            }
        }

        return true;
    }

    public bool IsSquareUnderAttackByPlayer(string square, bool color)
    {
        return GetPiecesAttackingSquare(square, color).Count > 0;
    }
}
