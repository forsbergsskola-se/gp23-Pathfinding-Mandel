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
        if (Input.GetKeyDown(KeyCode.D))
        {
            Grid grid = FindObjectOfType<Grid>();
            GridCell start = grid.GetCellForPosition(this.transform.position);
            GridCell end = grid.GetCellForPosition(gold.transform.position);
            var path = FindPath_DepthFirst(grid, start, end);
            foreach (var node in path)
            {
                node.spriteRenderer.color = Color.green;
            }
            StartCoroutine(Co_WalkPath(path));
        }
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            Grid grid = FindObjectOfType<Grid>();
            GridCell start = grid.GetCellForPosition(this.transform.position);
            GridCell end = grid.GetCellForPosition(gold.transform.position);
            var path = FindPath_BreadthFirst(grid, start, end);
            foreach (var node in path)
            {
                node.spriteRenderer.color = Color.yellow;
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

    private static IEnumerable<GridCell> FindPath_DepthFirst(Grid grid, GridCell start, GridCell end)
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
    
    private static IEnumerable<GridCell> FindPath_BreadthFirst(Grid grid, GridCell start, GridCell end)
    {
        Queue<GridCell> todo = new();
        HashSet<GridCell> visited = new();
        Dictionary<GridCell, GridCell> previous = new(); // <Key, Value>
        todo.Enqueue(start);
        visited.Add(start);
        
        while (todo.Count > 0)
        {
            var current = todo.Dequeue();
            foreach (var neighbour in grid.GetWalkableNeighbourForCell(current))
            {
                if(visited.Contains(neighbour)) continue;
                todo.Enqueue(neighbour);
                previous[neighbour] = current;
                visited.Add(neighbour);
                neighbour.spriteRenderer.color = Color.blue;
                if (neighbour == end) return TracePath(neighbour, previous).Reverse();
            }
        }
        //return null;
        throw new Exception("Path not found, returned null.");
    }

    private static IEnumerable<GridCell> TracePath(GridCell neighbour, Dictionary<GridCell, GridCell> previous)
    {
        while (true)
        {
            yield return neighbour;
            if(!previous.TryGetValue(neighbour, out neighbour))
                yield break;
        }
    }
}
