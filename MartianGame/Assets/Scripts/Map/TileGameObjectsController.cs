using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileGameObjectsController : MonoBehaviour
{
    public List<Tile> Tiles;

    public List<Sprite> LandSprites;
    public List<Sprite> RockSprites;
    public List<Sprite> StartSprites;
    public List<Sprite> FinishPrites;

    public Sprite GetSpriteForType(TileType type)
    {
        if (type == TileType.Land)
            return LandSprites[Random.Range(0, LandSprites.Count - 1)];
        if (type == TileType.Rock)
            return RockSprites[Random.Range(0, RockSprites.Count - 1)];
        if (type == TileType.Start)
            return StartSprites[Random.Range(0, StartSprites.Count - 1)];
        if (type == TileType.Finish)
            return FinishPrites[Random.Range(0, FinishPrites.Count - 1)];

        return LandSprites[Random.Range(0, LandSprites.Count - 1)];
    }
    public Tile GetTileAt(int x, int y)
    {
        Tile foundTile = Tiles.FirstOrDefault(a => a.X == x && a.Y == y);

        return foundTile;
    }
    public void CreateTiles(int width, int height, GameObject tilePrefab)
    {
        Tiles = new List<Tile>();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject tileObject = Instantiate<GameObject>(tilePrefab, new Vector3(x * Tile.TileSize, y * Tile.TileSize, 0), Quaternion.identity);
                tileObject.transform.SetParent(transform);
                Tile tile = tileObject.GetComponent<Tile>();

                if (tile == null)
                {
                    Debug.Log("TileGameObjectsController -- CreateTiles -- Sorry, bro, no Tile component in prefab. Go and add it!");
                }

                tile.TileGameObjectsController = this;
                tile.X = x;
                tile.Y = y;
                tile.Type = TileType.Land;
                Tiles.Add(tile);
            }
        }
    }
    public void DeleteTiles()
    {
        if (Tiles != null)
        {
            foreach (var tile in Tiles)
            {
                Destroy(tile.gameObject);
            }

            Tiles = null;
        }
    }
}
