using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    [SerializeField] private bool color; //true - black, false - white
    [SerializeField] private Vector2 coordinates; //position of the piece in chess notation, e.g. A8, B3
    [SerializeField] private bool isOccupied;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("The color of the square is: " + color);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
