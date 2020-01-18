using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableHandlerFactory
{
    public static IDraggableHandler CreateInstance(DraggableType draggableHandlerType, GameObject gameObject)
    {
        IDraggableHandler draggableHandler;
        switch(draggableHandlerType)
        {
            case DraggableType.PIECE:
                draggableHandler = gameObject.GetComponent<PieceDraggableHandler>();
                break;
            default:
                draggableHandler = null;
                break;

        }
        return draggableHandler;
    }
}
