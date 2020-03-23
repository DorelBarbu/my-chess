using System.Collections;
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
}
