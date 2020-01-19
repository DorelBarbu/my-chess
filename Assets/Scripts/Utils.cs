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

    public static Vector2 ConvertToCartesian(char line, char column)
    {
        Vector2 convertedCoordinates = new Vector2();
        convertedCoordinates.x = Constants.TABLE_SIZE - (int)(line-'0');
        convertedCoordinates.y = column - 'A';
        return convertedCoordinates;
    }

    public static char ConvertLineToChessNotation(int line)
    {
        return (char)('0' + Constants.TABLE_SIZE - line);
    }

    public static char ConvertColumnToChessNotation(int column)
    {
        return (char)('A' + column);
    }

    public static bool IsInsideBoard(int line, int column)
    {
        return 0 <= line && line < Constants.TABLE_SIZE && 0 <= column && column < Constants.TABLE_SIZE;
    }
}
