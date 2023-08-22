using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Tile : MonoBehaviour
{
    public UnitState UnitState => _unitState;
    private UnitState _unitState;

    private GridManager gridManager;
    public TileController tileController;

    public int x;
    public int y;

    public TextMeshPro mineCountText;
    public GameObject gameOver;
    public GameObject grass;

    private bool isClicked=false;

    public static Tile Instance { get; private set; }


    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        Instance = this;
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
        if (isClicked) return;
        GridManager.Instance.TileClickCount();
        if (gridManager.IsGameOver()||gridManager.IsGameWin())
        {
            return; // Oyun bitmişse tıklamalar devre dışı
        }

        isClicked = true;
        int neighborMineCount = tileController.CalculateNeighborMineCount(x, y);

        if (_unitState == UnitState.Mine)
        {
            Debug.Log("Mayına tıklandı");
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
                Debug.Log("Mayına tıklanmadı");

                mineCountText.text = neighborMineCount.ToString();
            }

            Debug.Log($"Tıklanan karenin koordinatları: X = {x}, Y = {y}");
        }
    }
    
    public bool IsClicked() => isClicked;
}


public enum UnitState
{
     Empty,
     Mine
}

