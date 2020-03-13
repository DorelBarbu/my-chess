using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : IPiece
{
    public List<List<Vector2>> GetAllowedMoves()
    {
        List<List<Vector2>> moves = new List<List<Vector2>>();
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(-1, 0, 1));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(-1, 1, 1));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(0, 1, 1));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(1, 1, 1));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(1, 0, 1));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(1, -1, 1));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(0, -1, 1));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(-1, -1, 1));
        return moves;
    }
}
