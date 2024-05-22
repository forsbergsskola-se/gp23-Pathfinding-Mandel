using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Collections;
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
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            Grid grid = FindObjectOfType<Grid>();
            GridCell start = grid.GetCellForPosition(this.transform.position);
            GridCell end = grid.GetCellForPosition(gold.transform.position);
            var path = FindPath_Dijkstra(grid, start, end);
            foreach (var node in path)
            {
                node.spriteRenderer.color = Color.magenta;
            }
            StartCoroutine(Co_WalkPath(path));
        }
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            Grid grid = FindObjectOfType<Grid>();
            GridCell start = grid.GetCellForPosition(this.transform.position);
            GridCell end = grid.GetCellForPosition(gold.transform.position);
            var path = FindPath_BestFirst(grid, start, end);
            foreach (var node in path)
            {
                node.spriteRenderer.color = Color.cyan;
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
        Stack<GridCell> path = new();
        HashSet<GridCell> visited = new();
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
                neighbour.spriteRenderer.color = Color.cyan;
                if (neighbour == end) return TracePath(neighbour, previous).Reverse();
            }
        }
        return null;
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
    
    private static IEnumerable<GridCell> FindPath_Dijkstra(Grid grid, GridCell start, GridCell end)
    {
        PriorityQueue<GridCell> todo = new();
        todo.Enqueue(start, 0);
        Dictionary<GridCell, int> costs = new()
        {
            [start] = 0
        };
        Dictionary<GridCell, GridCell> previous = new();
        
        while (todo.Count > 0)
        {
            var current = todo.Dequeue();
            if (current == end) return TracePath(current, previous).Reverse();
            
            foreach (var neighbour in grid.GetWalkableNeighbourForCell(current))
            {
                var newNeighbourCosts = costs[current] + neighbour.Costs; // calculate new path cost
                if(costs.TryGetValue(neighbour, out int neighbourCost) && // check if node has cost
                   neighbourCost <= newNeighbourCosts) continue; // and if the cost is more efficient
                
                todo.Enqueue(neighbour, newNeighbourCosts);
                previous[neighbour] = current;
                costs[neighbour] = newNeighbourCosts;
                neighbour.spriteRenderer.ShiftBrightness(0.4f);
            }
        }
        return null;
    }
    
    static int GetEstimatedCosts(GridCell from, GridCell to)
    {
        return Mathf.RoundToInt(Vector3.Distance(from.transform.position, to.transform.position));
    }
    
    static IEnumerable<GridCell> FindPath_BestFirst(Grid grid, GridCell start, GridCell end)
    {
        PriorityQueue<GridCell> todo = new();
        todo.Enqueue(start, 0);
        Dictionary<GridCell, int> costs = new();
        costs[start] = GetEstimatedCosts(start, end); // START = Estimated Distance Costs
        Dictionary<GridCell, GridCell> previous = new();

        while (todo.Count > 0)
        {
            var current = todo.Dequeue();
            if (current == end)
                return TracePath(current, previous).Reverse();
            
            foreach (var neighbor in grid.GetWalkableNeighbourForCell(current))
            {
                var newNeighborCosts = costs[current] + neighbor.Costs;
                if (costs.TryGetValue(neighbor, out int neighborCosts) &&
                    neighborCosts <= newNeighborCosts) continue;
                                                                 
                todo.Enqueue(neighbor, GetEstimatedCosts(neighbor, end)); // Get Estimate
                previous[neighbor] = current;
                costs[neighbor] = newNeighborCosts;
                
                neighbor.spriteRenderer.ShiftBrightness(0.4f);
            }
        }
        return null;
    }
    
    
}

// TODO add Different actors all walking at the same time

public static class SpriteRendererExtensions
{
    public static void ShiftHue(this SpriteRenderer spriteRenderer, float hue)
    {
        Color.RGBToHSV(spriteRenderer.color, out var h, out var s, out var v);
        spriteRenderer.color = Color.HSVToRGB((h + hue)%1, s, v);
    }
    
    public static void ShiftBrightness(this SpriteRenderer spriteRenderer, float brightness)
    {
        Color.RGBToHSV(spriteRenderer.color, out var h, out var s, out var v);
        if (v > 0.5f)
        {
            spriteRenderer.color = Color.HSVToRGB(h, s, (v-brightness)%1f);
        }
        else
        {
            spriteRenderer.color = Color.HSVToRGB(h, s, (v+brightness)%1f);
        }
    }
}