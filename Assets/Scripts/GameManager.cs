using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int openedGrassCounter;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    /*private void OnEnable()
    {
        if (UIController.Instance == null)
        {
            UIController uiController = FindObjectOfType<UIController>();
            
            uiController.GetNewGameButton().onClick.AddListener(PlayGame);
        }
        else
        {
            UIController.Instance.GetNewGameButton().onClick.AddListener(PlayGame);   
        }
    }*/
    
    private void Start()
    {
        UIController.Instance.GetNewGameButton().onClick.AddListener(PlayGame);
    }


    private void OnDisable()
    {
        UIController.Instance.GetNewGameButton().onClick.RemoveListener(PlayGame);
    }

    private void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GameOver() => UIController.Instance.ActivateGameOverPanel();
}
