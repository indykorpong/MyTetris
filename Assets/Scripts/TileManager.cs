using System.Collections.Generic;
using UnityEngine;

public static class TileManager
{
    private static Dictionary<PieceType, Direction[]> pieceDirectionsFromPieceType;

    public static void SetupParameters()
    {
        pieceDirectionsFromPieceType = new Dictionary<PieceType, Direction[]>
        {
            [PieceType.I] = new[] {Direction.Right, Direction.Right, Direction.Right, Direction.Right},
            [PieceType.J] = new[] {Direction.Right, Direction.Down, Direction.Right, Direction.Right}
        };
    }

    public static void UpdateTile(ref Vector2Int originPosition, PieceType pieceType, Direction direction)
    {
        var deltaPosition = DirectionToVector2Int(direction);

        var tilePositions = CreateTile(originPosition, pieceType);
        originPosition += deltaPosition;
        var tileNewPositions = CreateTile(originPosition, pieceType);
        if (tileNewPositions.Count == 0)
        {
            tileNewPositions = tilePositions;
            originPosition -= deltaPosition;
        }

        foreach (var position in tilePositions)
            PlayFieldGenerator.TileObjects[position.x, position.y].SetColor(Color.white);

        foreach (var position in tileNewPositions)
            PlayFieldGenerator.TileObjects[position.x, position.y].SetColor(Color.cyan);
    }

    public static List<Vector2Int> CreateTile(Vector2Int originPosition, PieceType pieceType)
    {
        var tilePositions = new List<Vector2Int>();
        switch (pieceType)
        {
            case PieceType.I:
                tilePositions = CreateTileI(originPosition);
                break;
        }

        return tilePositions;
    }

    public static List<Vector2Int> CreateTileI(Vector2Int originPosition)
    {
        var tilePositions = new List<Vector2Int>();
        var position = originPosition;
        var count = 0;
        foreach (var direction in pieceDirectionsFromPieceType[PieceType.I])
        {
            if (count == 0)
                count += 1;
            else
                position += DirectionToVector2Int(direction);

            if (IsOutOfBounds(position.x, position.y)) return new List<Vector2Int>();
            tilePositions.Add(position);
            PlayFieldGenerator.TileObjects[position.x, position.y].SetColor(Color.cyan);
        }

        return tilePositions;
    }

    private static Vector2Int DirectionToVector2Int(Direction direction)
    {
        var retVector = direction switch
        {
            Direction.Up => new Vector2Int(0, 1),
            Direction.Left => new Vector2Int(-1, 0),
            Direction.Down => new Vector2Int(0, -1),
            Direction.Right => new Vector2Int(1, 0),
            _ => Vector2Int.zero
        };

        return retVector;
    }

    private static bool IsOutOfBounds(int x, int y)
    {
        return x < 0 || x >= PlayFieldGenerator.NHorizontalBlocks || y < 0 || y >= PlayFieldGenerator.NVerticalBlocks;
    }
}