using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    [SerializeField] private ColorsEnum color; //true - black, false - white
    [SerializeField] char x;
    [SerializeField] char y;
    [SerializeField] private bool isOccupied;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeSprite(Sprite newSprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
    }

    public void Highlight()
    {
        UIManager uIManager = GameObject.Find(Constants.UIMANAGER).GetComponent<UIManager>();
        ChangeSprite(uIManager.highlightedSquareSprite);
    }

    public void ResetSprite()
    {
        if(color == ColorsEnum.BLACK)
        {
            ChangeSprite(GameObject.Find(Constants.UIMANAGER).GetComponent<UIManager>().blackSquareSprite);
        }
        else
        {
            ChangeSprite(GameObject.Find(Constants.UIMANAGER).GetComponent<UIManager>().whiteSquareSprite);
        }
    }
}
