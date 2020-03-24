using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class BoardConfigurationTest
{
    [Test]
    public void TestSquareConfigurationConstructor()
    {
        SquareConfiguration squareConfiguration = new SquareConfiguration('K', false);

        Assert.True(squareConfiguration.Color == false);
        Assert.True(squareConfiguration.Piece == 'K');
    }

    [Test]
    public void TestGetPieceAtSquare()
    {
        BoardConfiguration.Instance.SetPiecePosition('K', false, "A1");

        Assert.True(BoardConfiguration.Instance.GetPieceAtSquare("A1").Color == false);
        Assert.True(BoardConfiguration.Instance.GetPieceAtSquare("A1").Piece == 'K');
    }

    [Test]
    public void TestGetPiecePosition()
    {
        BoardConfiguration.Instance.SetPiecePosition('Q', false, "A2");

        Assert.True(BoardConfiguration.Instance.GetPiecePosition('Q', false) == "A2");
    }

    [Test]
    public void TestMovePiece()
    {
        BoardConfiguration.Instance.ResetBoardConfiguration();

        BoardConfiguration.Instance.SetPiecePosition('K', true, "A1");
        BoardConfiguration.Instance.MovePiece("A1", "A2");

        Assert.True(BoardConfiguration.Instance.GetPieceAtSquare("A1") == null);
        Assert.True(BoardConfiguration.Instance.GetPieceAtSquare("A2").Color == true);
        Assert.True(BoardConfiguration.Instance.GetPieceAtSquare("A2").Piece == 'K');
    }

    [Test]
    public void TestMovePiece2()
    {
        BoardConfiguration.Instance.ResetBoardConfiguration();

        BoardConfiguration.Instance.SetPiecePosition('Q', true, "A1");
        BoardConfiguration.Instance.MovePiece("A1", "A2");

        Assert.True(BoardConfiguration.Instance.GetPieceAtSquare("A1") == null);
        Assert.True(BoardConfiguration.Instance.GetPieceAtSquare("A2").Color == true);
        Assert.True(BoardConfiguration.Instance.GetPieceAtSquare("A2").Piece == 'Q');
    }

    [Test]
    public void TesteMovePawn()
    {
        BoardConfiguration.Instance.ResetBoardConfiguration();

        BoardConfiguration.Instance.SetPiecePosition('P', true, "B2", -1);
        List<string> allowedMovesForPawn = MovesManager.Instance.GetNextPossiblePositionsForPieceAtSquare("B2");

        Assert.True(allowedMovesForPawn.Count == 2);
        Assert.True(allowedMovesForPawn.Contains("B3") == true);
        Assert.True(allowedMovesForPawn.Contains("B4") == true);
    }

    [Test]
    public void TesteMovePawn2()
    {
        BoardConfiguration.Instance.ResetBoardConfiguration();

        BoardConfiguration.Instance.SetPiecePosition('P', true, "B7", 1);
        List<string> allowedMovesForPawn = MovesManager.Instance.GetNextPossiblePositionsForPieceAtSquare("B7");

        Debug.Log(allowedMovesForPawn[0]);
        Debug.Log(allowedMovesForPawn[1]);
         Assert.True(allowedMovesForPawn.Count == 2);
        Assert.True(allowedMovesForPawn.Contains("B6") == true);
        Assert.True(allowedMovesForPawn.Contains("B5") == true);
    }
}
