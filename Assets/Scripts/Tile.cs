using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public UnitState UnitState => _unitState;
    private UnitState _unitState;
    public MeshRenderer MyMeshRenderer;

    private bool isGameOver = false; // Oyunun bitti mi?

    public void Prepare(UnitState state)
    {
        _unitState = state;
       // MyMeshRenderer.material.color = _unitState == UnitState.Mine ? Color.red : Color.green;
    }
    public void OnTileClick()
    {
        if (isGameOver)
        {
            return; // Oyun bitmiþse týklamalar devredisi
        }
        else if (_unitState == UnitState.Mine)
        {
            Debug.Log("Mayýna týklandý!");
            GameOver();
        }
        else
        {
            Debug.Log("Mayýna týklanmadý!");
        }
    }

    private void GameOver()
    {
        // Oyunu bitirme iþlemleri burada yapýlabilir
        isGameOver = true;
        Debug.Log("Oyun bitti!");
    }
}
    public enum UnitState
    {
        Empty,
        Mine
    }