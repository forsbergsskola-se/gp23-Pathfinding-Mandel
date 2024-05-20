using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Grid))]
    public class GridInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Generate Grid"))
            {
                Grid grid = target as Grid;
                
                
                
                EditorUtility.SetDirty(grid); // do this so the change can be saved
            }
        }
    }
}
