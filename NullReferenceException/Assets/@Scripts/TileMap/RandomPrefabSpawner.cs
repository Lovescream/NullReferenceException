using System.Collections.Generic;
using UnityEngine;

public class RandomPrefabSpawner : MonoBehaviour
{
    [SerializeField] private PrefabSpawner[] prefabSpawnerData; // Unity 에디터에서 할당

    private List<GameObject> spawnedPrefabs = new List<GameObject>(); // 생성된 프리팹을 추적하기 위한 리스트

    public void SpawnPrefabs()
    {
        SoundManager.Instance.PlayMusic(SoundType.DEAD);
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
            GameObject newPrefab = Instantiate(spawnerData.prefab, new Vector3(position.x, position.y, 0), Quaternion.identity);
            spawnedPrefabs.Add(newPrefab); // 새 프리팹을 리스트에 추가
        }
    }

    private void DeleteExistingPrefabs()
    {
        foreach (GameObject prefab in spawnedPrefabs)
        {
            Destroy(prefab);
        }
        spawnedPrefabs.Clear(); // 리스트 초기화
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
            randomPositions.RemoveAt(randomIndex); // 같은 위치가 두 번 선택되는 것을 방지하기 위해 리스트에서 위치를 제거
        }

        return selectedPositions;
    }
}
