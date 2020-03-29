using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareConfiguration
{
    private bool movingDirection;
    public char Piece { get; set; }
    public bool Color { get; set; } //false - white, true - black

    public int MovingDirection { get; set; }

    public SquareConfiguration(char piece, bool color)
    {
        Piece = piece;
        Color = color;
    }

    public SquareConfiguration(char piece, bool color, int movingDirection)
    {
        Piece = piece;
        Color = color;
        MovingDirection = movingDirection;
    }

    public SquareConfiguration() {}
}
