using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardConfiguration
{
    public static Dictionary<string, SquareConfiguration> Config { get; set; }
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
        InitBoardConfigurationWithNull();
    }

    public void SetPiecePosition(char piece, bool color, string square)
    {
        SquareConfiguration newSquareConfiguration = new SquareConfiguration(piece, color);
        Config[square] = newSquareConfiguration;
    }

    public SquareConfiguration GetPieceAtSquare(string square)
    {
        //if (!Config.ContainsKey(square)) Config[square] = null;
        return Config[square];
    }

    public void MovePiece(string currentSquare, string nextSquare)
    {
        Config[nextSquare] = Config[currentSquare];
        Config[currentSquare] = null;
    }

    public string GetPiecePosition(char piece, bool color)
    {
        Dictionary<string, SquareConfiguration>.KeyCollection configKeys = Config.Keys;
        string answer = "";

        foreach (string square in configKeys)
        {
            if(Config[square] != null && Config[square].Piece == piece && Config[square].Color == color)
            {
                answer = square;
                break;
            }
        }

        return answer;
    }

    public void ResetBoardConfiguration()
    {
        InitBoardConfigurationWithNull();
    }

    private void InitBoardConfigurationWithNull()
    {
        Config = new Dictionary<string, SquareConfiguration>();

        for (char i = 'A'; i <= 'H'; i++)
        {
            for (int j = 1; j <= 8; j++)
            {
                char[] keyChar = { i, (char)('0' + j) };
                Config[new string(keyChar)] = null;
            }
        }
    }

}
