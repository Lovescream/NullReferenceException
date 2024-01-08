using UnityEngine;

[CreateAssetMenu(fileName = "PrefabSpawner", menuName = "SpawnConfig/PrefabSpawner", order = 1)]
public class PrefabSpawner : ScriptableObject
{
    public GameObject prefab; // Unity 에디터에서 할당
    public float spawnProbability; // 전체에서 몇퍼센트 차지할지
    public int count; // 몇개 생성할지
    public bool random;// 어떤방식으로 할지
}
