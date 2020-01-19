using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {
    [SerializeField] private ColorsEnum color;
    [SerializeField] private PieceControllerType type;
    [SerializeField] public Vector2 coordinates;
    private List<List<Vector2>> allowedMovesDeltas;
    private IPiece piece;

    private void Awake()
    {
        piece = PieceFactory.createInstance(type);
        allowedMovesDeltas = piece.GetAllowedMoves();
    }

    private void MatchPiecePositionToSquare()
    {
        Square containingSquare = transform.parent.gameObject.GetComponent<Square>();
        coordinates = Utils.ConvertToCartesian(containingSquare.X, containingSquare.Y);
    }

    private void Start()
    {
        MatchPiecePositionToSquare();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(Constants.LEFT_MOUSE_BUTTON))
        {
            DrawTrailOfPossibleMoves();
        }

        if (Input.GetMouseButtonUp(Constants.LEFT_MOUSE_BUTTON))
        {
            Board.ClearGreenSquares();
            Board.SetGreenSquares(null);
            MatchPiecePositionToSquare();
        }
    }

    public void DrawTrailOfPossibleMoves()
    {
        List<Square> greenSquares = new List<Square>();
        foreach (List<Vector2> direction in allowedMovesDeltas)
        {
            foreach (Vector2 v in direction)
            {
                int nextX = (int)coordinates.x + (int)v.x;
                int nextY = (int)coordinates.y + (int)v.y;
                if (Utils.IsInsideBoard(nextX, nextY))
                {
                    Square square = Board.GetSquareAtPosition(Utils.ConvertLineToChessNotation(nextX), Utils.ConvertColumnToChessNotation(nextY));
                    square.MarkAsAvailableForMove();
                    square.CanMoveTo = true;
                    greenSquares.Add(square);
                }
            }
        }
        Board.SetGreenSquares(greenSquares);
    }

    



}
