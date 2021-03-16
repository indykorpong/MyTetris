using UnityEngine;

public static class PieceGenerator
{
    private static Vector2 _subpieceOuterSize;
    private static Vector2 _subpieceInnerSize;
    public static float _subpieceOuterWidth;
    private static float _subpieceInnerWidth;
    private static readonly float _subpieceBorderWidth = 4;

    public static void Init()
    {
        _subpieceOuterWidth = PlayFieldGenerator.BlockWidth - PlayFieldGenerator.BlockBorderWidth;
        _subpieceInnerWidth = _subpieceOuterWidth - 2 * _subpieceBorderWidth;

        _subpieceOuterSize = new Vector2(_subpieceOuterWidth, _subpieceOuterWidth);
        _subpieceInnerSize = new Vector2(_subpieceInnerWidth, _subpieceInnerWidth);
    }

    public static Piece CreatePiece(PieceType type, Vector3 startingCoordinate)
    {
        var pieceObject = new GameObject($"Piece{type.ToString()}");
        pieceObject.transform.position = startingCoordinate;

        switch (type)
        {
            case PieceType.I:
                for (int i = 0; i < 4; i++)
                    CreateSubpieceBlock($"I{i + 1}", pieceObject,
                        new Vector3((i - 1.5f) * _subpieceOuterWidth, 0, 0),
                        _subpieceOuterSize, _subpieceInnerSize,
                        Color.black, Color.cyan);

                break;

            case PieceType.J:
                CreateSubpieceBlock("J1", pieceObject,
                    new Vector3(-_subpieceOuterWidth, _subpieceOuterWidth, 0),
                    _subpieceOuterSize, _subpieceInnerSize, Color.black, Color.blue);
                for (int i = 0; i < 3; i++)
                    CreateSubpieceBlock($"J{i + 2}", pieceObject,
                        new Vector3((i - 1f) * _subpieceOuterWidth, 0, 0),
                        _subpieceOuterSize, _subpieceInnerSize, Color.black, Color.blue);
                break;
        }

        var piece = new Piece(pieceObject, type);
        return piece;
    }

    private static GameObject CreateSubpieceBlock(string objectName, GameObject parentObject, Vector3 localPosition,
        Vector2 outerBlockSize, Vector2 innerBlockSize, Color outerBlockColor, Color innerBlockColor)
    {
        var subpiece = new GameObject(objectName);
        subpiece.transform.parent = parentObject.transform;
        subpiece.transform.localPosition = localPosition;

        var outerBlock = PlayFieldGenerator.CreateBlockObject("OuterBlock", subpiece, new Vector3(0, 0, -0.1f),
            outerBlockSize, outerBlockColor);
        var innerBlock = PlayFieldGenerator.CreateBlockObject("InnerBlock", subpiece, new Vector3(0, 0, -0.2f),
            innerBlockSize, innerBlockColor);

        return subpiece;
    }
}