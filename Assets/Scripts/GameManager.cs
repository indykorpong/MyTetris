using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        PlayFieldGenerator.Init();
        PieceGenerator.Init();
        var position1 = new Vector3(0, -PieceGenerator._subpieceOuterWidth / 2, 0);
        PieceGenerator.CreatePiece(PieceType.I, position1);

        var position2 = new Vector3(0, PieceGenerator._subpieceOuterWidth / 2, 0);
        PieceGenerator.CreatePiece(PieceType.J, position2);
    }
}