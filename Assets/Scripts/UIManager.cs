using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Sprite highlightedSquareSprite;
    public Sprite blackSquareSprite;
    public Sprite whiteSquareSprite;
    public Sprite candidateSquareSprite;


    public static void DrawTrailOfPossibleMoves(GameObject pieceObj)
    {
        Piece currentPiece = pieceObj.GetComponent<Piece>();
        List < List < Vector2 >> allowedMovesDeltas = currentPiece.GetAllowedMovesDeltas();
        Vector2 coordinates = currentPiece.GetCoordinates();

        List<Square> greenSquares = new List<Square>();

        foreach (List<Vector2> direction in allowedMovesDeltas)
        {
            foreach (Vector2 v in direction)
            {
                int nextX = (int)coordinates.x + (int)v.x;
                int nextY = (int)coordinates.y + (int)v.y;

                if (Utils.IsInsideBoard(nextX, nextY))
                {
                    char nextXChessNotation = Utils.ConvertLineToChessNotation(nextX);
                    char nextYChessNotation = Utils.ConvertColumnToChessNotation(nextY);

                    Square square = Board.GetSquareAtPosition(nextXChessNotation, nextYChessNotation);

                    if (square.IsAvailableForMove(currentPiece.GetColor()))
                    {
                        square.MarkAsAvailableForMove();
                        greenSquares.Add(square);
                    }

                    if (square.GetPiece())
                    {
                        break;
                    }
                }

            }
        }
        Board.SetGreenSquares(greenSquares);
    }
}
