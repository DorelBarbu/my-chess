﻿using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MovesManagerTest
{
    [Test]
    public void TestSquareConfigurationConstructor()
    {
        BoardConfiguration.Instance.ResetBoardConfiguration();

        BoardConfiguration.Instance.SetPiecePosition('K', false, "A1");
        BoardConfiguration.Instance.SetPiecePosition('Q', false, "A2");
        BoardConfiguration.Instance.SetPiecePosition('Q', true, "B1");

        List<string> possibleMoves = MovesManager.Instance.GetNextPossiblePositionsForPieceAtSquare("A1");

        Assert.True(possibleMoves.Count == 2);
        Assert.True(possibleMoves.Contains("A2") == false);
        Assert.True(possibleMoves.Contains("B1") == true);
        Assert.True(possibleMoves.Contains("B2") == true);
    }

    [Test]
    public void TestGetPiecesAttackingSquare()
    {
        BoardConfiguration.Instance.ResetBoardConfiguration();

        BoardConfiguration.Instance.SetPiecePosition('K', false, "A1");
        BoardConfiguration.Instance.SetPiecePosition('Q', false, "H8");

        List<char> piecesAttackingSquare = MovesManager.Instance.GetPiecesAttackingSquare("B2", false);

        Assert.True(piecesAttackingSquare.Count == 2);
        Assert.True(piecesAttackingSquare.Contains('K') == true);
        Assert.True(piecesAttackingSquare.Contains('Q') == true);
    }

    [Test]

    public void TestIsCheckForPlayer()
    {
        BoardConfiguration.Instance.ResetBoardConfiguration();

        BoardConfiguration.Instance.SetPiecePosition('K', false, "A1");
        BoardConfiguration.Instance.SetPiecePosition('Q', true, "H8");

        Assert.True(MovesManager.Instance.IsCheckForPlayer(false) == true);
    }
}
