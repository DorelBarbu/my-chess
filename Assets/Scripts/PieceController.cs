using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    public Piece piece;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /**
     * Make the piece follow the cursor
     */
    private void MoveToCursor()
    {
        //Decoupling the piece from the parent square
        transform.parent = null;
        Vector3 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;
        transform.position = newPosition;
    }

    private void OnMouseDrag()
    {
        MoveToCursor();
    }
}
