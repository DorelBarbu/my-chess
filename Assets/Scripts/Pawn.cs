using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : IPiece
{
    public List<List<Vector2>> GetAllowedMoves()
    {
        List<List<Vector2>> moves = new List<List<Vector2>>();
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(1, 0, 3));

        return moves;
    }
}
