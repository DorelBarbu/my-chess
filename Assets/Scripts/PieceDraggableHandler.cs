using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceDraggableHandler : MonoBehaviour, IDraggableHandler
{
    public void HandleDragStart()
    {
        Debug.Log("Drag start handled in PieceDraggableHandler");
    }

    private IEnumerator ChangeSquaresAppearance()
    {
        Square previousSquare = null;
        while (isDragging)
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

        if (previousSquare)
        {
            previousSquare.ResetSprite();
            PlaceOnSquare(previousSquare);
        }
    }
}
