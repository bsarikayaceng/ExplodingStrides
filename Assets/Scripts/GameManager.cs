using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Slider difficultySlider;
    public GameObject gameOver;

    private void Awake()
    {
        Instance = this;
    }
    public void PlayGame()
    {
        DifficulityManager difficulityManager = GetComponent<DifficulityManager>();
        if (difficulityManager != null)
        {
            DifficulityManager.Instance.difficultySlider.value = difficultySlider.value;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        print("rimelim akmis ayol");
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
    }
}
