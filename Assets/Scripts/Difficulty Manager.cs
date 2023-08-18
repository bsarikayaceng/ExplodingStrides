using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    /*private void OnEnable()
    {
        UIController.Instance.GetDifficultySlider().onValueChanged.AddListener(UpdateGridSize);
    }*/
    
    private void Start()
    {
        UIController.Instance.GetDifficultySlider().onValueChanged.AddListener(UpdateGridSize);
    }

    private void OnDisable()
    {
        UIController.Instance.GetDifficultySlider().onValueChanged.RemoveListener(UpdateGridSize);
    }

    private void UpdateGridSize(float sliderValue)
    {
        int selectedDifficulty = Mathf.RoundToInt((int) sliderValue);

        int gridSizeX = 5; // Default
        int gridSizeY = 5; 

            if (selectedDifficulty == 1) // Easy
            {
                gridSizeX = 5;
                gridSizeY = 5;
            }
            else if (selectedDifficulty == 2) // Medium
            {
                gridSizeX = 7;
                gridSizeY = 7;
            }
            else if (selectedDifficulty == 3) // Hard
            {
                gridSizeX = 10;
                gridSizeY = 10;
            }

            PlayerPrefs.SetInt("Grid Size X", gridSizeX);
            PlayerPrefs.SetInt("Grid Size Y", gridSizeY);
        print(PlayerPrefs.GetInt("Grid Size X"));
    }
}
