using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceDraggableHandler : MonoBehaviour, IDraggableHandler
{
    private Camera cam;
    private Square previousSquare;
    private Transform parentTransform;
    private Piece piece;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        piece = gameObject.GetComponent<Piece>();
    }

    public void HandleDragStart()
    {
        UIManager.DrawTrailOfPossibleMoves(gameObject);

        previousSquare = null;
        parentTransform = transform.parent;
    }

    public IEnumerator RunDurringDragging()
    {
        Vector3 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Square currentSquare = Utils.GetObjectBelow(newPosition.x, newPosition.y).GetComponent<Square>();

        if (previousSquare)
        {
            previousSquare.ResetSprite();
        }

        if (currentSquare)
        {
            currentSquare.Highlight();
            previousSquare = currentSquare;
        }

        yield return null;
    }

    public void HandleDragFinnish()
    {
        if (previousSquare)
        {
            previousSquare.ResetSprite();
            previousSquare.GetPiece();
            if(previousSquare.CanMoveTo == true)
            {
                Piece overlappingPiece = previousSquare.GetPiece();
                if(overlappingPiece)
                {
                    Destroy(overlappingPiece.gameObject);
                }
                parentTransform.gameObject.GetComponent<Square>().SetOccupied(false);
                Utils.PlaceOnObject(gameObject, previousSquare.gameObject);
                previousSquare.SetOccupied(true);
                EndTurnEvent.Invoke(FindObjectOfType<GameManager>().AtMove);

            }
            else
            {
                Utils.PlaceOnObject(gameObject, parentTransform.gameObject);
            }
        }

        piece.MatchPiecePositionToSquare();
        Board.ClearGreenSquares();
        Board.SetGreenSquares(null);
    }

    public delegate void EndTurn(ColorsEnum colorAtMove);

    public static event EndTurn EndTurnEvent;
}
