using UnityEngine;

public enum PieceType
{
    I,
    J,
    L,
    O,
    S,
    T,
    Z
}

public class Piece
{
    public GameObject PieceObject { get; set; }
    public PieceType PieceType { get; set; }

    public Piece(GameObject pieceObject, PieceType pieceType)
    {
        PieceObject = pieceObject;
        PieceType = pieceType;
    }
}