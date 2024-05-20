using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))] 
public class GridCell : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public bool isWalkable;

    void Start()
    {
        OnValidate();
    }

    private void OnValidate() // Editor only function, very handy
    {
        spriteRenderer.color = isWalkable ? Color.white : Color.black;
    }
}

public class Grid : MonoBehaviour
{
    public GridCell[] walkableGrid = new GridCell[100];
    public int width = 10;

    public bool IsWalkable(int x, int y)
    {
        return walkableGrid[y * width + x].isWalkable; // find the right index in the array.
    }
    
    
}
