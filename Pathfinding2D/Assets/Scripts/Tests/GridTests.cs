using Grids;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using Grid = Grids.Grid;

namespace Tests
{
    public class GridTests
    {
        private Grid _grid;

        [SetUp]
        public void SetUp()
        {
            var gridGameObject = new GameObject("Grid");
            _grid = gridGameObject.AddComponent<Grid>();
            _grid.width = 3;
            _grid.walkableGrid = new GridCell[9];
            int height = 3;
            int i = 0;
            
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < _grid.width; x++)
                {
                    var cellGameObject = new GameObject("GridCell");
                    cellGameObject.transform.SetParent(_grid.transform);
                    cellGameObject.transform.position = new Vector3(x, y, 0);
                    cellGameObject.AddComponent<SpriteRenderer>();
                    var cell = cellGameObject.AddComponent<GridCell>();
                    _grid.walkableGrid[i++] = cell;
                }
            }
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.DestroyImmediate(_grid);
            _grid = null;
        }
        [Test]
        public void GridTestsSimplePasses()
        {
            
            
        }
        
        
        
        
        
        
        

        // A UnityTest behaves like a coroutine in PlayMode
        // and allows you to yield null to skip a frame in EditMode
        [UnityEngine.TestTools.UnityTest]
        public System.Collections.IEnumerator GridTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // yield to skip a frame
            yield return null;
        }
    }
}