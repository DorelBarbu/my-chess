using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPiece
{
    List<List<Vector2>> GetAllowedMoves();
}
