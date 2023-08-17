using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficulityManager : MonoBehaviour
{
    public Slider difficultySlider;

    public static DifficulityManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        difficultySlider = GetComponent<Slider>();
    }

    public void UpdateGridSize()
    {
        int selectedDifficulty = Mathf.RoundToInt(difficultySlider.value);

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
