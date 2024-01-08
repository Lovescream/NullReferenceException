using UnityEngine;

[CreateAssetMenu(fileName = "PrefabSpawner", menuName = "SpawnConfig/PrefabSpawner", order = 1)]
public class PrefabSpawner : ScriptableObject
{
    public GameObject prefab; // Unity �����Ϳ��� �Ҵ�
    public float spawnProbability; // ��ü���� ���ۼ�Ʈ ��������
    public int count; // � ��������
    public bool random;// �������� ����
}
