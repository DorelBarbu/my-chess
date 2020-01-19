using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceDraggableHandler : MonoBehaviour, IDraggableHandler
{
    private Camera cam;
    private Square previousSquare;
    private Transform parentTransform;

    private void Awake()
    {
        cam = Camera.main;
    }

    public void HandleDragStart()
    {
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
            if(previousSquare.CanMoveTo == true)
            {
                Utils.PlaceOnObject(gameObject, previousSquare.gameObject);
            }
            else
            {
                Debug.Log("Cant move here");
                transform.parent = parentTransform;
                transform.localPosition = Vector3.zero;
            }
        }
    }
}
