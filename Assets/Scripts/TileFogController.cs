using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Tilemaps;



[Serializable]
public class TileData
{
    public TileType TileType = TileType.AIR;

}

public enum TileType
{
    SOLID = 0,
    AIR = 1
}

public class TileFogController : MonoBehaviour
{

    [SerializeField]
    Tilemap _tilemapSolid, _tilemapAir;

    [Range(32,256)]
    [SerializeField] int _tilemapSize;

    TileData[,] _tiles;
    [SerializeField]
    TileBase _tileSolid, _tileAir;

    [SerializeField]
    float _lightUpdateDelay;

    private void Awake()
    {

        // Store info about the tiles in the scene
        _tiles = new TileData[_tilemapSize, _tilemapSize];

        for (int x = 0; x < _tilemapSize; x++)
        {
            for (int y = 0; y< _tilemapSize; y++)
            {
                TileData tile = new TileData();

                if (x <= 1 || x >= _tilemapSize-1 || y <= 1 || y >= _tilemapSize - 1)
                {
                    tile.TileType = TileType.SOLID;
                    _tiles[x, y] = tile;
                    _tilemapSolid.SetTile(new Vector3Int(x, y), _tileSolid);
                }
                else if (_tilemapSolid.GetTile(new Vector3Int( x, y)) != null)
                {
                    //Solid
                    tile.TileType = TileType.SOLID;
                    _tiles[x, y] = tile;
                }
                else
                {
                    // Air
                    tile.TileType = TileType.AIR;
                    _tiles[x, y] = tile;
                    _tilemapAir.SetTile(new Vector3Int(x, y), _tileAir);
                }
            }
        }

        SetLights();
    }

    
    void SetLights()
    {
        // Bulk Set Tiles
        Vector3Int[] positions = new Vector3Int[_tilemapSize * _tilemapSize];
        TileBase[] tileArraySolid = new TileBase[positions.Length];
        TileBase[] tileArrayAir = new TileBase[positions.Length];
        int posIndex = 0;

        for (int x = 0; x < _tiles.GetLength(0); x++)
        {
            for (int y = 0; y < _tiles.GetLength(1); y++)
            {

                if(_tiles[x,y].TileType == TileType.SOLID)
                {
                    tileArraySolid[x * _tilemapSize + y] = _tileSolid;
                    tileArrayAir[x * _tilemapSize + y] = null;
                }
                else
                {
                    tileArrayAir[x * _tilemapSize + y] = _tileAir;
                    tileArraySolid[x * _tilemapSize + y] = null;
                }

                positions[posIndex] = new Vector3Int(x, y, 0);
                posIndex++;
            }
        }
        _tilemapAir.SetTiles(positions, tileArrayAir);
        _tilemapSolid.SetTiles(positions, tileArraySolid);
    }

    void LateUpdate()
    {
        // Left click draws/erases solid tiles
        if (Input.GetMouseButtonUp(0))
        {
                DrawOrEraseSingleTile();
        }
    }



    void DrawOrEraseSingleTile()
    {
        // Get mouse position
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        int x = (int)((pos.x));
        int y = (int)((pos.y));

        if (x != 0 && y != 0 && x != _tilemapSize - 1 && y != _tilemapSize - 1)
        {
            if ((x > 0 && x < _tiles.GetLength(0)) && (y > 0 && y < _tiles.GetLength(1)))
            {
                if (_tiles[x, y].TileType == TileType.SOLID)
                {
                    // Set Air
                    _tiles[x, y].TileType = TileType.AIR;
                    _tilemapAir.SetTile(new Vector3Int(x, y), _tileAir);
                    _tilemapSolid.SetTile(new Vector3Int(x,y), null);
                }
                else
                {
                    // Set Solid
                    _tiles[x, y].TileType = TileType.SOLID;
                    _tilemapSolid.SetTile(new Vector3Int(x, y), _tileSolid);
                    _tilemapAir.SetTile(new Vector3Int(x, y), null);
                }
            }
        }
    }

}
