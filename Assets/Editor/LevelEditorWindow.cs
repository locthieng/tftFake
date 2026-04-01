using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class LevelEditorWindow : EditorWindow
{
    private int levelIndex;
    private LevelData currentLevelData;
    private Transform levelRoot;

    [MenuItem("Tools/Level Editor")]
    public static void ShowWindow()
    {
        GetWindow<LevelEditorWindow>("Level Editor");
    }

    void OnEnable()
    {
        if (!Application.isPlaying)
        {
            AutoFindLevelData();
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("LEVEL EDITOR", EditorStyles.boldLabel);

        // Theo dõi sự thay đổi của Level Index để tự tìm data
        EditorGUI.BeginChangeCheck();
        levelIndex = EditorGUILayout.IntField("Level Index", levelIndex);

        currentLevelData = (LevelData)EditorGUILayout.ObjectField(
            "Level Data",
            currentLevelData,
            typeof(LevelData),
            false
        );

        levelRoot = (Transform)EditorGUILayout.ObjectField(
            "Level Root",
            levelRoot,
            typeof(Transform),
            true
        );

        GUILayout.Space(10);

        if (GUILayout.Button("Save Level"))
        {
            SaveLevel();
        }

        if (GUILayout.Button("Load Level"))
        {
            LoadLevel();
        }

        if (GUILayout.Button("Clear Level"))
        {
            ClearLevel();
        }
    }

    private void AutoFindLevelData()
    {
        string fileName = $"Level_{levelIndex}";
        string[] guids = AssetDatabase.FindAssets(fileName, new[] { "Assets/Levels" });

        if (guids.Length > 0)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            currentLevelData = AssetDatabase.LoadAssetAtPath<LevelData>(path);
        }
        else
        {
            currentLevelData = null;
        }
    }

    // ================= SAVE =================
    private void SaveLevel()
    {
        LevelData data = ScriptableObject.CreateInstance<LevelData>();
        data.levelIndex = levelIndex;
        data.objects = new List<LevelObjectData>();

        LevelObject[] objs = FindObjectsOfType<LevelObject>();

        foreach (var obj in objs)
        {
            LevelObjectData objData = new LevelObjectData
            {
                position = obj.transform.position,
                rotation = obj.transform.rotation,
                prefab = obj.prefabSource
            };

            data.objects.Add(objData);
        }

        string folderPath = "Assets/Levels";
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets", "Levels");
        }

        string path = $"{folderPath}/Level_{levelIndex}.asset";

        AssetDatabase.CreateAsset(data, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        // Tự động gán data vừa lưu vào ô Level Data cho Sếp
        currentLevelData = AssetDatabase.LoadAssetAtPath<LevelData>(path);

        Debug.Log("✅ Đã lưu và tự động gán Level: " + path);
    }

    // ================= LOAD =================
    private void LoadLevel()
    {
        if (currentLevelData == null)
        {
            Debug.LogWarning("❌ Chưa có Level Data để load, thưa Sếp!");
            return;
        }

        if (levelRoot == null)
        {
            Debug.LogError("❌ Sếp chưa gán Level Root (LevelController) kìa!");
            return;
        }

        // Xóa các object cũ trước khi load
        ClearLevel();

        Undo.IncrementCurrentGroup();
        int group = Undo.GetCurrentGroup();

        foreach (var objData in currentLevelData.objects)
        {
            if (objData.prefab == null) continue;

            GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(objData.prefab);
            Undo.RegisterCreatedObjectUndo(obj, "Spawn Level Object");

            obj.transform.position = objData.position;
            obj.transform.rotation = objData.rotation;
            obj.transform.SetParent(levelRoot);

            LevelObject levelObj = obj.GetComponent<LevelObject>();
            if (levelObj == null)
            {
                levelObj = Undo.AddComponent<LevelObject>(obj);
            }
            levelObj.prefabSource = objData.prefab;
        }

        Undo.CollapseUndoOperations(group);
        Debug.Log("✅ Đã Load Level xong cho Sếp!");
    }

    // ================= CLEAR =================
    private void ClearLevel()
    {
        if (levelRoot == null)
        {
            Debug.LogWarning("❌ Cần gán Level Root để dọn dẹp ạ!");
            return;
        }

        Undo.IncrementCurrentGroup();
        int group = Undo.GetCurrentGroup();

        for (int i = levelRoot.childCount - 1; i >= 0; i--)
        {
            Undo.DestroyObjectImmediate(levelRoot.GetChild(i).gameObject);
        }

        Undo.CollapseUndoOperations(group);
        Debug.Log("🧹 Đã dọn dẹp sạch sẽ Level!");
    }
}