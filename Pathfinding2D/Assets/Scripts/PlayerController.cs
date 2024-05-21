using System;
using System.Collections.Generic;
using Grids;
using UnityEngine;
using Grid = Grids.Grid;

public class PlayerController : MonoBehaviour
{
    public GameObject gold;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Grid grid = FindObjectOfType<Grid>();
            GridCell start = grid.GetCellForPosition(this.transform.position);
            GridCell end = grid.GetCellForPosition(gold.transform.position);
            var path = FindPath(grid, start, end);
            // start courotine
            // traverse the path
        }
    }

    private static IEnumerable<GridCell> FindPath(Grid grid, GridCell start, GridCell end)
    {
        Stack<GridCell> path = new Stack<GridCell>();
        HashSet<GridCell> visited = new HashSet<GridCell>();
        path.Push(start);
        visited.Add(start);

        while (path.Count > 0)
        {
            var foundNextNode = false;
            foreach (var neighbour in grid.GetWalkableNeighbourForCell(path.Peek()))
            {
                if(visited.Contains(neighbour)) continue;
                path.Push(neighbour);
                if (neighbour == end) return path;
                foundNextNode = true;
                break;
            }
            if (!foundNextNode)
                path.Pop();
        }
        return null;
    }
}
