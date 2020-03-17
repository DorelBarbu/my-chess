using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowedMovesGenerator
{
    public static List<Vector2> GetMovesForDirection(int rowOffset, int columnOffset, int limit)
    {
        List<Vector2> moves = new List<Vector2>();
        for(int i = 1; i < limit; i++)
        {
            moves.Add(new Vector2(i * rowOffset, i * columnOffset));
        }
        return moves;
    }

}
