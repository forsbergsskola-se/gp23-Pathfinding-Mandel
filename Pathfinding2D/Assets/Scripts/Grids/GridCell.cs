using UnityEngine;

namespace Grids
{
    [RequireComponent(typeof(SpriteRenderer))] 
    public class GridCell : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        [SerializeField] private CellType cellType;
        
        public enum CellType
        {
            Ground,
            Wall,
            Water
        }

        public bool Walkable => cellType != CellType.Wall;


        void Start()
        {
            OnValidate();
        }

        public override string ToString()
        {
            return
                $"{nameof(GridCell)} ({Mathf.FloorToInt(transform.position.x)} | {Mathf.FloorToInt(transform.position.y)})";
        }

        private void OnValidate() // Editor only function, very handy
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = Walkable ? Color.white : Color.black;
        }
    }
}