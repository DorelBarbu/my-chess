using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPieceMovementManager
{
    private GameObject piece;
    private bool isDragging;
    private Transform parentTransform;

    public UIPieceMovementManager(GameObject piece)
    {
        isDragging = false;
        this.piece = piece;
        parentTransform = piece.transform.parent;
    }

    public bool GetDraggingState()
    {
        return isDragging;
    }

    public void SetDraggingState(bool newIsDraggingValue)
    {
        isDragging = newIsDraggingValue;
    }

    private void MoveToCursor(Vector3 newPosition)
    {
        //Decoupling the piece from the parent square
        piece.transform.parent = null;
        piece.transform.position = newPosition;
    }

    private void PlaceOnSquare(Square square)
    {
        piece.transform.parent = square.transform;
        piece.transform.localPosition = Vector3.zero;
    }

    private GameObject GetOverlappingSquare(float x, float y)
    {
        Collider2D col = Physics2D.OverlapPoint(new Vector2(x, y));
        return col.gameObject;
    }

    private void SnapBackToOriginalPosition()
    {
        PlaceOnSquare(parentTransform.gameObject.GetComponent<Square>());
    }


}
