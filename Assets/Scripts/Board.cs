using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Dictionary<string, Square> SquareMapping = new Dictionary<string, Square>();
    private static List<Square> greenSquares;
    private const string TOP_LEFT = "8A";
    private const string TOP_RIGHT = "8H";
    private const string BOTTOM_LEFT = "1A";
    private const string BOTTOM_RIGHT = "1H";
    private void Awake()
    {
        MapSquares();
        greenSquares = new List<Square>();
    }

    public static void SetGreenSquares(List<Square> squares)
    {
        greenSquares = squares;
        if(squares != null)
        {
            foreach (Square square in squares)
            {
                square.MarkAsAvailableForMove();
            }
        }
      
    }

    public static void ClearGreenSquares()
    {
        foreach(Square square in greenSquares)
        {
            square.CanMoveTo = false;
            square.ResetSprite();
        }
    }

    private void MapSquares()
    {
        Square[] squares = FindObjectsOfType<Square>();
        foreach(Square square in squares)
        {
            char[] arr = { square.Y, square.X };
            SquareMapping.Add(new string(arr), square);
        }
    }

    public static Square GetSquareAtPosition(char line, char column)
    {
        char[] arr = { column, line };
        return SquareMapping[new string(arr)];
    }
}
