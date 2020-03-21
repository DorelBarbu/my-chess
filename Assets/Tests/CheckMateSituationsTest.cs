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
}
