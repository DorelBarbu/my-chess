using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private Dictionary<string, Square> SquareMapping = new Dictionary<string, Square>();
    private void Awake()
    {
        MapSquares();
    }

    private void MapSquares()
    {
        Square[] squares = FindObjectsOfType<Square>();
        foreach(Square square in squares)
        {
            SquareMapping.Add(square.gameObject.name, square);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
