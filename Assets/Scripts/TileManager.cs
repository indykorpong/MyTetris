using System.Collections.Generic;
using UnityEngine;

public static class TileManager
{
    private static Dictionary<PieceType, Direction[]> pieceDirectionsFromPieceType;
    private static Dictionary<PieceType, Color> colorFromPieceType;
    private static int rotationOffset;

    public static void SetupParameters()
    {
        pieceDirectionsFromPieceType = new Dictionary<PieceType, Direction[]>
        {
            [PieceType.I] = new[] {Direction.Right, Direction.Right, Direction.Right, Direction.Right},
            [PieceType.J] = new[] {Direction.Right, Direction.Down, Direction.Right, Direction.Right},
            [PieceType.L] = new[] {Direction.Right, Direction.Right, Direction.Right, Direction.Up},
            [PieceType.O] = new[] {Direction.Right, Direction.Right, Direction.Down, Direction.Left, Direction.Up},
            [PieceType.S] = new[] {Direction.Right, Direction.Right, Direction.Up, Direction.Right},
            [PieceType.T] = new[] {Direction.Right, Direction.Down, Direction.Left, Direction.Right, Direction.Right},
            [PieceType.Z] = new[] {Direction.Right, Direction.Right, Direction.Down, Direction.Right}
        };

        colorFromPieceType = new Dictionary<PieceType, Color>
        {
            [PieceType.I] = Color.cyan,
            [PieceType.J] = Color.blue,
            [PieceType.L] = new Color(1f, 0.5f, 0f),
            [PieceType.O] = Color.yellow,
            [PieceType.S] = Color.green,
            [PieceType.T] = Color.magenta,
            [PieceType.Z] = Color.red
        };

        rotationOffset = 0;
    }

    public static void RotateTileClockwise(ref Vector2Int originPosition, PieceType pieceType)
    {
        var tilePositions = CreateTile(originPosition, pieceType);
        rotationOffset += 1;
        var tileNewPositions = CreateTile(originPosition, pieceType);
        if (tileNewPositions.Count == 0)
        {
            tileNewPositions = tilePositions;
        }

        foreach (var position in tilePositions)
        {
            PlayFieldGenerator.TileObjects[position.x, position.y].SetColor(Color.white);
        }

        foreach (var position in tileNewPositions)
        {
            PlayFieldGenerator.TileObjects[position.x, position.y].SetColor(colorFromPieceType[pieceType]);
        }
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
            PlayFieldGenerator.TileObjects[position.x, position.y].SetColor(colorFromPieceType[pieceType]);
    }

    public static List<Vector2Int> CreateTile(Vector2Int originPosition, PieceType pieceType)
    {
        var tilePositions = new List<Vector2Int>();
        var position = originPosition;
        var count = 0;
        foreach (var direction in pieceDirectionsFromPieceType[pieceType])
        {
            var rotatedDirection = (Direction) (((int) direction + rotationOffset) % 4);
            if (count == 0)
                count += 1;
            else
                position += DirectionToVector2Int(rotatedDirection);

            if (IsOutOfBounds(position.x, position.y)) return new List<Vector2Int>();
            tilePositions.Add(position);
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