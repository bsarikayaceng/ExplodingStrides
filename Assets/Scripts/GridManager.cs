using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    [SerializeField]
    private Transform _gridStartPositionTransform;

    public List<Vector2Int> MinePositions { get => minePositions; set => minePositions = value; }

    private int _gridSizeX;
    private int _gridSizeY;

    public TextMeshProUGUI statusText;
    private List<Vector2Int> minePositions = new(); // May�n konumlar�n� tutacak liste

    private float _cellSize = 2.5f;
    private Tile[,] _grid;

    public Tile tilePrefab;
    private bool isGameOver = false;
    private bool isGameWin = false;
    public float minePercentage = 0.2f; // May�n y�zdesi

    //private int _clickedTileCount = 0;
    private int _totalNotClickedTiles;

    private List<Vector2Int> clickedTiles = new(); //T�klanan kareleri tutacak liste

    private Dictionary<DifficultyType, int> _difficultyTypeToGridSize = new()
    {
        { DifficultyType.Easy, 5 },
        { DifficultyType.Medium, 7 },
        { DifficultyType.Hard, 10 }
    };

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey(ConstantVariables.DifficultyKey))
            SetGridSize(1);

        int selectedDifficulty = PlayerPrefs.GetInt(ConstantVariables.DifficultyKey);

        UIController.Instance.SetDifficultySliderValue(selectedDifficulty);

        int selectedDifficultyGridSize =
            _difficultyTypeToGridSize[(DifficultyType)selectedDifficulty];

        _gridSizeX = selectedDifficultyGridSize;
        _gridSizeY = selectedDifficultyGridSize;

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            CreateGrid();
            Debug.Log("<color=magenta> Ben caliscam simdi</color>");
            GridTransformController(_gridStartPositionTransform);
            // Vector3 newPosition = new Vector3(-4.63f, 0f, -8.18f);
            //transform.position = newPosition;

            PlaceMines();
            PrintMinePositions();
            TileController.PrintNeighbourCoordinates();
        }
        int totalTiles = _gridSizeX * _gridSizeY;
        _totalNotClickedTiles = Mathf.RoundToInt(totalTiles - (totalTiles * minePercentage));
        UIController.Instance.GetDifficultySlider().onValueChanged.AddListener(SetGridSize);
        Debug.Log($"<color=aqua>totalTiles = {totalTiles}</color>");
    }

    internal void TileClickCount()
    {

        if (GameManager.Instance.openedGrassCounter-1 == _totalNotClickedTiles)
        {
            Win();
            Debug.Log("Oyun kazand�n!");
        }
        Debug.Log($"<color=lime>Not T�k t�k say�s� = {_totalNotClickedTiles}</color>");
    }
   
    private void OnDisable()
    {
        UIController.Instance.GetDifficultySlider().onValueChanged.RemoveListener(SetGridSize);
    }

    private void CreateGrid()
    {
        _grid = new Tile[_gridSizeX, _gridSizeY];

        for (int y = 0; y < _gridSizeY; y++)
        {
            for (int x = 0; x < _gridSizeX; x++)
            {
                Vector3 position = new Vector3(x * _cellSize, 0f, y * _cellSize);
                Tile newTile = Instantiate(tilePrefab, _gridStartPositionTransform.position,
                    Quaternion.Euler(Vector3.right * -90f), _gridStartPositionTransform);
                newTile.GetCoordinates(x,y);
                _grid[x, y] = newTile;

                newTile.transform.localPosition = position;
            }
        }
    }

    private void SetGridSize(float sliderValue)
    {
        PlayerPrefs.SetInt(ConstantVariables.DifficultyKey, (int) sliderValue);
        
        Debug.LogFormat($"<color=yellow>Difficulty set to {(DifficultyType) sliderValue}</color>");
    }

    private void PlaceMines()
    {
        int totalTiles = _gridSizeX * _gridSizeY;
        int totalMines = Mathf.RoundToInt(totalTiles * minePercentage);

        for (int i = 0; i < totalMines; i++)
        {
            int x = Random.Range(0, _gridSizeX);
            int y = Random.Range(0, _gridSizeY);

            // E�er burada zaten may�n varsa tekrar se� i'yi azalt
            if (MinePositions.Contains(new Vector2Int(x, y)))
            {
                i--;
            }
            else
            {
                MinePositions.Add(new Vector2Int(x, y));
                _grid[x, y].Prepare(UnitState.Mine); //tile'�n UnitState'ini Mine burada yap�yoruz
            }
        }
    }

    private void PrintMinePositions()
    {
        foreach (var position in MinePositions)
        {
            Debug.Log("May�n Burada X: " + position.x + ", Y: " + position.y);
        }
    }

    public Tile GetTileAtPosition(int x, int y)
    {
        return _grid[x, y];
    }


    private void GridTransformController(Transform GridManagerTransform) // yer ayarlama for grid clones
    {
        if (_gridSizeX == 5 && _gridSizeY == 5)
        {
            Initialize(GridManagerTransform, new Vector3(-4.5f, -2.2f, -8f), new Vector3(1, 1, 1));
        }
        
        else if (_gridSizeX == 7 && _gridSizeY == 7)
        {
            Initialize(GridManagerTransform, new Vector3(-5f, -2.2f, -8.5f), new Vector3(0.7f, 0.7f, 0.7f));
            Debug.Log("<color=blue> ben 7'im ve yerime oturdum tatlim</color>");
        }
 
        else if(_gridSizeX == 10 && _gridSizeY == 10)
        {
            Initialize(GridManagerTransform, new Vector3(-5.5f, -2.2f, -9f), new Vector3(0.5f, 0.5f, 0.5f));
            Debug.Log("<color=cyan> ben 10um ama onsuzum..</color>");
        }
    }


    private static void Initialize(Transform transform, Vector3 position, Vector3 scale)
    {
        transform.position = position;
        transform.localScale = scale;
    }


    internal bool CheckForWin()
    {
        return GameManager.Instance.openedGrassCounter-1 == _totalNotClickedTiles;
    }

    public void Win()
    {
        isGameWin = true;
        Debug.Log("You Win Bitch <3");
        GameManager.Instance.GameOver();
    }
    public void GameOver()
    {
        isGameOver = true;
        GameManager.Instance.GameOver();
        Debug.Log( "You Lost Loser :P");
    }

    public bool IsGameOver() => isGameOver;
    public bool IsGameWin() => isGameWin;
    public int GetGridSizeX => _gridSizeX;
    public int GetGridSizeY => _gridSizeY;
}
