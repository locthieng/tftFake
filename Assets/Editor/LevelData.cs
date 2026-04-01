using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class LevelObjectData
{
    public Vector3 position;
    public Quaternion rotation;
    public GameObject prefab;
}

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/Level Data")]
public class LevelData : ScriptableObject
{
    public int levelIndex;
    public List<LevelObjectData> objects = new List<LevelObjectData>();
}