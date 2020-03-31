using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    [SerializeField] private ColorsEnum color; //true - black, false - white
    [SerializeField] private char x;
    [SerializeField] private char y;
    [SerializeField] private bool isOccupied;
    [SerializeField] private bool canMoveTo = false;
    private GameObject highlightSquare;
    private UIManager uiManager;

    public string GetAlgebraicCoordinates()
    {
        char[] arr = { y, x };
        return new string(arr);
    }

    public bool CanMoveTo
    {
        set { canMoveTo = value; }
        get { return canMoveTo; }
    }

    public char X
    {
        get { return x; }
        set { x = value; }
    }

    public char Y
    {
        get { return y; }
        set { y = value; }
    }

    public void OnMouseDown()
    {
        Debug.Log("clicked on " + name);
        Piece currentPiece = GetPiece();
    }

    private void Awake()
    {
        uiManager = GameObject.Find(Constants.UIMANAGER).GetComponent<UIManager>();
    }

    private void ChangeSprite(Sprite newSprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
    }

    public void Highlight()
    {
        ChangeSprite(uiManager.highlightedSquareSprite);
    }

    public void ResetSprite()
    {
        if(canMoveTo == false)
            Destroy(this.highlightSquare);

        if (color == ColorsEnum.BLACK)
        {
            ChangeSprite(uiManager.blackSquareSprite);
        }
        else
        {
            ChangeSprite(uiManager.whiteSquareSprite);
        }
    }

    private void CreateHighlightSquare()
    {
        GameObject highlightSquare = new GameObject("Square");
        highlightSquare.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = highlightSquare.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = uiManager.candidateSquareSprite;
        spriteRenderer.sortingOrder = 3;
        Utils.PlaceOnObject(highlightSquare, gameObject);
        this.highlightSquare = highlightSquare;
    }

    public void MarkAsAvailableForMove()
    {
        CreateHighlightSquare();
        canMoveTo = true;
    }

    public void SetOccupied(bool occupiedValue)
    {
        isOccupied = occupiedValue;
    }

    public bool GetOccupied()
    {
        return isOccupied;
    }

    public Piece GetPiece()
    {
        Piece piece = null;
        if(transform.childCount > 0)
        {
            piece = transform.GetChild(0).gameObject.GetComponent<Piece>();
        }
        return piece;
    }

    public bool IsAvailableForMove(ColorsEnum pieceColor)
    {
        return GetPiece() == null || GetPiece().GetColor() != pieceColor;
    }
}
