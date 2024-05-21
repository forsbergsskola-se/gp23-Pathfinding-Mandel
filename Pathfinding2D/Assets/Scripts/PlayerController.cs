using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            foreach (var node in path)
            {
                node.spriteRenderer.color = Color.green;
            }
            StartCoroutine(Co_WalkPath(path));
        }
    }

    IEnumerator Co_WalkPath(IEnumerable<GridCell> path)
    {
        foreach (var cell in path)
        {
            while (Vector2.Distance(transform.position, cell.transform.position) > 0.001f)
            {
                Vector3 targetPosition = Vector2.MoveTowards(transform.position, cell.transform.position, Time.deltaTime);
                targetPosition.z = transform.position.z;
                transform.position = targetPosition;
                yield return null;
            }
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
                visited.Add(neighbour);
                neighbour.spriteRenderer.color = Color.blue;
                if (neighbour == end) return path.Reverse();
                foundNextNode = true;
                break;
            }
            if (!foundNextNode)
                path.Pop();
        }
        return null;
    }
}
