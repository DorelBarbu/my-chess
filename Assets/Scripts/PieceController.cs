using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    public Piece piece;
    private Camera cam;
    private bool isDragging;
    private Transform parentTransform;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(Constants.LEFT_MOUSE_BUTTON))
        {
            if(isDragging == true)
            {
                isDragging = false;
                SnapBackToOriginalPosition();
            }
          
        }

    }

    /**
     * Make the piece follow the cursor
     */
    private void MoveToCursor(Vector3 newPosition)
    {
        //Decoupling the piece from the parent square
        transform.parent = null;
        transform.position = newPosition;
    }

    private void SnapBackToOriginalPosition()
    {
        transform.parent = parentTransform;
        transform.localPosition = Vector3.zero;
    }

    private void OnMouseDrag()
    {
        if(isDragging == false)
        {
            parentTransform = transform.parent;
        }
        isDragging = true;
        Vector3 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 1;
        MoveToCursor(newPosition);
    }

    private IEnumerator ChangeSquaresAppearance()
    {
        Square previousSquare = null;
        while(isDragging)
        {
            Vector3 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = 1;
            GameObject overlappingSquare = GetOverlappingSquare(newPosition.x, newPosition.y);
            if(previousSquare)
            {
                previousSquare.ResetSprite();
            }
            Square currentSquare = overlappingSquare.GetComponent<Square>();
            if(currentSquare)
            {
                currentSquare.Highlight();
                previousSquare = currentSquare;
            }
            yield return null;
        }
        if(previousSquare)
        {
            previousSquare.ResetSprite();
        }
    }
           
    
    private GameObject GetOverlappingSquare(float x, float y)
    {
        Collider2D col = Physics2D.OverlapPoint(new Vector2(x, y));
        return col.gameObject;
    }
    
}
