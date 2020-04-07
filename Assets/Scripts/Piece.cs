using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {
    [SerializeField] private ColorsEnum color;
    [SerializeField] private PieceControllerType type;
    [SerializeField] public Vector2 coordinates;
    [SerializeField] private int movingDirection;

    private List<List<Vector2>> allowedMovesDeltas;

    public PieceControllerType Type
    {
        get { return type; }
        set { type = value; }
    }

    public IPiece IPiece { get; set; }

    private void Awake()
    {
        IPiece = PieceFactory.createInstance(type);
        allowedMovesDeltas = IPiece.GetAllowedMoves();
        //FindObjectOfType<GameManager>().AddPiece(this);
    }

    public void AddPieceToBoardConfiguration()
    {
        BoardConfiguration.Instance.SetPiecePosition(Constants.PIECE_MAPPING[type], Constants.COLOR_MAPPING[color], GetSquare().GetAlgebraicCoordinates(), movingDirection);
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
        AddPieceToBoardConfiguration();
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(Constants.LEFT_MOUSE_BUTTON))
        {
            //UIManager.DrawTrailOfPossibleMoves(gameObject);
        }
    }

    public ColorsEnum GetColor()
    {
        return color;
    }

    public void SetColor(ColorsEnum value)
    {
        color = value;
    }

    public List<List<Vector2>> GetAllowedMovesDeltas()
    {
        return allowedMovesDeltas;
    }

    public Vector2 GetCoordinates()
    {
        return coordinates;
    }

    public void RevertToPreviousPosition(Transform parentTransform)
    {
        Utils.PlaceOnObject(gameObject, parentTransform.gameObject);
    }
    
    public void PlaceOnSquare(Square destinationSquare, Transform parentTransform)
    {
        parentTransform.gameObject.GetComponent<Square>().SetOccupied(false);
        Utils.PlaceOnObject(gameObject, destinationSquare.gameObject);
        destinationSquare.SetOccupied(true);
        MatchPiecePositionToSquare();
    }

    public void RemovePieceFromGameManagerPiecesList()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if(gameManager != null) gameManager.getPiecesOfColor(color).Remove(this);
    }

    private void OnDestroy()
    {
        RemovePieceFromGameManagerPiecesList();
    }

    public Square GetSquare()
    {
        return transform.parent.GetComponent<Square>();
    }

    public void RemoveFromGame()
    {
        Destroy(gameObject);
    }
}
