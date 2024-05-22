using UnityEngine;

namespace Grids
{
    [RequireComponent(typeof(SpriteRenderer))] 
    public class GridCell : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public bool isWalkable;

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
            spriteRenderer.color = isWalkable ? Color.white : Color.black;
        }
    }
}