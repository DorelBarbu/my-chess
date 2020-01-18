using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDraggableHandler
{
    void HandleDragStart();
    void HandleDragFinnish();
    IEnumerator RunDurringDragging();
}
