using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareConfiguration
{
    public char Piece { get; set; }
    public bool Color { get; set; } //false - white, true - black

    public SquareConfiguration(char piece, bool color)
    {
        Piece = piece;
        Color = color;
    }

    public SquareConfiguration() {}
}
