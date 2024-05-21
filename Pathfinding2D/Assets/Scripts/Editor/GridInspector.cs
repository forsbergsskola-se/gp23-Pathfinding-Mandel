using Unity.VisualScripting;
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
                Undo.IncrementCurrentGroup(); // mark changes to be one undo
                Undo.RecordObject(grid, "Update Grid References"); // undo takes you back here
                foreach (var cell in grid.GetComponentsInChildren<GridCell>())
                {
                    if(cell != null)
                        Undo.DestroyObjectImmediate(cell.gameObject); // Undo manager keeps track of the object
                }

                var i = 0;
                int height = grid.walkableGrid.Length / grid.width;
                for (var y = 0; y < height; y++)
                {
                    for (var x = 0; x < grid.width; x++)
                    {
                        GridCell gridElement = PrefabUtility.InstantiatePrefab(cellPrefab, grid.transform) as GridCell;
                        gridElement.transform.position = new Vector3(x, y, 0);
                        grid.walkableGrid[i++] = gridElement;
                        Undo.RegisterCreatedObjectUndo(gridElement.gameObject, "Create Grid Cell");
                    }    
                }
                Undo.SetCurrentGroupName("Generate Grid Cells");
                EditorUtility.SetDirty(grid); // do this so the change can be saved
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}
