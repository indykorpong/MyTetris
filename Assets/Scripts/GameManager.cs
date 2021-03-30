using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Vector2Int pieceOriginPosition = new Vector2Int(3, 22);
    private List<Vector2Int> piecePositions = new List<Vector2Int>();
    private PieceType pieceType = PieceType.J;

    private void Start()
    {
        PlayFieldGenerator.Init();
        TileManager.SetupParameters();
        SetPieceOriginPosition(pieceType);
        piecePositions = TileManager.CreateTile(pieceOriginPosition, pieceType);
        StartCoroutine(UpdateTile());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            TileManager.UpdateTile(ref pieceOriginPosition, pieceType, Direction.Left);
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            TileManager.UpdateTile(ref pieceOriginPosition, pieceType, Direction.Right);
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            TileManager.UpdateTile(ref pieceOriginPosition, pieceType, Direction.Down);
        else if(Input.GetKeyDown(KeyCode.UpArrow))
            TileManager.RotateTileClockwise(ref pieceOriginPosition, pieceType);
    }

    private IEnumerator UpdateTile()
    {
        while (true)
        {
            TileManager.UpdateTile(ref pieceOriginPosition, pieceType, Direction.Down);
            yield return new WaitForSeconds(1f);
        }
    }

    private void SetPieceOriginPosition(PieceType pieceType)
    {
        pieceOriginPosition = pieceType switch
        {
            PieceType.I => new Vector2Int(3, 22),
            PieceType.J => new Vector2Int(3, 22),
            PieceType.T => new Vector2Int(4, 22),
            _ => pieceOriginPosition
        };
    }
}