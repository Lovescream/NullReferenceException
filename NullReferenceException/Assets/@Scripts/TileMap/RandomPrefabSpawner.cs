using System.Collections.Generic;
using UnityEngine;

public class RandomPrefabSpawner : MonoBehaviour
{
    public GameObject prefab; // ����Ƽ �����Ϳ��� �Ҵ�
    public float spawnProbability = 0.1f; // 10% Ȯ��

    private List<GameObject> spawnedPrefabs = new List<GameObject>(); // ������ �����յ��� �����ϱ� ���� ����Ʈ

    public void SpawnPrefabsWithExactProbability()
    {
        DeleteExistingPrefabs(); // ���� ������ ����

        if (ProveduralGenerationAlgorithms.PositionPoints.Count == 0)
        {
            Debug.LogError("PositionPoints ����Ʈ�� ��� �ֽ��ϴ�.");
            return;
        }

        int numberOfPrefabsToSpawn = Mathf.FloorToInt(ProveduralGenerationAlgorithms.PositionPoints.Count * spawnProbability);
        List<Vector2Int> selectedPositions = SelectRandomPositions(numberOfPrefabsToSpawn);

        foreach (Vector2Int position in selectedPositions)
        {
            GameObject newPrefab = Instantiate(prefab, new Vector3(position.x, position.y, 0), Quaternion.identity);
            spawnedPrefabs.Add(newPrefab); // �� �������� ����Ʈ�� �߰�
        }
    }

    private void DeleteExistingPrefabs()
    {
        // ����Ʈ�� �ִ� ��� �����յ��� ��ȸ�ϸ� ����
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
            randomPositions.RemoveAt(randomIndex); // ���� ��ġ�� �� �� ���õ��� �ʵ��� �ش� ��ġ�� ����Ʈ���� ����
        }

        return selectedPositions;
    }
}
