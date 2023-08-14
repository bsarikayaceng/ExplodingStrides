using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Tile : MonoBehaviour
{
    public UnitState UnitState => _unitState;
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
        if (gridManager.IsGameOver())
        {
            return; // Oyun bitmiþse týklamalar devre dýþý
        }
        if (_unitState == UnitState.Mine)
        {
            Debug.Log("Mayýna týklandý");
            gridManager.GameOver();
           // gameOver.SetActive(true);
           // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
        }
        else
        {
            grass.SetActive(false);
            Debug.Log("Mayýna týklanmadý");

            int neighborMineCount = tileController.CalculateNeighborMineCount(x, y);
            mineCountText.text = neighborMineCount.ToString();
        }
    }
}
    public enum UnitState
    {
        Empty,
        Mine
    }

