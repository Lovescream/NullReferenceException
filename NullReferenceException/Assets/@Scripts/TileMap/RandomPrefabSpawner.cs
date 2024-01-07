using System.Collections.Generic;
using UnityEngine;

public class RandomPrefabSpawner : MonoBehaviour
{
    public GameObject prefab; // 유니티 에디터에서 할당
    public float spawnProbability = 0.1f; // 10% 확률

    private List<GameObject> spawnedPrefabs = new List<GameObject>(); // 생성된 프리팹들을 추적하기 위한 리스트

    public void SpawnPrefabsWithExactProbability()
    {
        DeleteExistingPrefabs(); // 기존 프리팹 삭제

        if (ProveduralGenerationAlgorithms.PositionPoints.Count == 0)
        {
            Debug.LogError("PositionPoints 리스트가 비어 있습니다.");
            return;
        }

        int numberOfPrefabsToSpawn = Mathf.FloorToInt(ProveduralGenerationAlgorithms.PositionPoints.Count * spawnProbability);
        List<Vector2Int> selectedPositions = SelectRandomPositions(numberOfPrefabsToSpawn);

        foreach (Vector2Int position in selectedPositions)
        {
            GameObject newPrefab = Instantiate(prefab, new Vector3(position.x, position.y, 0), Quaternion.identity);
            spawnedPrefabs.Add(newPrefab); // 새 프리팹을 리스트에 추가
        }
    }

    private void DeleteExistingPrefabs()
    {
        // 리스트에 있는 모든 프리팹들을 순회하며 삭제
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
            randomPositions.RemoveAt(randomIndex); // 같은 위치가 두 번 선택되지 않도록 해당 위치를 리스트에서 제거
        }

        return selectedPositions;
    }
}
