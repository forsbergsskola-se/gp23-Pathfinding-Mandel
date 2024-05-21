using System;
using System.Collections;
using System.Collections.Generic;
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
        var maxX = width - 1;
        var maxY = walkableGrid.Length / width - 1;

        if (pos.x < 0 -0.5 || pos.x > maxX +0.5 || pos.y < 0-0.5 || pos.x > maxY +0.5)
        {
            throw new PositionOutsideOfGridExeption();
        }

        int i = 0;
        var gridPos = new Vector3(0, 0, -1);
        for (var y = 0; y < maxY; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if (pos.x > gridPos.x -0.5 && pos.x <= gridPos.x + 0.5)
                {
                    if (pos.y > gridPos.y -0.5 && pos.y <= gridPos.y + 0.5)
                    {
                        return walkableGrid[i];
                    }
                }
                i++;
            }
        }

        return null;
    }
}

public class PositionOutsideOfGridExeption : Exception
{
    public override string Message => "The position is outside of the grid!";
}
