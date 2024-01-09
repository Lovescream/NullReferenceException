using System.Collections.Generic;
using UnityEngine;

public class RandomPrefabSpawner : MonoBehaviour
{
    [SerializeField] private PrefabSpawner[] prefabSpawnerData; // Unity �����Ϳ��� �Ҵ�

    public void SpawnPrefabs()
    {
        if (Main.Object == null) return;

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
            // Main.Object�� ����Ͽ� ������ ����
            switch (spawnerData.prefabType)
            {
                case PrefabType.Enemy:
                    Main.Object.SpawnEnemy(spawnerData.key, position);
                    break;
                case PrefabType.Chest:
                    Main.Object.SpawnChest(position);
                    break;
                    // ��Ÿ �ٸ� ������ Ÿ�Կ� ���� ó��...
            }
        }
    }

    private void DeleteExistingPrefabs()
    {
        // Main.Object�� ����Ͽ� ���� ������ ����
        Main.Object.Clear();
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
            randomPositions.RemoveAt(randomIndex);
        }

        return selectedPositions;
    }
}

// �߰����� enum�̳� Ŭ������ �ʿ��� �� �ֽ��ϴ�.
public enum PrefabType { Enemy, Chest /*, ��Ÿ Ÿ��... */ }
