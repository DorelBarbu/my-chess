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

    public static void ToggleDraggableForPieces(Piece[] pieces)
    {
        foreach (Piece piece in pieces)
        {
            piece.GetComponent<Draggable>().DraggingEnabled = !piece.GetComponent<Draggable>().DraggingEnabled;
        }
    }

    public static bool IsSquareUnderAttack(List<Piece> pieces, Square square)
    {
        List<Square> squaresUnderAttack = new List<Square>();
        foreach(Piece piece in pieces)
        {
            squaresUnderAttack.AddRange(GetTrailOfPossibleMoves(piece.gameObject));
        }

        return squaresUnderAttack.Find(currentSquare => currentSquare.X == square.X && currentSquare.Y == square.Y) != null;

    }
    public static List<Square> GetTrailOfPossibleMoves(GameObject pieceObj)
    {
        Piece currentPiece = pieceObj.GetComponent<Piece>();
        List<List<Vector2>> allowedMovesDeltas = currentPiece.GetAllowedMovesDeltas();
        Vector2 coordinates = currentPiece.GetCoordinates();

        List<Square> greenSquares = new List<Square>();

        foreach (List<Vector2> direction in allowedMovesDeltas)
        {
            foreach (Vector2 v in direction)
            {
                int nextX = (int)coordinates.x + (int)v.x;
                int nextY = (int)coordinates.y + (int)v.y;

                if (IsInsideBoard(nextX, nextY))
                {
                    char nextXChessNotation = ConvertLineToChessNotation(nextX);
                    char nextYChessNotation = ConvertColumnToChessNotation(nextY);

                    Square square = Board.GetSquareAtPosition(nextXChessNotation, nextYChessNotation);

                    if (square.IsAvailableForMove(currentPiece.GetColor()))
                    {
                        greenSquares.Add(square);
                    }

                    if (square.GetPiece() || square.GetOccupied() == true)
                    {
                        break;
                    }
                }

            }
        }

        return greenSquares;
    }

    public static ColorsEnum NegateColor(ColorsEnum color)
    {
        return color == ColorsEnum.BLACK ? ColorsEnum.WHITE : ColorsEnum.BLACK;
    }

    public static Square GetKingSquareForPlayer(ColorsEnum playerColor, GameManager gameManager)
    {
        Piece king = gameManager.getPiecesOfColor(playerColor).Find(piece => piece.Type == PieceControllerType.KING);
        char chessNotationX = ConvertLineToChessNotation((int)king.coordinates.x);
        char chessNotationY = ConvertColumnToChessNotation((int)king.coordinates.y);
        char[] charKey = { chessNotationX, chessNotationY };
        return Board.SquareMapping[new string(charKey)];
    }

    public static bool isCheck(ColorsEnum color, GameManager gameManager)
    {
        Square kingSquare = GetKingSquareForPlayer(color, gameManager);
        List<Piece> attackingPieces = gameManager.getPiecesOfColor(Utils.NegateColor(color));
        return IsSquareUnderAttack(attackingPieces, kingSquare);
    }

    public static bool isCheckMate(ColorsEnum color, GameManager gameManager)
    {
        Square kingSquare = GetKingSquareForPlayer(color, gameManager);
        Piece king = kingSquare.GetPiece();
        List<Square> kingMoves = GetTrailOfPossibleMoves(king.gameObject);

        List<Piece> attackingPieces = gameManager.getPiecesOfColor(Utils.NegateColor(color));
       
        bool isCheckMate = true;
        foreach(Square square in kingMoves)
        {
            if(IsSquareUnderAttack(attackingPieces, square) == false)
            {
                isCheckMate = false;
                break;
            }
        }

        return isCheckMate;
    }
}
