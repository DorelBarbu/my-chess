using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private static Dictionary<string, Square> SquareMapping = new Dictionary<string, Square>();
    private static List<Square> greenSquares;
    private void Awake()
    {
        MapSquares();
        greenSquares = new List<Square>();
    }

    public static void SetGreenSquares(List<Square> squares)
    {
        greenSquares = squares;
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
            SquareMapping.Add(square.gameObject.name, square);
        }
    }

    public static Square GetSquareAtPosition(char line, char column)
    {
        char[] arr = { line, column };
        return SquareMapping[new string(arr)];
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
