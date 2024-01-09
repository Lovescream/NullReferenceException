using System.Collections.Generic;
using UnityEngine;

public class RandomPrefabSpawner : MonoBehaviour
{
    [SerializeField] private PrefabSpawner[] prefabSpawnerData; // Unity 에디터에서 할당

    public void SpawnPrefabs()
    {
        if (Main.Object == null) return;

        DeleteExistingPrefabs(); // 기존 프리팹 삭제

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
            Debug.LogError("PositionPoints 리스트가 비어 있습니다.");
            return;
        }

        List<Vector2Int> selectedPositions = SelectRandomPositions(number);

        foreach (Vector2Int position in selectedPositions)
        {
            // Main.Object를 사용하여 프리팹 생성
            switch (spawnerData.prefabType)
            {
                case PrefabType.Enemy:
                    Main.Object.SpawnEnemy(spawnerData.key, position);
                    break;
                case PrefabType.Chest:
                    Main.Object.SpawnChest(position);
                    break;
                    // 기타 다른 프리팹 타입에 대한 처리...
            }
        }
    }

    private void DeleteExistingPrefabs()
    {
        // Main.Object를 사용하여 기존 프리팹 삭제
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

// 추가적인 enum이나 클래스가 필요할 수 있습니다.
public enum PrefabType { Enemy, Chest /*, 기타 타입... */ }
