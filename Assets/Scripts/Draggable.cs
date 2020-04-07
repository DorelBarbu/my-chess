using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public DraggableType draggableType;
    private Camera cam;
    private bool isDragging;
    private IDraggableHandler draggableHandler;
    [SerializeField] public bool draggingEnabled;

    public bool DraggingEnabled
    {
        get { return draggingEnabled;  }
        set { draggingEnabled = value;  }
    }
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
    public void StopDragging()
    {
        if (isDragging == true)
        {
            isDragging = false;
            draggableHandler.HandleDragFinnish();
        }
    }
    
    public void OnMouseDragg()
    {
        if(DraggingEnabled == true)
        {
            if (isDragging == false)
            {
                draggableHandler.HandleDragStart();
            }
            isDragging = true;
            StartCoroutine("StartUpdating");
        }
      
    }

    private IEnumerator StartUpdating()
    {
        while(isDragging)
        {
            
            Utils.MoveToCursor(gameObject, GetMousePositionForDragging());
            yield return draggableHandler.RunDurringDragging();
        }
    }

    private Vector3 GetMousePositionForDragging()
    {
        Vector3 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 1;
        return newPosition;
    }
}
