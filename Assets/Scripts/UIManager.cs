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
        Piece piece = pieceObj.GetComponent<Piece>();
        List < List < Vector2 >> allowedMovesDeltas = piece.GetAllowedMovesDeltas();
        Vector2 coordinates = piece.GetCoordinates();
        ColorsEnum color = piece.GetColor();

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
                    Piece nextPiece = square.GetPiece();

                    if (square.GetOccupied() == false || nextPiece.GetColor() != color)
                    {
                        square.MarkAsAvailableForMove();
                        greenSquares.Add(square);
                    }
                    else
                    {
                        break;
                    }

                    if (nextPiece && nextPiece.GetColor() != color)
                    {
                        break;
                    }
                }

            }
        }
        Board.SetGreenSquares(greenSquares);
    }
}
