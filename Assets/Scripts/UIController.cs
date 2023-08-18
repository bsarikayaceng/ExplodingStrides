using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [SerializeField]
    private Button _newGameButton;
    [SerializeField]
    private Slider _difficultySlider;
    [SerializeField]
    private GameObject _gameOverPanel;
    
    private void Awake()
    {
        Instance = this;
    }
    
    public Button GetNewGameButton() => _newGameButton;
    public Slider GetDifficultySlider() => _difficultySlider;

    public void ActivateGameOverPanel() => _gameOverPanel.SetActive(true);

    public void SetDifficultySliderValue(int selectedDifficulty) => _difficultySlider.value = selectedDifficulty;
}
