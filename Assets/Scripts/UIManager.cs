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
        Board.SetGreenSquares(Utils.GetTrailOfPossibleMovesEnhanced(pieceObj));
    }
}
