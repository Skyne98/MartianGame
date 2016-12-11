using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Assets.Scripts.Map;
using System;
using System.Linq;

public class MapController : MonoBehaviour, IMap
{
    public string FileName;

    public GameObject TilePrefab;
    public GameObject CharacterPrefab;

    public int MapWidth;
    public int MapHeight;
    public string MapName;
    public string MapNextFileName;

    //Components
    TileGameObjectsController _tileGameObjectController;
    Character _character;

    void Awake()
    {
        _tileGameObjectController = GetComponent<TileGameObjectsController>();

        LoadFile(Application.dataPath + "//Maps//" + FileName);
    }
    void LoadFile(string fileName)
    {
        if (File.Exists(fileName))
        {
            //Delete gameobjects
            _tileGameObjectController.DeleteTiles();

            //Load variables
            StreamReader file = new StreamReader(fileName);

            int counter = 0;
            int mapLine = 0;

            string[] lines = file.ReadToEnd().Replace("\r", "").Split('\n');

            foreach (var line in lines)
            {
                if (counter == 0)
                {
                    MapWidth = int.Parse(line);
                }
                else if (counter == 1)
                {
                    MapHeight = int.Parse(line);

                    _tileGameObjectController.CreateTiles(MapWidth, MapHeight, TilePrefab);
                }
                else if (counter == 2)
                {
                    MapName = line;
                }
                else if (counter == 3)
                {
                    MapNextFileName = line;
                }

                counter++;
            }

            for (int x = 0; x < MapWidth; x++)
            {
                for (int y = 0; y < MapHeight; y++)
                {
                    _tileGameObjectController.GetTileAt(x, y).Type = GetTypeForChar(lines[4 + y][x]);
                }
            }

            if (_character != null)
            {
                Destroy(_character.gameObject);
                _character = null;
            }

            Tile startTile = _tileGameObjectController.Tiles.FirstOrDefault(a => a.Type == TileType.Start);
            GameObject characterObj = Instantiate<GameObject>(CharacterPrefab);
            _character = characterObj.GetComponent<Character>();
            _character.Initialize(startTile, _tileGameObjectController);

            file.Close();
        }
        else
        {
            LoadDefault();
        }
    }
    TileType GetTypeForChar(char ch)
    {
        if (ch == 'L')
        {
            return TileType.Land;
        }
        if (ch == 'R')
        {
            return TileType.Rock;
        }
        if (ch == 'S')
        {
            return TileType.Start;
        }
        if (ch == 'F')
        {
            return TileType.Finish;
        }

        return TileType.Land;
    }
    void LoadDefault()
    {
        _tileGameObjectController.DeleteTiles();
        _tileGameObjectController.CreateTiles(8, 8, TilePrefab);

        if (_character != null)
        {
            Destroy(_character.gameObject);
            _character = null;
        }

        Tile startTile = _tileGameObjectController.Tiles.FirstOrDefault(a => a.Type == TileType.Start);
        GameObject characterObj = Instantiate<GameObject>(CharacterPrefab);
        _character = characterObj.GetComponent<Character>();
        _character.Initialize(startTile, _tileGameObjectController);
    }

    public bool IsLoaded()
    {
        return MapWidth != 0;
    }

    public int GetWidth()
    {
        return MapWidth;
    }

    public int GetHeight()
    {
        return MapHeight;
    }

    public string GetName()
    {
        return MapName;
    }

    public Tile GetTileAt(int x, int y)
    {
        return _tileGameObjectController.GetTileAt(x, y);
    }

    public ICharacter GetCharacter()
    {
        return _character;
    }
}
