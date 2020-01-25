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

    public void MatchPiecePositionToSquare()
    {
        if(transform.parent)
        {
            Square containingSquare = transform.parent.gameObject.GetComponent<Square>();
            coordinates = Utils.ConvertToCartesian(containingSquare.X, containingSquare.Y);
            containingSquare.SetOccupied(true);
        }
        
    }

    private void Start()
    {
        MatchPiecePositionToSquare();
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(Constants.LEFT_MOUSE_BUTTON))
        {
            UIManager.DrawTrailOfPossibleMoves(gameObject);
        }
    }

    public ColorsEnum GetColor()
    {
        return color;
    }

    public List<List<Vector2>> GetAllowedMovesDeltas()
    {
        return allowedMovesDeltas;
    }

    public Vector2 GetCoordinates()
    {
        return coordinates;
    }



}
