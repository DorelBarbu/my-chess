using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : IPiece
{
    public List<List<Vector2>> GetAllowedMoves()
    {
        List<List<Vector2>> moves = new List<List<Vector2>>();
        moves.Add(new List<Vector2>() { new Vector2(-2, 1) });
        moves.Add(new List<Vector2>() { new Vector2(-2, -1) });
        moves.Add(new List<Vector2>() { new Vector2(-1, -2) });
        moves.Add(new List<Vector2>() { new Vector2(1, -2) });
        return moves;
    }
}
