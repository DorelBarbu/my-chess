using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceDraggableHandler : MonoBehaviour, IDraggableHandler
{
    private Camera cam;
    private Square previousSquare;

    private void Awake()
    {
        cam = Camera.main;
        previousSquare = null;
    }

    public void HandleDragStart() {}

    public IEnumerator RunDurringDragging()
    {
        Vector3 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 1;
        GameObject overlappingSquare = GetOverlappingSquare(newPosition.x, newPosition.y);
        if (previousSquare)
        {
            previousSquare.ResetSprite();
        }
        Square currentSquare = overlappingSquare.GetComponent<Square>();
        if (currentSquare)
        {
            currentSquare.Highlight();
            previousSquare = currentSquare;
        }

        yield return null;
    }

    private void PlaceOnSquare(Square square)
    {
        transform.parent = square.transform;
        transform.localPosition = Vector3.zero;
    }


    private GameObject GetOverlappingSquare(float x, float y)
    {
        Collider2D col = Physics2D.OverlapPoint(new Vector2(x, y));
        return col.gameObject;
    }

    public void HandleDragFinnish()
    {
        if (previousSquare)
        {
            previousSquare.ResetSprite();
            PlaceOnSquare(previousSquare);
        }
    }
}
