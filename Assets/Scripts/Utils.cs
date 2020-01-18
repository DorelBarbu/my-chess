using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static GameObject GetObjectBelow(float x, float y)
    {
        Collider2D col = Physics2D.OverlapPoint(new Vector2(x, y));
        return col != null ? col.gameObject : null;
    }

    public static void PlaceOnObject(GameObject source, GameObject target)
    {
        source.transform.parent = target.transform;
        source.transform.localPosition = Vector3.zero;
    }

    public static void MoveToCursor(GameObject gameObject, Vector3 newPosition)
    {
        //Decoupling the piece from the parent square
        gameObject.transform.parent = null;
        gameObject.transform.position = newPosition;
    }
}
