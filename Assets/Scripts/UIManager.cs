using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Sprite highlightedSquareSprite;
    public Sprite blackSquareSprite;
    public Sprite whiteSquareSprite;
    public Sprite candidateSquareSprite;

    private GameObject movingPiece = null;


    public static void DrawTrailOfPossibleMoves(GameObject pieceObj)
    {
        Board.SetGreenSquares(Utils.GetTrailOfPossibleMovesEnhanced(pieceObj));
    }

    void Update()
    {
        //Start dragging
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mouseCoordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] hits = Physics2D.OverlapPointAll(new Vector2(mouseCoordinates.x, mouseCoordinates.y));
            foreach(Collider2D hit in hits)
            {
                if(hit.gameObject.tag != "Square")
                {
                    hit.gameObject.GetComponent<Draggable>().OnMouseDragg();
                    movingPiece = hit.gameObject;
                }
            }
        }

        //Stop dragging
        if (Input.GetMouseButtonUp(0))
        {
            if(movingPiece != null)
            {
                movingPiece.gameObject.GetComponent<Draggable>().StopDragging();
                movingPiece = null;
            }
        }
        
    }

    private IEnumerator Move(GameObject obj)
    {
        while(movingPiece != null)
        {
            Utils.MoveToCursor(obj, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            yield return null;
        }
        
    }
}
