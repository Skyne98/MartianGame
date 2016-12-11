using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public int X, Y;
    private TileType _type;
    public static float TileSize = 2f;
    SpriteRenderer _spriteRenderer;
    public TileGameObjectsController TileGameObjectsController;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public TileType Type
    {
        get { return _type; }
        set { _type = value; _spriteRenderer.sprite = TileGameObjectsController.GetSpriteForType(_type); }
    }

}

public enum TileType
{
    Land,
    Rock,
    Start,
    Finish
}
