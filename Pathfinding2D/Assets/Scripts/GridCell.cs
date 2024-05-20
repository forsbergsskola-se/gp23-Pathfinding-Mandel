using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))] 
public class GridCell : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public bool isWalkable;

    void Start()
    {
        OnValidate();
    }

    private void OnValidate() // Editor only function, very handy
    {
        spriteRenderer.color = isWalkable ? Color.white : Color.black;
    }
}