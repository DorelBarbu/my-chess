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
    }

    public void HandleDragStart()
    {
        previousSquare = null;
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
            Utils.PlaceOnObject(gameObject, previousSquare.gameObject);
        }
    }
}
