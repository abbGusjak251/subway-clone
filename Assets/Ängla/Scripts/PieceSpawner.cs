using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    public PieceType type;
    private Piece currentPiece;

    public void Spawn()
    {
        int pieceCount = 0;
        switch (type)
        {
            case PieceType.jump:
                pieceCount = LevelManager.Instance.jumps.Count;
                break;
            case PieceType.slide:
                pieceCount = LevelManager.Instance.slides.Count;
                break;
            case PieceType.train:
                pieceCount = LevelManager.Instance.trains.Count;
                break;
            case PieceType.ramp:
                pieceCount = LevelManager.Instance.ramps.Count;
                break;
        }
        currentPiece = LevelManager.Instance.GetPiece(type, Random.Range(0, pieceCount));
        currentPiece.gameObject.SetActive(true);
        currentPiece.transform.SetParent(transform, false);
    }

    public void DeSpawn()
    {
        currentPiece.gameObject.SetActive(false);
    }
}