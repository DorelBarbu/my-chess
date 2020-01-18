using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowedMovesGenerator
{
    public List<Vector2> GetAllowedMovesUp(int limit)
    {
        List<Vector2> allowedMoves = new List<Vector2>();
        for(int i = 1; i < Constants.TABLE_SIZE; i++)
        {
            allowedMoves.Add(new Vector2(i, 0));
        }
        return allowedMoves;
    }

    public List<Vector2> GetAllowedMovesDown(int limit)
    {
        List<Vector2> allowedMoves = new List<Vector2>();
        for (int i = 1; i < Constants.TABLE_SIZE; i++)
        {
            allowedMoves.Add(new Vector2(-i, 0));
        }
        return allowedMoves;
    }

    public List<Vector2> GetAllowedMovesLeft(int limit)
    {
        List<Vector2> allowedMoves = new List<Vector2>();
        for (int i = 1; i < Constants.TABLE_SIZE; i++)
        {
            allowedMoves.Add(new Vector2(0, -i));
        }
        return allowedMoves;
    }

    public List<Vector2> GetAllowedMovesRight(int limit)
    {
        List<Vector2> allowedMoves = new List<Vector2>();
        for (int i = 1; i < Constants.TABLE_SIZE; i++)
        {
            allowedMoves.Add(new Vector2(0, i));
        }
        return allowedMoves;
    }

}
