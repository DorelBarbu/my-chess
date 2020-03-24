using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public static bool BLACK = true;
    public static bool WHITE = false;
    public static int LEFT_MOUSE_BUTTON = 0;
    public static string QUEEN = "queen";
    public static string SQUARE = "Square";
    public static string UIMANAGER = "UiManager";
    public static int TABLE_SIZE = 8;
    public static Dictionary<PieceControllerType, char> PIECE_MAPPING = new Dictionary<PieceControllerType, char>()
    {
        { PieceControllerType.PAWN, 'P' },
        { PieceControllerType.KING, 'K' },
        { PieceControllerType.QUEEN, 'Q' },
        { PieceControllerType.KNIGHT, 'N' },
        { PieceControllerType.ROOK, 'R' },
        { PieceControllerType.BISHOP, 'B' }
    };

    public static Dictionary<ColorsEnum, bool> COLOR_MAPPING = new Dictionary<ColorsEnum, bool>()
    {
        { ColorsEnum.WHITE, false },
        { ColorsEnum.BLACK, true }
    };
}
