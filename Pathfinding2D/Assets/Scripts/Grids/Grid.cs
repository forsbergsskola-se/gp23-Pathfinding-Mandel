using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GridCell[] walkableGrid = new GridCell[100];
    public int width = 10;

    public bool IsWalkable(int x, int y)
    {
        return walkableGrid[y * width + x].isWalkable; // find the right index in the array.
    }

    public GridCell GetCellForPosition(Vector3 pos)
    {
        return walkableGrid[(int)pos.y * width + (int)pos.x];
    }
}

