using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public static int gridSizeX=5;
    public static int gridSizeY=5;
    public float cellSize;
    public Tile[,] _grid;

    public Tile tilePrefab;
    private bool isGameOver = false;
    public float minePercentage = 0.2f; // Mayýn yüzdesi
    public static List<Vector2Int> minePositions = new List<Vector2Int>(); // Mayýn konumlarýný tutacak liste
    private void Start()
    {
        CreateGrid();
        Vector3 newPosition = new Vector3(-5.8f, 0f, 8f);
        transform.position = newPosition;

        PlaceMines();
        PrintMinePositions();
        TileController.PrintNeighbourCoordinates();
    }

    private void CreateGrid()
    {
        _grid = new Tile[gridSizeX, gridSizeY];

        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                Vector3 position = new Vector3(x * cellSize, 0f, y * cellSize);
                Tile newTile = Instantiate(tilePrefab);
                newTile.GetCoordinates(x,y);
                _grid[x, y] = newTile;

                newTile.transform.parent = transform;
                newTile.transform.localPosition = position;
                Vector3 newRotation = new Vector3(-90, 0, 0);
                newTile.transform.localEulerAngles = newRotation;
                //_grid[x, y].tileController.UpdateMineCount();
            }
        }

        Vector3 centerOffset = new Vector3(gridSizeX * cellSize * 0.5f, gridSizeY * cellSize * 0.5f, 0);
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

    public void GameOver()
    {
        isGameOver = true;
        Debug.Log("Oyun bitti!");
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}
