using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Grid))]
    public class GridInspector : UnityEditor.Editor
    {
        private UnityEngine.Object cellPrefab;
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            cellPrefab = EditorGUILayout.ObjectField("Cell Prefab", cellPrefab, typeof(GridCell));
            EditorGUI.BeginDisabledGroup(cellPrefab == null);
            if (GUILayout.Button("Generate Grid"))
            {
                Grid grid = target as Grid;

                
                foreach (var t in grid.walkableGrid)
                {
                    if (grid.walkableGrid != null)
                    {
                        //DestroyImmediate(grid.walkableGrid);
                    }
                }

                var i = 0;
                for (var x = 0; x < grid.width; x++)
                {
                    for (var y = 0; y < grid.walkableGrid.Length / grid.width; y++)
                    {
                        GridCell gridElement = (GridCell)PrefabUtility.InstantiatePrefab(cellPrefab);
                        gridElement.transform.position = new Vector3(x, y, 0);
                        grid.walkableGrid[i++] = gridElement;
                    }
                }
                
                
                EditorUtility.SetDirty(grid); // do this so the change can be saved
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}
