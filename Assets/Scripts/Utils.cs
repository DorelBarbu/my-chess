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
        newPosition.z = 3;
        gameObject.transform.position = newPosition;
    }

    public static Vector2 ConvertToCartesian(char line, char column)
    {
        Vector2 convertedCoordinates = new Vector2();
        convertedCoordinates.x = Constants.TABLE_SIZE - (int)(line-'0');
        convertedCoordinates.y = column - 'A';
        return convertedCoordinates;
    }

    public static string ConvertCartesianToAlgebraic(Vector2 cartesianCoordinates)
    {
        char[] arr = {ConvertColumnToChessNotation((int)cartesianCoordinates.y), ConvertLineToChessNotation((int)cartesianCoordinates.x)};
        return new string(arr);
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

    public static List<Square> GetTrailOfPossibleMovesEnhanced(GameObject pieceObj)
    {
        string piecePosition = pieceObj.GetComponent<Piece>().GetSquare().GetAlgebraicCoordinates();
        //Debug.Log("Getting moves for piece at position: " + piecePosition);
        List<string> nextPossiblePositions = MovesManager.Instance.GetNextPossiblePositionsForPieceAtSquare(piecePosition);

        List<Square> greenSquares = new List<Square>();

        if(nextPossiblePositions != null)
        {
            nextPossiblePositions.ForEach(position => greenSquares.Add(Board.SquareMapping[position]));
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
        char[] charKey = { chessNotationY, chessNotationX };
        return Board.SquareMapping[new string(charKey)];
    }

    public static bool isCheck(ColorsEnum color, GameManager gameManager)
    {
        Square kingSquare = GetKingSquareForPlayer(color, gameManager);
        List<Piece> attackingPieces = gameManager.getPiecesOfColor(Utils.NegateColor(color));
        return IsSquareUnderAttack(attackingPieces, kingSquare);
    }

    public static GameObject CreatePieceGameObject(string name, PieceControllerType type, ColorsEnum color)
    {
        GameObject newGameObject = new GameObject(name);

        newGameObject.AddComponent<Piece>();
        Piece PieceComponent = newGameObject.GetComponent<Piece>();
        PieceComponent.IPiece = PieceFactory.createInstance(type);
        PieceComponent.Type = type;
        PieceComponent.SetColor(color);

        return newGameObject;
    }

    public static GameObject InstantiatePieceAndPlaceOnSquare(string name, PieceControllerType type, ColorsEnum color, string destinationSquare)
    {
        GameObject newPiece = Utils.CreatePieceGameObject(name, type, color);
        Square square = Board.SquareMapping[destinationSquare];

        Utils.PlaceOnObject(newPiece, square.gameObject);

        return newPiece;
    }

    public static string ConverToAlgebraicNotation(int line, int col)
    {
        char algebraicLine = ConvertLineToChessNotation(line);
        char algebraicColumn = ConvertColumnToChessNotation(col);
        char[] arr = { algebraicColumn, algebraicLine };

        return new string(arr);
    }

    public static bool IsPawnInitialPosition(string square)
    {
        SquareConfiguration pawnPiece = BoardConfiguration.Instance.GetPieceAtSquare(square);

        return pawnPiece.MovingDirection == 1 && square[1] != '7' || pawnPiece.MovingDirection == -1 && square[1] != '2';
    }
}
