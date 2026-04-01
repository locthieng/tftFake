using UnityEngine;
using UnityEditor;

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

    private void OnGUI()
    {
        GUILayout.Label("LEVEL EDITOR", EditorStyles.boldLabel);

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

        if (GUILayout.Button("💾 Save Level"))
        {
            SaveLevel();
        }

        if (GUILayout.Button("📥 Load Level"))
        {
            LoadLevel();
        }

        if (GUILayout.Button("🧹 Clear Level"))
        {
            ClearLevel();
        }
    }

    // ================= SAVE =================
    private void SaveLevel()
    {
        LevelData data = ScriptableObject.CreateInstance<LevelData>();
        data.levelIndex = levelIndex;

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

        Debug.Log("✅ Saved Level: " + path);
    }

    // ================= LOAD =================
    private void LoadLevel()
    {
        if (currentLevelData == null)
        {
            Debug.LogWarning("❌ Missing LevelData!");
            return;
        }

        GameObject levelController = GameObject.Find("LevelController");

        if (levelController == null)
        {
            Debug.LogError("❌ Không tìm thấy LevelController trong scene!");
            return;
        }

        foreach (Transform child in levelController.transform)
        {
            Undo.DestroyObjectImmediate(child.gameObject);
        }

        Undo.IncrementCurrentGroup();
        int group = Undo.GetCurrentGroup();

        GameObject root = new GameObject($"Level_{currentLevelData.levelIndex}");
        Undo.RegisterCreatedObjectUndo(root, "Create Level Root");

        root.transform.SetParent(levelController.transform);

        foreach (var objData in currentLevelData.objects)
        {
            if (objData.prefab == null) continue;

            GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(objData.prefab);
            Undo.RegisterCreatedObjectUndo(obj, "Spawn Level Object");

            obj.transform.position = objData.position;
            obj.transform.rotation = objData.rotation;

            obj.transform.SetParent(root.transform);

            LevelObject levelObj = obj.GetComponent<LevelObject>();
            if (levelObj == null)
            {
                levelObj = Undo.AddComponent<LevelObject>(obj);
            }

            levelObj.prefabSource = objData.prefab;
        }

        Undo.CollapseUndoOperations(group);

        Selection.activeGameObject = root;

        Debug.Log("✅ Loaded Level vào LevelController!");
    }

    // ================= CLEAR =================
    private void ClearLevel()
    {
        if (levelRoot == null)
        {
            Debug.LogWarning("❌ LevelController chưa được gán!");
            return;
        }

        Undo.IncrementCurrentGroup();
        int group = Undo.GetCurrentGroup();

        for (int i = levelRoot.childCount - 1; i >= 0; i--)
        {
            Transform child = levelRoot.GetChild(i);

            if (child != null)
            {
                Undo.DestroyObjectImmediate(child.gameObject);
            }
        }

        Undo.CollapseUndoOperations(group);

        Debug.Log("🧹 Clear Level thành công!");
    }
}