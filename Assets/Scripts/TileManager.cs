using System.Collections.Generic;
using UnityEngine;

public static class TileManager
{
    private static List<Vector2Int> tileIPositions;
    private static readonly int tileICount = 4;

    public static void SetupParameters()
    {
        tileIPositions = new List<Vector2Int>();
        for (int i = 0; i < tileICount; i++)
            tileIPositions.Add(new Vector2Int(PlayFieldGenerator.NHorizontalBlocks / 2 - 2 + i,
                PlayFieldGenerator.NVerticalBlocks - 1));
    }

    public static bool UpdateTile(ref List<Vector2Int> tilePositions, Direction direction)
    {
        var deltaPosition = DirectionToVector2Int(direction);

        foreach (var position in tilePositions)
            if (IsOutOfBounds(position.x + deltaPosition.x, position.y + deltaPosition.y))
                return false;


        foreach (var position in tilePositions)
            PlayFieldGenerator.TileObjects[position.x, position.y].SetColor(Color.white);

        for (int i = 0; i < tilePositions.Count; i++)
        {
            tilePositions[i] =
                new Vector2Int(tilePositions[i].x + deltaPosition.x, tilePositions[i].y + deltaPosition.y);
            PlayFieldGenerator.TileObjects[tilePositions[i].x, tilePositions[i].y].SetColor(Color.cyan);
        }

        return true;
    }

    public static List<Vector2Int> CreateTileI()
    {
        foreach (var position in tileIPositions)
            PlayFieldGenerator.TileObjects[position.x, position.y].SetColor(Color.cyan);

        return tileIPositions;
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