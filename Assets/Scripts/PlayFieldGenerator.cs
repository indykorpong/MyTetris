using UnityEngine;

public static class PlayFieldGenerator
{
    public const float BlockBorderWidth = 6; // block border size
    public const float BlockWidth = 54; // square outer block's width

    private const int NHorizontalBlocks = 10;
    private const int NVerticalBlocks = 22;

    private static Vector3 _outerBlockOffset;
    private static Vector3 _playFieldOffset;
    private static float _xPlayFieldOffset;
    private static float _yPlayFieldOffset;

    public static void Init()
    {
        CalculateParameters();
        CreatePlayField();
    }

    private static void CalculateParameters()
    {
        _outerBlockOffset = new Vector3(0, 0, 0.1f);
        _xPlayFieldOffset = -(NHorizontalBlocks * (BlockWidth - BlockBorderWidth) + BlockBorderWidth) / 2;
        _yPlayFieldOffset = -(NVerticalBlocks * (BlockWidth - BlockBorderWidth) + BlockBorderWidth) / 2;
        _playFieldOffset = new Vector3(_xPlayFieldOffset, _yPlayFieldOffset, 0);
    }

    private static GameObject CreatePlayField()
    {
        var playField = new GameObject("PlayField");

        for (int y = 0; y < NVerticalBlocks; y++)
        for (int x = 0; x < NHorizontalBlocks; x++)
            if (x == 0 && y == 0)
            {
                var block = CreateFullBlock(BlockWidth, BlockBorderWidth);
                block.transform.parent = playField.transform;
                block.transform.position = _playFieldOffset + new Vector3(BlockWidth / 2, BlockWidth / 2, 0);
            }
            else if (x > 0 && y == 0)
            {
                var block = CreateSideBlock(BlockWidth, BlockBorderWidth);
                block.transform.parent = playField.transform;
                block.transform.position = _playFieldOffset + new Vector3(
                    BlockWidth + (x - 0.5f) * (BlockWidth - BlockBorderWidth),
                    BlockWidth / 2, 0);
            }
            else if (x == 0 && y > 0)
            {
                var block = CreateUBlock(BlockWidth, BlockBorderWidth);
                block.transform.parent = playField.transform;
                block.transform.position = _playFieldOffset + new Vector3(BlockWidth / 2,
                    BlockWidth + (y - 0.5f) * (BlockWidth - BlockBorderWidth), 0);
            }
            else
            {
                var block = CreateQuarterBlock(BlockWidth, BlockBorderWidth);
                block.transform.parent = playField.transform;
                block.transform.position = _playFieldOffset + new Vector3(
                    BlockWidth + (x - 0.5f) * (BlockWidth - BlockBorderWidth),
                    BlockWidth + (y - 0.5f) * (BlockWidth - BlockBorderWidth), 0);
            }

        return playField;
    }

    private static GameObject CreateFullBlock(float fullWidth, float borderWidth)
    {
        var fullBlock = new GameObject("FullBlock");

        var outerBlock = CreateBlockObject("OuterBlock", fullBlock, _outerBlockOffset, new Vector2(fullWidth, fullWidth),
            Color.black);

        var innerBlock = CreateBlockObject("InnerBlock", fullBlock, Vector3.zero,
            new Vector2(fullWidth - 2 * borderWidth, fullWidth - 2 * borderWidth), Color.white);

        return fullBlock;
    }

    private static GameObject CreateSideBlock(float fullWidth, float borderWidth)
    {
        var sideBlock = new GameObject("SideBlock");

        var outerBlock = CreateBlockObject("OuterBlock", sideBlock, _outerBlockOffset,
            new Vector2(fullWidth - borderWidth, fullWidth),
            Color.black);

        var innerBlock = CreateBlockObject("InnerBlock", sideBlock, new Vector3(-borderWidth / 2, 0, 0),
            new Vector2(fullWidth - 2 * borderWidth, fullWidth - 2 * borderWidth), Color.white);

        return sideBlock;
    }

    private static GameObject CreateUBlock(float fullWidth, float borderWidth)
    {
        var uBlock = new GameObject("UBlock");

        var outerBlock = CreateBlockObject("OuterBlock", uBlock, _outerBlockOffset,
            new Vector2(fullWidth, fullWidth - borderWidth),
            Color.black);

        var innerBlock = CreateBlockObject("InnerBlock", uBlock, new Vector3(0, -borderWidth / 2, 0),
            new Vector2(fullWidth - 2 * borderWidth, fullWidth - 2 * borderWidth), Color.white);

        return uBlock;
    }

    private static GameObject CreateQuarterBlock(float fullWidth, float borderWidth)
    {
        var quarterBlock = new GameObject("QuarterBlock");

        var outerBlock = CreateBlockObject("OuterBlock", quarterBlock, _outerBlockOffset,
            new Vector2(fullWidth - borderWidth, fullWidth - borderWidth),
            Color.black);

        var innerBlock = CreateBlockObject("InnerBlock", quarterBlock, new Vector3(-borderWidth / 2, -borderWidth / 2, 0),
            new Vector2(fullWidth - 2 * borderWidth, fullWidth - 2 * borderWidth), Color.white);

        return quarterBlock;
    }

    public static GameObject CreateBlockObject(string objectName, GameObject parentObject, Vector3 localPosition,
        Vector2 size, Color color)
    {
        var block = new GameObject(objectName);
        block.transform.parent = parentObject.transform;
        block.transform.localPosition = localPosition;
        var blockSpriteRenderer = block.AddComponent<SpriteRenderer>();
        blockSpriteRenderer.color = color;
        blockSpriteRenderer.sprite = CreateRectangleSprite(size.x, size.y, color);

        return block;
    }

    private static Sprite CreateRectangleSprite(float width, float height, Color color)
    {
        var texture = new Texture2D(Mathf.CeilToInt(width), Mathf.CeilToInt(height));
        for (int y = 0; y < height; y++)
        for (int x = 0; x < width; x++)
            texture.SetPixel(x, y, color);

        var tile = Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 1f);
        return tile;
    }
}