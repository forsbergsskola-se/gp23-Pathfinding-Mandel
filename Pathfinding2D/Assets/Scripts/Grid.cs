using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private bool[] walkableGrid = new bool[100];
    public int width;

    public bool IsWalkable(int x, int y)
    {
        return walkableGrid[y * width + x]; // find the right index in the array.
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
