using UnityEngine;

public enum PieceType
{
    I,
    J,
    L,
    O,
    S,
    T,
    Z
}

public enum Direction
{
    Up,
    Left,
    Down,
    Right
}

public class Tile
{
    public GameObject TileObject { get; set; }
    public SpriteRenderer SpriteRenderer { get; set; }

    public Tile(GameObject tileObject, SpriteRenderer spriteRenderer)
    {
        TileObject = tileObject;
        SpriteRenderer = spriteRenderer;
    }

    public void SetColor(Color color)
    {
        SpriteRenderer.color = color;
    }
}