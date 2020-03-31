using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

//true - black
//false - white

public class CheckMateSituationTest
{
    [Test]
    public void Test1()
    {
        BoardConfiguration.Instance.ResetBoardConfiguration();

        BoardConfiguration.Instance.SetPiecePosition('K', false, "F5");
        BoardConfiguration.Instance.SetPiecePosition('K', true, "H5");
        BoardConfiguration.Instance.SetPiecePosition('R', false, "H1");

        Assert.True(MovesManager.Instance.IsCheckMateForPlayer(true) == true);
    }

    [Test]
    public void Test2()
    {
        Debug.Log("<----- Test " + "2" + " ----->");
        BoardConfiguration.Instance.ResetBoardConfiguration();

        BoardConfiguration.Instance.SetPiecePosition('K', false, "C1");
        BoardConfiguration.Instance.SetPiecePosition('R', true, "C2");
        BoardConfiguration.Instance.SetPiecePosition('N', true, "C3");
        BoardConfiguration.Instance.SetPiecePosition('B', true, "B3");
        BoardConfiguration.Instance.SetPiecePosition('B', true, "B4");

        Assert.True(MovesManager.Instance.IsCheckMateForPlayer(false) == true);
    }

    [Test]
    public void Test3()
    {
        Debug.Log("<----- Test " + "3" + " ----->");
        BoardConfiguration.Instance.ResetBoardConfiguration();

        BoardConfiguration.Instance.SetPiecePosition('Q', false, "D1");
        BoardConfiguration.Instance.SetPiecePosition('P', false, "D2", -1);
        BoardConfiguration.Instance.SetPiecePosition('K', false, "E1");
        BoardConfiguration.Instance.SetPiecePosition('P', false, "E2", -1);
        BoardConfiguration.Instance.SetPiecePosition('B', false, "F1");
        BoardConfiguration.Instance.SetPiecePosition('Q', true, "H4");


        Assert.True(MovesManager.Instance.IsCheckMateForPlayer(false) == true);
    }

    [Test]
    public void Test4()
    {
        Debug.Log("<----- Test " + "3" + " ----->");
        BoardConfiguration.Instance.ResetBoardConfiguration();

        BoardConfiguration.Instance.SetPiecePosition('K', true, "A6");
        BoardConfiguration.Instance.SetPiecePosition('Q', false, "B6");
        BoardConfiguration.Instance.SetPiecePosition('K', false, "C6");


        Assert.True(MovesManager.Instance.IsCheckMateForPlayer(true) == true);
    }

    [Test]
    public void Test5()
    {
        Debug.Log("<----- Test " + "5" + " ----->");
        BoardConfiguration.Instance.ResetBoardConfiguration();

        BoardConfiguration.Instance.SetPiecePosition('K', false, "A1");
        BoardConfiguration.Instance.SetPiecePosition('P', false, "A2", -1);
        BoardConfiguration.Instance.SetPiecePosition('P', false, "B2", -1);
        BoardConfiguration.Instance.SetPiecePosition('N', false, "D3");
        BoardConfiguration.Instance.SetPiecePosition('R', true, "H1");

        Assert.True(MovesManager.Instance.IsCheckMateForPlayer(false) == false);
    }
}
