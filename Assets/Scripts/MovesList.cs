using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesList
{
    private static MovesList movesListInstance;

    public static MovesList Instance
    {
        get
        {
            if(movesListInstance == null)
            {
                movesListInstance = new MovesList();
            }
            return movesListInstance;
        }
    }

    public Dictionary<char, List<Vector2>> AllowedMovesIndexes
    {
        get;
        set;
    }

    private MovesList()
    {
        AllowedMovesIndexes = new Dictionary<char, List<Vector2>>();

        AllowedMovesIndexes['K'] = GenerateAllowedIndexesForKing();
        AllowedMovesIndexes['Q'] = GenerateAllowedIndexesForQueen();
      
    }

    private List<Vector2> GenerateAllowedIndexesForKing()
    {
        List<Vector2> moves = new List<Vector2>();

        moves.AddRange(AllowedMovesGenerator.GetMovesForDirection(-1, 0, 2));
        moves.AddRange(AllowedMovesGenerator.GetMovesForDirection(-1, 1, 2));
        moves.AddRange(AllowedMovesGenerator.GetMovesForDirection(0, 1, 2));
        moves.AddRange(AllowedMovesGenerator.GetMovesForDirection(1, 1, 2));
        moves.AddRange(AllowedMovesGenerator.GetMovesForDirection(1, 0, 2));
        moves.AddRange(AllowedMovesGenerator.GetMovesForDirection(1, -1, 2));
        moves.AddRange(AllowedMovesGenerator.GetMovesForDirection(0, -1, 2));
        moves.AddRange(AllowedMovesGenerator.GetMovesForDirection(-1, -1, 2));

        return moves;
    }

    private List<Vector2> GenerateAllowedIndexesForQueen()
    {
        List<Vector2> moves = new List<Vector2>();

        moves.AddRange(AllowedMovesGenerator.GetMovesForDirection(-1, 0, Constants.TABLE_SIZE));
        moves.AddRange(AllowedMovesGenerator.GetMovesForDirection(-1, 1, Constants.TABLE_SIZE));
        moves.AddRange(AllowedMovesGenerator.GetMovesForDirection(0, 1, Constants.TABLE_SIZE));
        moves.AddRange(AllowedMovesGenerator.GetMovesForDirection(1, 1, Constants.TABLE_SIZE));
        moves.AddRange(AllowedMovesGenerator.GetMovesForDirection(1, 0, Constants.TABLE_SIZE));
        moves.AddRange(AllowedMovesGenerator.GetMovesForDirection(1, -1, Constants.TABLE_SIZE));
        moves.AddRange(AllowedMovesGenerator.GetMovesForDirection(0, -1, Constants.TABLE_SIZE));
        moves.AddRange(AllowedMovesGenerator.GetMovesForDirection(-1, -1, Constants.TABLE_SIZE));

        return moves;
    }
}
