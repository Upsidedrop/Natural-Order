using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    //Assigning variables
    Tilemap tilemap;
    [SerializeField]
    Tile grass;
    [SerializeField]
    Tile water;
    int tilemapWidth = 75;
    int tilemapHeight = 60;
    Vector3Int globalOffset = new(150, 125, 0);
    [SerializeField]
    int seed;
    Tile[] buffer;
    Tile[] tiles;
    readonly int worldHeight = 442;
    readonly int worldWidth = 591;
    [SerializeField]
    int smoothnessFactor;
    [SerializeField]
    float waterLevel;
    private void Awake()
    {
        UnityEngine.Random.InitState(seed);
        tilemap = GetComponent<Tilemap>();
        buffer = new Tile[worldWidth * worldHeight];
        tiles = new Tile[buffer.Length];
        CreateWater();

    }
    // Start is called before the first frame update
    void Start()
    {
        GenerateSeed(new Vector3Int(-globalOffset.x, globalOffset.y, 0));
        SmoothenTerrain(new Vector3Int(-globalOffset.x, globalOffset.y, 0));
        GenerateSeed(new Vector3Int(globalOffset.x, globalOffset.y, 0));
        SmoothenTerrain(new Vector3Int(globalOffset.x, globalOffset.y, 0));
        GenerateSeed(new Vector3Int(-globalOffset.x, -globalOffset.y, 0));
        SmoothenTerrain(new Vector3Int(-globalOffset.x, -globalOffset.y, 0));
        GenerateSeed(new Vector3Int(globalOffset.x, -globalOffset.y, 0));
        SmoothenTerrain(new Vector3Int(globalOffset.x, -globalOffset.y, 0));
        PaintTiles();
    }
    void GenerateSeed(Vector3Int offset)
    {
        for (int i = 0; i < tilemapWidth * 2; i++)
        {
            for (int i1 = 0; i1 < tilemapHeight * 2; i1++)
            {

                switch (UnityEngine.Random.Range(1, waterLevel+2))
                {
                    case < 2:
                        tiles[Vector3IntToInt(offset + new Vector3Int(-tilemapWidth + i, -tilemapHeight + i1, 0))] = grass;
                        break;
                    default:
                        tiles[Vector3IntToInt(offset + new Vector3Int(-tilemapWidth + i, -tilemapHeight + i1, 0))] = water;
                        break;
                }

            }

        }
    }
    void SmoothenTerrain(Vector3Int offset)
    {
        for (int r = 0; r < smoothnessFactor; r++)
        {
            for (int i = 0; i < tilemapWidth * 2; i++)
            {
                    for (int i1 = 0; i1 < tilemapHeight * 2; i1++)
                    {
                        if (CountNeighbors(i, i1, offset) > 3)
                        {
                            buffer[Vector3IntToInt(offset + new Vector3Int(-tilemapWidth + i, -tilemapHeight + i1, 0))] = grass;
                        }
                        else
                        {
                            buffer[Vector3IntToInt(offset + new Vector3Int(-tilemapWidth + i, -tilemapHeight + i1, 0))] = water;
                        }
                    }
            }
            Array.Copy(buffer, tiles, buffer.Length);
        }
        
    }
    int CountNeighbors(int i, int i1, Vector3Int offset)
    {
        int count = 0;
        if (tiles[Vector3IntToInt(offset + new Vector3Int(-tilemapWidth + i + 1, -tilemapHeight + i1, 0))] == grass)
        {
            count++;
        }
        if (tiles[Vector3IntToInt(offset + new Vector3Int(-tilemapWidth + i, -tilemapHeight + i1 + 1, 0))] == grass)
        {
            count++;
        }
        if (tiles[Vector3IntToInt(offset + new Vector3Int(-tilemapWidth + i, -tilemapHeight + i1 - 1, 0))] == grass)
        {
            count++;
        }
        if (tiles[Vector3IntToInt(offset + new Vector3Int(-tilemapWidth + i - 1, -tilemapHeight + i1, 0))] == grass)
        {
            count++;
        }
        if (tiles[Vector3IntToInt(offset + new Vector3Int(-tilemapWidth + i + 1, -tilemapHeight + i1 - 1, 0))] == grass)
        {
            count++;
        }
        if (tiles[Vector3IntToInt(offset + new Vector3Int(-tilemapWidth + i + 1, -tilemapHeight + i1 + 1, 0))] == grass)
        {
            count++;
        }
        if (tiles[Vector3IntToInt(offset + new Vector3Int(-tilemapWidth + i - 1, -tilemapHeight + i1 - 1, 0))] == grass)
        {
            count++;
        }
        if (tiles[Vector3IntToInt(offset + new Vector3Int(-tilemapWidth + i - 1, -tilemapHeight + i1 + 1, 0))] == grass)
        {
            count++;
        }
        return count;
    }
    int Vector3IntToInt(Vector3Int vector3Int)
    {
        int result = vector3Int.y + 217 + (worldHeight * (vector3Int.x + 289));
        return result;
    }
    void PaintTiles()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            tilemap.SetTile(IntToVector3Int(i), tiles[i]);
        }
    }
    Vector3Int IntToVector3Int(int input)
    {
        Vector3Int result = new(
            ((input - (input % worldHeight)) / worldHeight) - 289,
            (input % worldHeight) - 217);
        return result;
    }
    void CreateWater()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = water;
            buffer[i] = water;
        }
    }
}
