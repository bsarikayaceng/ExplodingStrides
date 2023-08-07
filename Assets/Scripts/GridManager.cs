using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
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
        Vector3 newPosition = new Vector3(-3.6f, 0.52f, 6.97f);
        transform.position = newPosition;

        Vector3 newRotation = new Vector3(-126.24f, -196.3f, 1.2f);
        transform.eulerAngles = newRotation;

        Vector3 newScale = new Vector3(0.039f, 0.039f, 0.039f);
        transform.localScale = newScale;

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
                Vector3 position = new Vector3(x * cellSize, y * cellSize, 0);
                Tile newTile = Instantiate(tilePrefab);
                _grid[x, y] = newTile;

                newTile.transform.parent = transform;
                newTile.transform.localPosition = position;
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

            // Eðer bu konumda zaten bir mayýn varsa tekrar seçmek için i'yi azalt
            if (minePositions.Contains(new Vector2Int(x, y)))
            {
                i--;
            }
            else
            {
                minePositions.Add(new Vector2Int(x, y));
                _grid[x, y].Prepare(UnitState.Mine); // Mayýný prepare etmek için tile'ýn UnitState'ini Mine olarak ayarlýyoruz
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
