using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AI;
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class HexagonGridController : Singleton<HexagonGridController>
{
    public GameObject hexPrefab;

    public int width = 7;
    public int height = 7;
    public float size = 1f;

    float HexWidth => Mathf.Sqrt(3f) * size;
    float HexHeight => 2f * size;
    float Horiz => HexWidth;
    float Vert => 0.75f * HexHeight;


#if UNITY_EDITOR

    public void GenerateGrid()
    {
        ClearGrid();

        for (int r = 0; r < height; r++)
        {
            for (int q = 0; q < width; q++)
            {
                Vector3 pos = GetHexPosition(q, r);

                // Instantiate as a prefab instance in the scene so it retains prefab connection.
                var obj = PrefabUtility.InstantiatePrefab(hexPrefab, transform) as GameObject;
                if (obj == null)
                {
                    // Fallback to plain Instantiate if PrefabUtility fails for some reason.
                    obj = Object.Instantiate(hexPrefab, transform);
                }

                obj.transform.position = pos;

                // Support Undo in the editor.
                Undo.RegisterCreatedObjectUndo(obj, "Create Hex Cell");
            }
        }

        // Mark scene dirty so the user is prompted to save changes.
        EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
    }

    public void ClearGrid()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    Vector3 GetHexPosition(int q, int r)
    {
        float x = q * Horiz;
        float z = r * Vert;

        if (r % 2 == 0)
            x += Horiz / 2f;

        return transform.position + new Vector3(x, 0, z);
    }
#endif
}