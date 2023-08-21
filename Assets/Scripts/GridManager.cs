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
    private List<Vector2Int> minePositions = new(); // Mayýn konumlarýný tutacak liste

    private float _cellSize = 2.5f;
    private Tile[,] _grid;

    public Tile tilePrefab;
    private bool isGameOver = false;
    public float minePercentage = 0.2f; // Mayýn yüzdesi
    
    private List<Vector2Int> clickedTiles = new(); //Týklanan kareleri tutacak liste

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
            _difficultyTypeToGridSize[(DifficultyType) selectedDifficulty];

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
        
        UIController.Instance.GetDifficultySlider().onValueChanged.AddListener(SetGridSize);
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
                //_grid[x, y].tileController.UpdateMineCount();
            }
        }

        /*Vector3 centerOffset = new Vector3(gridSizeX * _cellSize * 0.5f, gridSizeY * _cellSize * 0.5f, 0);
        _gridStartPositionTransform.position = -centerOffset;*/
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

            // Eðer burada zaten mayýn varsa tekrar seç i'yi azalt
            if (MinePositions.Contains(new Vector2Int(x, y)))
            {
                i--;
            }
            else
            {
                MinePositions.Add(new Vector2Int(x, y));
                _grid[x, y].Prepare(UnitState.Mine); //tile'ýn UnitState'ini Mine burada yapýyoruz
            }
        }
    }

    private void PrintMinePositions()
    {
        foreach (var position in MinePositions)
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

    private void GridTransformController(Transform GridManagerTransform) // yer ayarlama for grid clones
    {
        if (_gridSizeX == 5 && _gridSizeY == 5)
        {
            Vector3 newPosition = new Vector3(-4.5f, -2.2f, -8f);
            GridManagerTransform.position = newPosition;
        }
        
        else if (_gridSizeX == 7 && _gridSizeY == 7)
        {
            Vector3 newPosition = new Vector3(-4.5f, -2.2f, -8f);
            GridManagerTransform.position = newPosition;
            Vector3 newScale = new Vector3(0.7f, 0.7f, 0.7f);
            GridManagerTransform.localScale = newScale;
            Debug.Log("<color=blue> ben 7'im ve yerime oturdum tatlim</color>");
        }
 
        else if(_gridSizeX == 10 && _gridSizeY == 10)
        {
            Vector3 newPosition = new Vector3(-5.5f, -2.2f, -9f);
            GridManagerTransform.position = newPosition;
            Vector3 newScale = new Vector3(0.5f, 0.5f, 0.5f);
            GridManagerTransform.localScale = newScale;
            Debug.Log("<color=cyan> ben 10um ama onsuzum..</color>");
        }
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

    public int GetGridSizeX => _gridSizeX;
    public int GetGridSizeY => _gridSizeY;
}
