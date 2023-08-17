using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Tile : MonoBehaviour
{
    public UnitState UnitState => _unitState;

    public TileState TileState { get; internal set; }

    private UnitState _unitState;
    public GameObject grass;
    private GridManager gridManager;
    public TileController tileController;
    public int x;
    public int y;
    public TextMeshPro mineCountText;
    public GameObject gameOver;


    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
    }


    public void GetCoordinates(int x, int y)
    {
        this.x = x;
        this.y = y;

    }

    public void Prepare(UnitState state)
    {
        _unitState = state;
    }
    public void OnTileClick()
    {
        if (gridManager.IsGameOver() || gridManager.IsTileClicked(new Vector2Int(x, y)))
        {
            return; // Oyun bitmi�se t�klamalar devre d���
        }

        int neighborMineCount = tileController.CalculateNeighborMineCount(x, y);

        if (_unitState == UnitState.Mine)
        {
            Debug.Log("May�na t�kland�");
            gridManager.GameOver();
        }
        else
        {
            grass.SetActive(false);

            if (neighborMineCount == 0)
            {
                mineCountText.text = neighborMineCount.ToString();
            }
            else
            {
                Debug.Log("May�na t�klanmad�");
                mineCountText.text = neighborMineCount.ToString();
            }

            Debug.Log($"T�klanan karenin koordinatlar�: X = {x}, Y = {y}");
        }
    }

}

public enum UnitState
{
     Empty,
     Mine
}

public enum TileState
{
    Open,
    Revealed
}

