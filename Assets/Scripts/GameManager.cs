using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<Vector2Int> piecePositions = new List<Vector2Int>();
    private Vector2Int pieceOriginPosition;
    private PieceType pieceType = PieceType.I;

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
    }

    private IEnumerator UpdateTile()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            TileManager.UpdateTile(ref pieceOriginPosition, pieceType, Direction.Down);
        }
    }

    private void SetPieceOriginPosition(PieceType pieceType)
    {
        switch (pieceType)
        {
            case PieceType.I:
                pieceOriginPosition = new Vector2Int(3, 21);
                break;
        }
    }
}