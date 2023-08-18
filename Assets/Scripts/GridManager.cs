using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    public static int gridSizeX;
    public static int gridSizeY;
    
    public TextMeshProUGUI statusText;
    public List<Vector2Int> minePositions = new List<Vector2Int>(); // Mayýn konumlarýný tutacak liste
    
    private float _cellSize = 2.414f;
    private Tile[,] _grid;

    public Tile tilePrefab;
    private bool isGameOver = false;
    public float minePercentage = 0.2f; // Mayýn yüzdesi
    
    private List<Vector2Int> clickedTiles = new List<Vector2Int>(); //Týklanan kareleri tutacak liste

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CreateGrid();
        Vector3 newPosition = new Vector3(-4.63f, 0f, -8.18f);
        transform.position = newPosition;

        PlaceMines();
        PrintMinePositions();
        TileController.PrintNeighbourCoordinates();
    }

    private void CreateGrid()
    {
        gridSizeX = PlayerPrefs.GetInt("Grid Size X");
        gridSizeY = PlayerPrefs.GetInt("Grid Size Y");
        _grid = new Tile[gridSizeX, gridSizeY];

        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                Vector3 position = new Vector3(x * _cellSize, 0f, y * _cellSize);
                Tile newTile = Instantiate(tilePrefab);
                newTile.GetCoordinates(x,y);
                _grid[x, y] = newTile;

                newTile.transform.parent = transform;
                newTile.transform.localPosition = position;
                //_grid[x, y].tileController.UpdateMineCount();
            }
        }

        Vector3 centerOffset = new Vector3(gridSizeX * _cellSize * 0.5f, gridSizeY * _cellSize * 0.5f, 0);
        transform.position = -centerOffset;
    }

    private void PlaceMines()
    {
        int totalTiles = gridSizeX * gridSizeY;
        int totalMines = Mathf.RoundToInt(totalTiles * minePercentage);

        for (int i = 0; i < totalMines; i++)
        {
            int x = Random.Range(0, gridSizeX);
            int y = Random.Range(0, gridSizeY);

            // Eðer burada zaten mayýn varsa tekrar seç i'yi azalt
            if (minePositions.Contains(new Vector2Int(x, y)))
            {
                i--;
            }
            else
            {
                minePositions.Add(new Vector2Int(x, y));
                _grid[x, y].Prepare(UnitState.Mine); //tile'ýn UnitState'ini Mine burada yapýyoruz
            }
        }
    }

    private void PrintMinePositions()
    {
        foreach (var position in minePositions)
        {
            Debug.Log("Mayýn Burada X: " + position.x + ", Y: " + position.y);
        }
    }

    public Tile GetTileAtPosition(int x, int y)
    {
        return _grid[x, y];
    }

    public void AddTileToList(Vector2Int tileCoordinates)
    {
        if (!clickedTiles.Contains(tileCoordinates))
        {
            clickedTiles.Add(tileCoordinates);
        }
    }

    // Týklanýlan kareleri kontrol etme
    public bool IsTileClicked(Vector2Int tileCoordinates)
    {
        return clickedTiles.Contains(tileCoordinates);
    }


    public void Win()
    {
        statusText.text = "You Win Bitch <3";
    }
    public void GameOver()
    {
        isGameOver = true;
        Debug.Log("Oyun bitti!");
        GameManager.Instance.GameOver();
        //statusText.text = "You Lost Loser :P";
    }

    public bool IsGameOver() => isGameOver;
}
