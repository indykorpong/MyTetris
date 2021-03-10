using UnityEngine;

public class PlayFieldGenerator : MonoBehaviour
{
    public float blockBorderWidth; // block border size
    public float blockWidth; // block complete width in y-axis

    public int nHorizontalBlocks = 10;
    public int nVerticalBlocks = 22;
    private Vector3 _playFieldOffset;

    private float _xPlayFieldOffset;
    private float _yPlayFieldOffset;

    private void Start()
    {
        CalculateOffset();
        CreatePlayField();
    }

    private void CalculateOffset()
    {
        _xPlayFieldOffset = -(nHorizontalBlocks * (blockWidth - blockBorderWidth) + blockBorderWidth) / 2;
        _yPlayFieldOffset = -(nVerticalBlocks * (blockWidth - blockBorderWidth) + blockBorderWidth) / 2;
        _playFieldOffset = new Vector3(_xPlayFieldOffset, _yPlayFieldOffset, 0);
    }

    private void CreatePlayField()
    {
        for (int y = 0; y < nVerticalBlocks; y++)
        for (int x = 0; x < nHorizontalBlocks; x++)
            if (x == 0 && y == 0)
            {
                var block = CreateFullBlock(blockWidth, blockBorderWidth);
                block.transform.parent = transform;
                block.transform.position = _playFieldOffset + new Vector3(blockWidth / 2, blockWidth / 2, 0);
            }
            else if (x > 0 && y == 0)
            {
                var block = CreateSideBlock(blockWidth, blockBorderWidth);
                block.transform.parent = transform;
                block.transform.position = _playFieldOffset + new Vector3(
                    blockWidth + (x - 0.5f) * (blockWidth - blockBorderWidth),
                    blockWidth / 2, 0);
            }
            else if (x == 0 && y > 0)
            {
                var block = CreateUBlock(blockWidth, blockBorderWidth);
                block.transform.parent = transform;
                block.transform.position = _playFieldOffset + new Vector3(blockWidth / 2,
                    blockWidth + (y - 0.5f) * (blockWidth - blockBorderWidth), 0);
            }
            else
            {
                var block = CreateQuarterBlock(blockWidth, blockBorderWidth);
                block.transform.parent = transform;
                block.transform.position = _playFieldOffset + new Vector3(
                    blockWidth + (x - 0.5f) * (blockWidth - blockBorderWidth),
                    blockWidth + (y - 0.5f) * (blockWidth - blockBorderWidth), 0);
            }
    }

    private GameObject CreateFullBlock(float fullWidth, float borderWidth)
    {
        var block = new GameObject("FullBlock");

        var outerBlock = new GameObject("OuterBlock");
        outerBlock.transform.parent = block.transform;
        var outerBlockSpriteRenderer = outerBlock.AddComponent<SpriteRenderer>();
        outerBlockSpriteRenderer.sprite = CreateSquareBlockSprite(fullWidth, Color.black);

        var innerBlock = new GameObject("InnerBlock");
        innerBlock.transform.parent = block.transform;
        var innerBlockSpriteRender = innerBlock.AddComponent<SpriteRenderer>();
        innerBlockSpriteRender.sprite = CreateSquareBlockSprite(fullWidth - 2 * borderWidth, Color.white);

        return block;
    }

    private GameObject CreateSideBlock(float fullWidth, float borderWidth)
    {
        var block = new GameObject("SideBlock");

        var outerBlock = new GameObject("OuterBlock");
        outerBlock.transform.parent = block.transform;
        var outerBlockSpriteRenderer = outerBlock.AddComponent<SpriteRenderer>();
        outerBlockSpriteRenderer.sprite = CreateRectangleSprite(fullWidth - borderWidth, fullWidth, Color.black);

        var innerBlock = new GameObject("InnerBlock");
        innerBlock.transform.parent = block.transform;
        innerBlock.transform.position = new Vector3(-borderWidth / 2, 0, 0);
        var innerBlockSpriteRender = innerBlock.AddComponent<SpriteRenderer>();
        innerBlockSpriteRender.sprite =
            CreateRectangleSprite(fullWidth - 2 * borderWidth, fullWidth - 2 * borderWidth, Color.white);

        return block;
    }

    private GameObject CreateUBlock(float fullWidth, float borderWidth)
    {
        var block = new GameObject("UBlock");

        var outerBlock = new GameObject("OuterBlock");
        outerBlock.transform.parent = block.transform;
        var outerBlockSpriteRenderer = outerBlock.AddComponent<SpriteRenderer>();
        outerBlockSpriteRenderer.sprite =
            CreateRectangleSprite(fullWidth, fullWidth - borderWidth, Color.black);

        var innerBlock = new GameObject("InnerBlock");
        innerBlock.transform.parent = block.transform;
        innerBlock.transform.position = new Vector3(0, -borderWidth / 2, 0);
        var innerBlockSpriteRender = innerBlock.AddComponent<SpriteRenderer>();
        innerBlockSpriteRender.sprite =
            CreateRectangleSprite(fullWidth - 2 * borderWidth, fullWidth - 2 * borderWidth, Color.white);

        return block;
    }

    private GameObject CreateQuarterBlock(float fullWidth, float borderWidth)
    {
        var block = new GameObject("QuarterBlock");

        var outerBlock = new GameObject("OuterBlock");
        outerBlock.transform.parent = block.transform;
        var outerBlockSpriteRenderer = outerBlock.AddComponent<SpriteRenderer>();
        outerBlockSpriteRenderer.sprite =
            CreateRectangleSprite(fullWidth - borderWidth, fullWidth - borderWidth, Color.black);

        var innerBlock = new GameObject("InnerBlock");
        innerBlock.transform.parent = block.transform;
        innerBlock.transform.position = new Vector3(-borderWidth / 2, -borderWidth / 2, 0);
        var innerBlockSpriteRender = innerBlock.AddComponent<SpriteRenderer>();
        innerBlockSpriteRender.sprite =
            CreateRectangleSprite(fullWidth - 2 * borderWidth, fullWidth - 2 * borderWidth, Color.white);

        return block;
    }

    private Sprite CreateSquareBlockSprite(float length, Color color)
    {
        var texture = new Texture2D(Mathf.CeilToInt(length), Mathf.CeilToInt(length));
        for (int y = 0; y < length; y++)
        for (int x = 0; x < length; x++)
            texture.SetPixel(x, y, color);

        var tile = Sprite.Create(texture, new Rect(0, 0, length, length), new Vector2(0.5f, 0.5f), 1f);
        return tile;
    }

    private Sprite CreateRectangleSprite(float width, float height, Color color)
    {
        var texture = new Texture2D(Mathf.CeilToInt(width), Mathf.CeilToInt(height));
        for (int y = 0; y < height; y++)
        for (int x = 0; x < width; x++)
            texture.SetPixel(x, y, color);

        var tile = Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 1f);
        return tile;
    }
}