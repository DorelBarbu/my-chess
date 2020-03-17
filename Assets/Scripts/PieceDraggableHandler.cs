using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceDraggableHandler : MonoBehaviour, IDraggableHandler
{
    private Camera cam;
    private Square destinationSquare;
    private Transform parentTransform;
    private Piece piece;
    private GameManager gameManager;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        piece = gameObject.GetComponent<Piece>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void HandleDragStart()
    {
        UIManager.DrawTrailOfPossibleMoves(gameObject);

        destinationSquare = null;
        parentTransform = transform.parent;
    }

    public IEnumerator RunDurringDragging()
    {
        Vector3 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Square currentSquare = Utils.GetObjectBelow(newPosition.x, newPosition.y).GetComponent<Square>();

        if (destinationSquare)
        {
            destinationSquare.ResetSprite();
        }

        if (currentSquare)
        {
            currentSquare.Highlight();
            destinationSquare = currentSquare;
        }

        yield return null;
    }

    public void HandleDragFinnish()
    {
        if (destinationSquare)
        {
            destinationSquare.ResetSprite();
            if(destinationSquare.CanMoveTo == true)
            {
                piece.PlaceOnSquare(destinationSquare, parentTransform);
                //Debug.Log("Placed " + piece.name + " on " + piece.coordinates.x + " " + piece.coordinates.y);
                if (Utils.isCheck(piece.GetColor(), gameManager) == true)
                {
                    Debug.Log("You put yourself in chess " + piece.GetColor());
                    piece.RevertToPreviousPosition(parentTransform);
                    piece.MatchPiecePositionToSquare();
                    destinationSquare.SetOccupied(false);
                }
                else
                {
                    if(Utils.isCheck(Utils.NegateColor(piece.GetColor()), gameManager))
                    {
                        Debug.Log("You put the other player in check");
                    }
                    EndTurnEvent.Invoke(FindObjectOfType<GameManager>().AtMove, (Utils.isCheck(Utils.NegateColor(piece.GetColor()), gameManager)));
                }

            }
            else
            {
                piece.RevertToPreviousPosition(parentTransform);
            }
        }

        Board.ClearGreenSquares();
        Board.SetGreenSquares(null);
    }

    public delegate void EndTurn(ColorsEnum colorAtMove, bool isCheck);

    public static event EndTurn EndTurnEvent;
}
