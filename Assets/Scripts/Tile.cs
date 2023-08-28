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

    private bool isOpen = false;

    public static Tile Instance { get; private set; }

    private TileState _tileState;

    public TileState TileState
    {
        get { return _tileState; }
        set { _tileState = value; }
    }

    public Transform rabbitTransform; // Tavşanin Transformu

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
        //TestTouch.Instance.SetTargetTile(this);
        //Vector3 targetPosition = new Vector3(x, y, 0); // Tile'ın koordinatlarını kullanarak hedef pozisyonu oluştur
        //TestTouch.SetTargetPosition(targetPosition);
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
            //Vector3 clickedPosition = new Vector3(x, y, rabbitTransform.position.z); // Tıkladığınız karenin konumu
           // rabbitTransform.position = clickedPosition; // Tavşan karakterin pozisyonunu güncelle
            if (neighborMineCount == 0)
            {
                mineCountText.text = neighborMineCount.ToString();
                OpenZeroTilesRecursively();
            }
            else
            {
                Debug.Log("Mayına tıklanmadı");

                mineCountText.text = neighborMineCount.ToString();
            }

            Debug.Log($"Tıklanan karenin koordinatları: X = {x}, Y = {y}");
        }
    }



    public void OpenZeroTilesRecursively()
    {
        if (TileState == TileState.Open)
            return;

        TileState = TileState.Open;
        grass.SetActive(false);
        mineCountText.gameObject.SetActive(true);

        List<Tile> neighbours = GetNeighbours(x, y);

        foreach (var neighbour in neighbours)
        {
            if (neighbour.TileState != TileState.Open)
            {
                int neighborMineCount = neighbour.tileController.CalculateNeighborMineCount(neighbour.x, neighbour.y);
                neighbour.mineCountText.text = neighborMineCount.ToString();

                if (neighborMineCount == 0)
                {
                    neighbour.OpenZeroTilesRecursively();
                }
            }
        }
    }



    private List<Tile> GetNeighbours(int x, int y)
    {
        List<Tile> neighbours = new List<Tile>();

        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (i == x && j == y)
                    continue;

                if (i >= 0 && i < GridManager.Instance.GetGridSizeX && j >= 0 && j < GridManager.Instance.GetGridSizeY)
                {
                    neighbours.Add(GridManager.Instance.GetTileAtPosition(i, j));
                }
            }
        }

        return neighbours;
    }




    public bool IsClicked() => isClicked;
}


public enum UnitState
{
     Empty,
     Mine
}

public enum TileState
{
    Closed,
    Open,
    Revealed
}