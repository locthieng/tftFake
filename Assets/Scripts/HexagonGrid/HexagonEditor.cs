using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.TerrainTools;
using UnityEngine;
[CustomEditor(typeof(HexagonGridController))]
public class HexagonEditor : Editor
{
    private HexagonGridController hexController;

    private void OnSceneGUI(SceneView sceneView)
    {
        if (hexController == null)
        {
            return;
        }
        if (Application.isPlaying) return;
    }
    private void OnEnable()
    {
        hexController = (HexagonGridController)target;
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        hexController = (HexagonGridController)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Generate Hex Grid"))
        {
            hexController.GenerateGrid();
        }

        if (GUILayout.Button("Clear Hex Grid"))
        {
            hexController.ClearGrid();
        }

        GUILayout.Space(10);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(hexController);
            if (!Application.isPlaying)
            {
                EditorSceneManager.MarkSceneDirty(hexController.gameObject.scene);
            }
        }
    }
}
