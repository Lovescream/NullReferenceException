using UnityEngine;

[CreateAssetMenu(fileName = "PrefabSpawner", menuName = "SpawnConfig/PrefabSpawner", order = 1)]
public class PrefabSpawner : ScriptableObject
{
    public PrefabType prefabType;
    public string key;
    public bool random;
    public float spawnProbability;
    public int count;
    // 기타 필요한 필드...
}
