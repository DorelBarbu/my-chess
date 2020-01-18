using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public DraggableType draggableType;
    public Piece piece;
    private Camera cam;
    private bool isDragging;
    private Transform parentTransform;
    private IDraggableHandler draggableHandler;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        draggableHandler = DraggableHandlerFactory.CreateInstance(draggableType, gameObject);
    }

    public bool IsDragging()
    {
        return isDragging;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(Constants.LEFT_MOUSE_BUTTON))
        {
            // Stop the dragging
            if(isDragging == true)
            {
                isDragging = false;
                draggableHandler.HandleDragFinnish();
            } 
        }
    }
    private void OnMouseDrag()
    {
        if(isDragging == false)
        {
            parentTransform = transform.parent;
            draggableHandler.HandleDragStart();
        }
        isDragging = true;
        Vector3 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 1;
        Utils.MoveToCursor(gameObject, newPosition);
        StartCoroutine("StartUpdating");
    }

    private IEnumerator StartUpdating()
    {
        while(isDragging)
        {
            yield return draggableHandler.RunDurringDragging();
        }

    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected");
    }
}
