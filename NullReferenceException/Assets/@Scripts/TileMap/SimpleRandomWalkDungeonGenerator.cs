using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{
    [SerializeField] private SimpleRandomWalkSO randomWalkParmeters;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk();
        tilemapVisualizer.Clear();
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPosition = startPosition;
        HashSet<Vector2Int> floorPosition = new HashSet<Vector2Int>();
        for (int i = 0; i < randomWalkParmeters.iteration; i++)
        {
            var path = ProveduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, randomWalkParmeters.walkLangth);
            floorPosition.UnionWith(path);
            if (randomWalkParmeters.startRandomlyEachIteration) currentPosition = floorPosition.ElementAt(Random.Range(0, floorPosition.Count));
        }
        return floorPosition;
    }

}
