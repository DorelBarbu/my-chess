﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : IPiece
{
    public List<List<Vector2>> GetAllowedMoves()
    {
        List<List<Vector2>> moves = new List<List<Vector2>>();
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(-1, 0, Constants.TABLE_SIZE));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(0, 1, Constants.TABLE_SIZE));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(1, 0, Constants.TABLE_SIZE));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(0, -1, Constants.TABLE_SIZE));

        return moves;
    }
}
