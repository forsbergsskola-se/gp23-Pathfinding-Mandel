using System.Collections.Generic;
using UnityEngine;

namespace Grids
{
    public class Grid : MonoBehaviour
    {
        public GridCell[] walkableGrid = new GridCell[100];
        public int width = 10;

        public int Height => walkableGrid.Length / width;

        public bool IsWalkable(int x, int y)
        {
            return walkableGrid[y * width + x].isWalkable; // find the right index in the array.
        }

        public GridCell GetCellForPosition(Vector3 pos)
        {
            var index = GetCellIndexForPosition(pos);
            
            return walkableGrid[index.x + index.y * width];
        }

        private static Vector2Int GetCellIndexForPosition(Vector3 pos)
        {
            return new Vector2Int(Mathf.FloorToInt(pos.x + 0.5f), Mathf.FloorToInt(pos.y + 0.5f));
        }

        bool IsValidAndWalkable(Vector2Int index)
        {
            if (index.x < 0) return false;
            if (index.x >= width) return false;
            if (index.y < 0) return false;
            if (index.y >= Height) return false;
            return IsWalkable(index.x, index.y);
        }

        public GridCell GetCellForIndex(Vector2Int index)
        {
            return walkableGrid[index.y * width + index.x];
        }

        public IEnumerable<Vector2Int> GetWalkDirection()
        {
            yield return Vector2Int.up;
            yield return Vector2Int.right;
            yield return Vector2Int.down;
            yield return Vector2Int.left;
        }
        
        public IEnumerable<GridCell> GetWalkableNeighbourForCell(GridCell cell)
        {
            var cellIndex = GetCellIndexForPosition(cell.transform.position);

            foreach (var direction in GetWalkDirection())
            {
                if (IsValidAndWalkable(cellIndex + direction))
                    yield return GetCellForIndex(cellIndex + direction);
            }
        }
    }
}

