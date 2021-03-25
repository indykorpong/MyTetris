using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<Vector2Int> tilePositions = new List<Vector2Int>();
    
    private void Start()
    {
        PlayFieldGenerator.Init();
        TileManager.SetupParameters();
        tilePositions = TileManager.CreateTileI();
        StartCoroutine(UpdateTile());
    }
    
    private IEnumerator UpdateTile()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            TileManager.UpdateTile(ref tilePositions, Direction.Down);
        }
    }
}