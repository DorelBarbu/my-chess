using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class MovementSystem
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
        GameObject queen = Utils.CreatePieceGameObject("BQ", PieceControllerType.QUEEN, ColorsEnum.BLACK);

        Square square = Board.SquareMapping["C8"];
        Utils.PlaceOnObject(queen, square.gameObject);

        Assert.NotNull(square);
        Assert.NotNull(square.GetPiece());
        Assert.IsTrue(square.GetPiece().name == "BQ");


        yield return null;
    }

    [UnityTest]
    public IEnumerator TestGetKingSquareForPlayer()
    {
        GameObject king = Utils.CreatePieceGameObject("BK", PieceControllerType.KING, ColorsEnum.BLACK);
        Square square = Board.SquareMapping["C3"];
        Utils.PlaceOnObject(king, square.gameObject);

        Assert.True(MonoBehaviour.FindObjectOfType<GameManager>().getPiecesOfColor(ColorsEnum.BLACK).Count == 1);
        Assert.NotNull(Utils.GetKingSquareForPlayer(ColorsEnum.BLACK, MonoBehaviour.FindObjectOfType<GameManager>()));

        yield return null;

    }
}
