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
            int x = Mathf.FloorToInt(pos.x + 0.5f);
            int y = Mathf.FloorToInt(pos.y + 0.5f);
            
            return walkableGrid[x + y * width];
        }
    }
}

