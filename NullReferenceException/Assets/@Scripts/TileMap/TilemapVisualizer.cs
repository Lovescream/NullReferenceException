using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField] private Tilemap floorTilemap,wallTielmap;

    [SerializeField] private TileBase floorTile, wallTop;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPosition)
    {
        PaintTiles(floorPosition, floorTilemap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase Tile)
    {
        foreach (var position in positions)
        {
            paintSingleTile(tilemap, Tile, position);
        }
    }

    internal void PaintSingleBasicWall(Vector2Int position)
    {
        paintSingleTile(wallTielmap, wallTop, position);
    }

    private void paintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTielmap.ClearAllTiles();
    }
}
