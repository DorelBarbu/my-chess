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

    private bool CanPawnCapturePiece(string pieceLocation, bool pawnColor)
    {
        SquareConfiguration pieceSquareConfiguration = BoardConfiguration.Instance.GetPieceAtSquare(pieceLocation);
        return pieceSquareConfiguration != null && pieceSquareConfiguration.Color != pawnColor;
    }

    
    public List<string> GetDiagonalMovesForPawn(string piecePosition)
    {
        SquareConfiguration squareConfiguration = BoardConfiguration.Instance.GetPieceAtSquare(piecePosition);

        if(squareConfiguration.Piece != 'P')
        {
            return null;
        }
        
        List<string> allowedMovesForPawn = new List<string>();

        Vector2 pawnCartesianCoordinates = Utils.ConvertToCartesian(piecePosition[1], piecePosition[0]);
        bool pawnColor = BoardConfiguration.Instance.GetPieceAtSquare(piecePosition).Color;

        Vector2 upLeftMove = pawnCartesianCoordinates + new Vector2(squareConfiguration.MovingDirection, -1);
        Vector2 upRightMove = pawnCartesianCoordinates + new Vector2(squareConfiguration.MovingDirection, 1);

        if(Utils.IsInsideBoard((int)upLeftMove.x, (int)upLeftMove.y) == true)
        {
            string upLeftMoveAlgebraicCoordinates = Utils.ConvertCartesianToAlgebraic(upLeftMove);
            if(CanPawnCapturePiece(upLeftMoveAlgebraicCoordinates, pawnColor))
            {
                allowedMovesForPawn.Add(upLeftMoveAlgebraicCoordinates);
            }
        }

        if(Utils.IsInsideBoard((int)upRightMove.x, (int)upRightMove.y) == true)
        {
            string upRightMoveAlgebraicCoordinates = Utils.ConvertCartesianToAlgebraic(upRightMove);
            if(CanPawnCapturePiece(upRightMoveAlgebraicCoordinates, pawnColor))
            {
                allowedMovesForPawn.Add(upRightMoveAlgebraicCoordinates);
            }
        }

        return allowedMovesForPawn;
    }

    private void AddTwoSquareStepMoveForPawn(string piecePosition, List<string> allowedMovesForPawn)
    {
        if(IsPawnInitialPosition(piecePosition))
        {
            SquareConfiguration squareConfiguration = BoardConfiguration.Instance.GetPieceAtSquare(piecePosition);
            Vector2 pawnCartesianCoordinates = Utils.ConvertToCartesian(piecePosition[1], piecePosition[0]);
            bool pawnColor = BoardConfiguration.Instance.GetPieceAtSquare(piecePosition).Color;

            Vector2 twoSquareMove = pawnCartesianCoordinates + new Vector2(2 * squareConfiguration.MovingDirection, 0);

            if (Utils.IsInsideBoard((int)twoSquareMove.x, (int)twoSquareMove.y))
            {
                allowedMovesForPawn.Add(Utils.ConvertCartesianToAlgebraic(twoSquareMove));
            }
        }
    }

    public bool IsPawnInitialPosition(string pawnPosition)
    {
        SquareConfiguration pawnSquareConfiguration = BoardConfiguration.Instance.GetPieceAtSquare(pawnPosition);

        return pawnSquareConfiguration.MovingDirection == -1 && pawnPosition[1] == '2' ||
            pawnSquareConfiguration.MovingDirection == 1 && pawnPosition[1] == '7';
    }

    public List<string> GetNextPossiblePositionsForPieceAtSquare(string piecePosition)
    {
        SquareConfiguration squareConfiguration = BoardConfiguration.Instance.GetPieceAtSquare(piecePosition);
        if(squareConfiguration == null)
        {
            return null;
        }
        List<List<Vector2>> allowedMovesDeltas = MovesList.Instance.AllowedMovesIndexes[squareConfiguration.Piece];
        Vector2 origin = Utils.ConvertToCartesian(piecePosition[1], piecePosition[0]);
        List<string> nextPossiblePositions = new List<string>();

        foreach(List<Vector2> deltasForDirection in allowedMovesDeltas)
        {
            foreach(Vector2 delta in deltasForDirection)
            {
                Vector2 nextPosition;
                nextPosition.x = origin.x + squareConfiguration.MovingDirection * delta.x;
                nextPosition.y = origin.y + delta.y;

                if (Utils.IsInsideBoard((int)nextPosition.x, (int)nextPosition.y))
                {
                    string nextSquare = Utils.ConverToAlgebraicNotation((int)nextPosition.x, (int)nextPosition.y);
                    SquareConfiguration nextSquareConfiguration = BoardConfiguration.Instance.GetPieceAtSquare(nextSquare);

                    if (nextSquareConfiguration == null || nextSquareConfiguration.Color != squareConfiguration.Color)
                    {
                        nextPossiblePositions.Add(nextSquare);
                    }

                    if(nextSquareConfiguration != null)
                    {
                        break;
                    }
                }
            }
          
        }

        // Handle the special cases for pawn separately
        if(squareConfiguration.Piece == 'P')
        {
            nextPossiblePositions.AddRange(GetDiagonalMovesForPawn(piecePosition));
            AddTwoSquareStepMoveForPawn(piecePosition, nextPossiblePositions);
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
        //Debug.Log("Check if the King is in chess at position " + kingSquare);

        bool isCheck = false;

        foreach (KeyValuePair<string, SquareConfiguration> entry in boardConfiguration)
        {
            if (entry.Value != null && entry.Value.Color == !playerColor)
            {
                //Debug.Log("Attack from: " + entry.Value.Piece + " at " + entry.Key + " ?");
                List<string> possibleMovesForCurrentPiece = GetNextPossiblePositionsForPieceAtSquare(entry.Key);
                if(possibleMovesForCurrentPiece.Contains(kingSquare) == true)
                {
                    //Debug.Log("Check detected");
                    isCheck = true;
                    break;
                }
            }
        }

        return isCheck;
    }

    public bool IsCheckMateForPlayer(bool playerColor)
    {
        //Test if the player is in check
        if (IsCheckForPlayer(playerColor) == false)
        {
            //Debug.Log("The king is not in check");
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
                    //Debug.Log("Trying to move " + value.Piece + " from " + squarePosition + "to " + possibleMove);
                    SquareConfiguration pieceAtPossibleMove = BoardConfiguration.Instance.GetPieceAtSquare(possibleMove);
                    BoardConfiguration.Instance.MovePiece(squarePosition, possibleMove);
                    if(IsCheckForPlayer(playerColor) == false)
                    {
                        //Debug.Log("King can escape by moving to: " + possibleMove);
                        BoardConfiguration.Instance.MovePiece(possibleMove, squarePosition);
                        return false;
                    }
                    BoardConfiguration.Instance.MovePiece(possibleMove, squarePosition);
                    BoardConfiguration.Config[possibleMove] = pieceAtPossibleMove;
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
