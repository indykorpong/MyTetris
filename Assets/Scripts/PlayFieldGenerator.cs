using UnityEngine;

public static class PlayFieldGenerator
{
    public const float BlockBorderWidth = 6; // block border size
    public const float BlockWidth = 54; // square outer block's width

    public const int NHorizontalBlocks = 10;
    public const int NVerticalBlocks = 22;
    private static float _innerBlockWidth;

    private static Vector3 _outerBlockOffset;
    private static Vector3 _playFieldOffset;
    private static float _xPlayFieldOffset;
    private static float _yPlayFieldOffset;

    private static Vector3[,] _tilePositions;
    public static Tile[,] TileObjects { get; private set; }

    private static Vector3 _tileLayerOffset;

    public static void Init()
    {
        SetupParameters();
        CreatePlayField();
        CreateTileLayer();
    }

    private static void SetupParameters()
    {
        _innerBlockWidth = BlockWidth - 2 * BlockBorderWidth;

        _outerBlockOffset = new Vector3(0, 0, 0.1f);
        _xPlayFieldOffset = -(NHorizontalBlocks * (BlockWidth - BlockBorderWidth) + BlockBorderWidth) / 2;
        _yPlayFieldOffset = -(NVerticalBlocks * (BlockWidth - BlockBorderWidth) + BlockBorderWidth) / 2;
        _playFieldOffset = new Vector3(_xPlayFieldOffset, _yPlayFieldOffset, 0);

        _tilePositions = new Vector3[NHorizontalBlocks, NVerticalBlocks];
        TileObjects = new Tile[NHorizontalBlocks, NVerticalBlocks];

        _tileLayerOffset = new Vector3(0, 0, -0.1f);
    }

    private static GameObject CreatePlayField()
    {
        var playField = new GameObject("PlayField");

        for (int y = 0; y < NVerticalBlocks; y++)
        for (int x = 0; x < NHorizontalBlocks; x++)
        {
            GameObject block;
            GameObject innerBlock;

            if (x == 0 && y == 0)
            {
                (block, innerBlock) = CreateFullBlock(BlockWidth, BlockBorderWidth);
                block.transform.parent = playField.transform;
                block.transform.position = _playFieldOffset + new Vector3(BlockWidth / 2, BlockWidth / 2, 0);
            }
            else if (x > 0 && y == 0)
            {
                (block, innerBlock) = CreateSideBlock(BlockWidth, BlockBorderWidth);
                block.transform.parent = playField.transform;
                block.transform.position = _playFieldOffset + new Vector3(
                    BlockWidth + (x - 0.5f) * (BlockWidth - BlockBorderWidth),
                    BlockWidth / 2, 0);
            }
            else if (x == 0 && y > 0)
            {
                (block, innerBlock) = CreateUBlock(BlockWidth, BlockBorderWidth);
                block.transform.parent = playField.transform;
                block.transform.position = _playFieldOffset + new Vector3(BlockWidth / 2,
                    BlockWidth + (y - 0.5f) * (BlockWidth - BlockBorderWidth), 0);
            }
            else
            {
                (block, innerBlock) = CreateQuarterBlock(BlockWidth, BlockBorderWidth);
                block.transform.parent = playField.transform;
                block.transform.position = _playFieldOffset + new Vector3(
                    BlockWidth + (x - 0.5f) * (BlockWidth - BlockBorderWidth),
                    BlockWidth + (y - 0.5f) * (BlockWidth - BlockBorderWidth), 0);
            }

            _tilePositions[x, y] = innerBlock.transform.position;
        }

        return playField;
    }

    private static void CreateTileLayer()
    {
        var pieceLayer = new GameObject("TileLayer");
        for (int y = 0; y < NVerticalBlocks; y++)
        for (int x = 0; x < NHorizontalBlocks; x++)
        {
            TileObjects[x, y] = CreateTile($"Tile ({x},{y})", pieceLayer,
                _tilePositions[x, y] + _tileLayerOffset, new Vector2(_innerBlockWidth, _innerBlockWidth),
                Color.white);
        }
    }
    
    private static (GameObject, GameObject) CreateFullBlock(float fullWidth, float borderWidth)
    {
        var fullBlock = new GameObject("FullBlock");

        var outerBlock = CreateTile("OuterBlock", fullBlock, _outerBlockOffset,
            new Vector2(fullWidth, fullWidth),
            Color.black);

        var innerBlock = CreateTile("InnerBlock", fullBlock, Vector3.zero,
            new Vector2(fullWidth - 2 * borderWidth, fullWidth - 2 * borderWidth), Color.white);

        return (fullBlock, innerBlock.TileObject);
    }

    private static (GameObject, GameObject) CreateSideBlock(float fullWidth, float borderWidth)
    {
        var sideBlock = new GameObject("SideBlock");

        var outerBlock = CreateTile("OuterBlock", sideBlock, _outerBlockOffset,
            new Vector2(fullWidth - borderWidth, fullWidth),
            Color.black);

        var innerBlock = CreateTile("InnerBlock", sideBlock, new Vector3(-borderWidth / 2, 0, 0),
            new Vector2(fullWidth - 2 * borderWidth, fullWidth - 2 * borderWidth), Color.white);

        return (sideBlock, innerBlock.TileObject);
    }

    private static (GameObject, GameObject) CreateUBlock(float fullWidth, float borderWidth)
    {
        var uBlock = new GameObject("UBlock");

        var outerBlock = CreateTile("OuterBlock", uBlock, _outerBlockOffset,
            new Vector2(fullWidth, fullWidth - borderWidth),
            Color.black);

        var innerBlock = CreateTile("InnerBlock", uBlock, new Vector3(0, -borderWidth / 2, 0),
            new Vector2(fullWidth - 2 * borderWidth, fullWidth - 2 * borderWidth), Color.white);

        return (uBlock, innerBlock.TileObject);
    }

    private static (GameObject, GameObject) CreateQuarterBlock(float fullWidth, float borderWidth)
    {
        var quarterBlock = new GameObject("QuarterBlock");

        var outerBlock = CreateTile("OuterBlock", quarterBlock, _outerBlockOffset,
            new Vector2(fullWidth - borderWidth, fullWidth - borderWidth),
            Color.black);

        var innerBlock = CreateTile("InnerBlock", quarterBlock,
            new Vector3(-borderWidth / 2, -borderWidth / 2, 0),
            new Vector2(fullWidth - 2 * borderWidth, fullWidth - 2 * borderWidth), Color.white);

        return (quarterBlock, innerBlock.TileObject);
    }

    public static Tile CreateTile(string objectName, GameObject parentObject, Vector3 localPosition,
        Vector2 size, Color color)
    {
        var block = new GameObject(objectName);
        block.transform.parent = parentObject.transform;
        block.transform.localPosition = localPosition;
        var blockSpriteRenderer = block.AddComponent<SpriteRenderer>();
        blockSpriteRenderer.color = color;
        blockSpriteRenderer.sprite = CreateRectangleSprite(size.x, size.y, color);

        var tile = new Tile(block, blockSpriteRenderer);

        return tile;
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