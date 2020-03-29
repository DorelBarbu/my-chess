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

    public Dictionary<char, List<List<Vector2>>> AllowedMovesIndexes
    {
        get;
        set;
    }

    private MovesList()
    {
        AllowedMovesIndexes = new Dictionary<char, List<List<Vector2>>>();

        AllowedMovesIndexes['K'] = GenerateAllowedIndexesForKing();
        AllowedMovesIndexes['Q'] = GenerateAllowedIndexesForQueen();
        AllowedMovesIndexes['R'] = GenerateAllowedIndexesForRook();
        AllowedMovesIndexes['N'] = GenerateAllowedMovesIndexesForKnight();
        AllowedMovesIndexes['B'] = GenerateAllowedIndexesForBishop();
        AllowedMovesIndexes['P'] = GenerateAllowedIndexesForPawn();


    }

    private List<List<Vector2>> GenerateAllowedIndexesForKing()
    {
        List<List<Vector2>> moves = new List<List<Vector2>>();

        moves.Add(AllowedMovesGenerator.GetMovesForDirection(-1, 0, 2));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(-1, 1, 2));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(0, 1, 2));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(1, 1, 2));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(1, 0, 2));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(1, -1, 2));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(0, -1, 2));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(-1, -1, 2));

        return moves;
    }

    private List<List<Vector2>> GenerateAllowedIndexesForQueen()
    {
        List<List<Vector2>> moves = new List<List<Vector2>>();

        moves.Add(AllowedMovesGenerator.GetMovesForDirection(-1, 0, Constants.TABLE_SIZE));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(-1, 1, Constants.TABLE_SIZE));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(0, 1, Constants.TABLE_SIZE));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(1, 1, Constants.TABLE_SIZE));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(1, 0, Constants.TABLE_SIZE));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(1, -1, Constants.TABLE_SIZE));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(0, -1, Constants.TABLE_SIZE));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(-1, -1, Constants.TABLE_SIZE));

        return moves;
    }

    private List<List<Vector2>> GenerateAllowedIndexesForRook()
    {
        List<List<Vector2>> moves = new List<List<Vector2>>();

        moves.Add(AllowedMovesGenerator.GetMovesForDirection(-1, 0, Constants.TABLE_SIZE));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(0, 1, Constants.TABLE_SIZE));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(1, 0, Constants.TABLE_SIZE));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(0, -1, Constants.TABLE_SIZE));

        return moves;
    }

    public List<List<Vector2>> GenerateAllowedMovesIndexesForKnight()
    {
        List<List<Vector2>> moves = new List<List<Vector2>>();

        moves.Add(new List<Vector2>() { new Vector2(-2, 1) });
        moves.Add(new List<Vector2>() { new Vector2(-2, -1) });
        moves.Add(new List<Vector2>() { new Vector2(-1, -2) });
        moves.Add(new List<Vector2>() { new Vector2(-1, 2) });
        moves.Add(new List<Vector2>() { new Vector2(1, -2) });
        moves.Add(new List<Vector2>() { new Vector2(2, -1) });
        moves.Add(new List<Vector2>() { new Vector2(2, 1) });
        moves.Add(new List<Vector2>() { new Vector2(1, 2) });

        return moves;
    }

    private List<List<Vector2>> GenerateAllowedIndexesForBishop()
    {
        List<List<Vector2>> moves = new List<List<Vector2>>();

        moves.Add(AllowedMovesGenerator.GetMovesForDirection(-1, 1, Constants.TABLE_SIZE));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(1, -1, Constants.TABLE_SIZE));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(1, 1, Constants.TABLE_SIZE));
        moves.Add(AllowedMovesGenerator.GetMovesForDirection(-1, -1, Constants.TABLE_SIZE));

        return moves;
    }

     private List<List<Vector2>> GenerateAllowedIndexesForPawn()
    {
        List<List<Vector2>> moves = new List<List<Vector2>>();

        moves.Add(AllowedMovesGenerator.GetMovesForDirection(1, 0, 2));

        return moves;
    }
}
