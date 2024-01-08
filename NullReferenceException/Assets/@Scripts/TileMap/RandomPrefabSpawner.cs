using System.Collections.Generic;
using UnityEngine;

public class RandomPrefabSpawner : MonoBehaviour
{
    [SerializeField] private PrefabSpawner[] prefabSpawnerData; // Unity �����Ϳ��� �Ҵ�

    private List<GameObject> spawnedPrefabs = new List<GameObject>(); // ������ �������� �����ϱ� ���� ����Ʈ

    public void SpawnPrefabs()
    {
        SoundManager.Instance.PlayMusic(SoundType.DEAD);
        DeleteExistingPrefabs(); // ���� ������ ����

        foreach (var spawnerData in prefabSpawnerData)
        {
            if (spawnerData.random)
            {
                SpawnPrefabsWithExactProbability(spawnerData);
            }
            else
            {
                SpawnPrefabsWithSpecificNumber(spawnerData);
            }
        }
    }

    private void SpawnPrefabsWithExactProbability(PrefabSpawner spawnerData)
    {
        int numberOfPrefabsToSpawn = Mathf.FloorToInt(ProveduralGenerationAlgorithms.PositionPoints.Count * spawnerData.spawnProbability);
        SpawnPrefabs(spawnerData, numberOfPrefabsToSpawn);
    }

    private void SpawnPrefabsWithSpecificNumber(PrefabSpawner spawnerData)
    {
        SpawnPrefabs(spawnerData, spawnerData.count);
    }

    private void SpawnPrefabs(PrefabSpawner spawnerData, int number)
    {
        if (ProveduralGenerationAlgorithms.PositionPoints.Count == 0)
        {
            Debug.LogError("PositionPoints ����Ʈ�� ��� �ֽ��ϴ�.");
            return;
        }

        List<Vector2Int> selectedPositions = SelectRandomPositions(number);

        foreach (Vector2Int position in selectedPositions)
        {
            GameObject newPrefab = Instantiate(spawnerData.prefab, new Vector3(position.x, position.y, 0), Quaternion.identity);
            spawnedPrefabs.Add(newPrefab); // �� �������� ����Ʈ�� �߰�
        }
    }

    private void DeleteExistingPrefabs()
    {
        foreach (GameObject prefab in spawnedPrefabs)
        {
            Destroy(prefab);
        }
        spawnedPrefabs.Clear(); // ����Ʈ �ʱ�ȭ
    }

    List<Vector2Int> SelectRandomPositions(int count)
    {
        List<Vector2Int> randomPositions = new List<Vector2Int>(ProveduralGenerationAlgorithms.PositionPoints);
        List<Vector2Int> selectedPositions = new List<Vector2Int>();

        for (int i = 0; i < count; i++)
        {
            if (randomPositions.Count == 0)
            {
                break;
            }

            int randomIndex = Random.Range(0, randomPositions.Count);
            selectedPositions.Add(randomPositions[randomIndex]);
            randomPositions.RemoveAt(randomIndex); // ���� ��ġ�� �� �� ���õǴ� ���� �����ϱ� ���� ����Ʈ���� ��ġ�� ����
        }

        return selectedPositions;
    }
}
