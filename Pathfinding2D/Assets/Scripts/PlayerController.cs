using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Gold;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Grid grid = FindObjectOfType<Grid>();
            GridCell start = grid.GetCellForPosition(this.transform.position);
            GridCell end = grid.GetCellForPosition(Gold.transform.position);
            var path = FindPath(grid, start, end);
            // start courotine
            // traverse the path
        }
    }

    static IEnumerable<GridCell> FindPath(Grid grid, GridCell start, GridCell end)
    {
        // Track visited cells
        // track sells that need to be visited
        // track the goal
        // decide what cells to visit next
        
        // if current == goal
        // reconstruct path

        throw new NotImplementedException();

    }
}
