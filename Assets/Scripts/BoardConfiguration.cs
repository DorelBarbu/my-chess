using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardConfiguration
{
    static Dictionary<string, SquareConfiguration> Config { get; set; }
    private static BoardConfiguration boardConfigurationInstance;

    public static BoardConfiguration Instance
    {
        get
        {
            if(boardConfigurationInstance == null)
            {
                boardConfigurationInstance = new BoardConfiguration();
            }
            return boardConfigurationInstance;
        }
    }

    private BoardConfiguration()
    {
        Config = null;
        for(char i = 'A'; i <= 'H'; i++)
        {
            for(int j = 1; j <= 8; j++)
            {
                char[] keyChar = { i, (char)('0' + j) };
                Config[new string(keyChar)] = new SquareConfiguration();
            }
        }
    }

    public void SetPiecePosition(char piece, bool color, string square)
    {
        Config[square].Piece = piece;
        Config[square].Color = color;
    }

    public SquareConfiguration GetPiecePosition(string square)
    {
        return Config[square];
    }

    public void MovePiece(string currentSquare, string nextSquare)
    {
        Config[nextSquare] = Config[currentSquare];
        Config[currentSquare] = new SquareConfiguration();
    }

}
