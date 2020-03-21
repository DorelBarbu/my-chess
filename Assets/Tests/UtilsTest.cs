using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class UtilsTest
{
    private static string SCENE_NAME = "EmptyBoard";
    private static bool isSceneLoaded = false;

    [SetUp]
    public void LoadEmptyScene()
    {
        if (!isSceneLoaded)
        {
            SceneManager.LoadScene(SCENE_NAME);
            isSceneLoaded = true;
        }
    }


    [UnityTest]
    public IEnumerator TestLoadingScreen()
    {
        Scene loadedScene = SceneManager.GetSceneByName(SCENE_NAME);
        Assert.IsTrue(loadedScene.isLoaded);
        yield return null;
    }

    [UnityTest]
    public IEnumerator TestInstantiatingPieceAndPlaceOnSquare()
    {
        Utils.InstantiatePieceAndPlaceOnSquare("BQ", PieceControllerType.QUEEN, ColorsEnum.BLACK, "C8");

        Square square = Board.SquareMapping["C8"];

        Assert.NotNull(square);
        Assert.NotNull(square.GetPiece());
        Assert.IsTrue(square.GetPiece().name == "BQ");


        yield return null;
    }

    [UnityTest]
    public IEnumerator TestGetKingSquareForPlayer()
    {
        GameObject king = Utils.InstantiatePieceAndPlaceOnSquare("BK", PieceControllerType.KING, ColorsEnum.BLACK, "C3");
        MonoBehaviour.FindObjectOfType<GameManager>().AddPiece(king.GetComponent<Piece>());

        Assert.True(MonoBehaviour.FindObjectOfType<GameManager>().getPiecesOfColor(ColorsEnum.BLACK).Count == 1);
        Assert.NotNull(Utils.GetKingSquareForPlayer(ColorsEnum.BLACK, MonoBehaviour.FindObjectOfType<GameManager>()));

        yield return null;
    }

    [Test]
    public void TestConvertToCartesian()
    {
        Vector2 v = Utils.ConvertToCartesian('8', 'D');

        Assert.True(v.x == 0);
        Assert.True(v.y == 3);
    }

    [Test]
    public void TestConvertToAlgebraicNotation()
    {
        Assert.True(Utils.ConverToAlgebraicNotation(0, 3) == "D8");
    }
}
