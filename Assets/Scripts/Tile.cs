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
    Right,
    Down,
    Left
}

public class Tile
{
    public GameObject TileObject { get; set; }
    private SpriteRenderer SpriteRenderer { get; set; }

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