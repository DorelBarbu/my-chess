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
    private Square currentSquare;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        piece = gameObject.GetComponent<Piece>();
        gameManager = FindObjectOfType<GameManager>();
        currentSquare = piece.GetSquare();
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
        GameObject objectBelow = Utils.GetObjectBelow(newPosition.x, newPosition.y, Constants.SQUARE);

        if(objectBelow != null)
        {
            Square currentSquare = Utils.GetObjectBelow(newPosition.x, newPosition.y, Constants.SQUARE).GetComponent<Square>();

            if (destinationSquare)
            {
                destinationSquare.ResetSprite();
            }

            if (currentSquare)
            {
                currentSquare.Highlight();
                destinationSquare = currentSquare;
            }
        }

        yield return null;
    }

    public void HandleDragFinnish()
    {
        bool currentPlayerColor = Constants.COLOR_MAPPING[piece.GetColor()];

        if (destinationSquare)
        {
            destinationSquare.ResetSprite();
            Piece destinationSquarePiece = destinationSquare.GetPiece();

            if (destinationSquare.CanMoveTo == true)
            {
                BoardConfiguration.Instance.MovePiece(currentSquare.GetAlgebraicCoordinates(), destinationSquare.GetAlgebraicCoordinates());

                if(MovesManager.Instance.IsCheckForPlayer(currentPlayerColor))
                {
                    //Debug.Log("You put yourself in check");
                    BoardConfiguration.Instance.MovePiece(destinationSquare.GetAlgebraicCoordinates(), currentSquare.GetAlgebraicCoordinates());
                    piece.RevertToPreviousPosition(parentTransform);
                    if(destinationSquarePiece != null)
                    {
                        destinationSquarePiece.AddPieceToBoardConfiguration();
                    }
                }
                else
                {
                    if(destinationSquarePiece != null)
                    {
                        Debug.Log("Found piece on the destination square");
                        destinationSquarePiece.RemoveFromGame();
                    }
                    piece.PlaceOnSquare(destinationSquare, parentTransform);
                    currentSquare = piece.GetSquare();
                    EndTurnEvent.Invoke(FindObjectOfType<GameManager>().AtMove, MovesManager.Instance.IsCheckForPlayer(!currentPlayerColor));
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
