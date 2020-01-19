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
    private UIManager uiManager;

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
        if(canMoveTo == true)
        {
            MarkAsAvailableForMove();
        }
        else
        {
            if (color == ColorsEnum.BLACK)
            {
                ChangeSprite(uiManager.blackSquareSprite);
            }
            else
            {
                ChangeSprite(uiManager.whiteSquareSprite);
            }
        }
    }

    public void MarkAsAvailableForMove()
    {
        ChangeSprite(uiManager.candidateSquareSprite);
    }
}
