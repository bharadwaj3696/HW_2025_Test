using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Panels")]
    [SerializeField] GameObject startScreen;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject gameplayUI;

    [Header("Game Over UI")]
    [SerializeField] TextMeshProUGUI finalScoreText;

    public enum GameState
    {
        Start,
        Playing,
        GameOver
    }

    public GameState currentState { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        currentState = GameState.Start;
    }

    void Start()
    {
        ShowStartScreen();
    }

    void ShowStartScreen()
    {
        currentState = GameState.Start;
        Time.timeScale = 0f; // Pause game

        if (startScreen != null) startScreen.SetActive(true);
        if (gameOverScreen != null) gameOverScreen.SetActive(false);
        if (gameplayUI != null) gameplayUI.SetActive(false);
    }

    public void StartGame()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1f; // Resume game

        if (startScreen != null) startScreen.SetActive(false);
        if (gameOverScreen != null) gameOverScreen.SetActive(false);
        if (gameplayUI != null) gameplayUI.SetActive(true);
    }

    public void GameOver()
    {
        if (currentState == GameState.GameOver) return; // Prevent multiple calls

        currentState = GameState.GameOver;
        Time.timeScale = 0f; // Pause game

        if (gameOverScreen != null) gameOverScreen.SetActive(true);
        if (gameplayUI != null) gameplayUI.SetActive(false);

        // Show final score
        if (finalScoreText != null && ScoreManager.Instance != null)
        {
            finalScoreText.text = "Final Score: " + ScoreManager.Instance.GetScore();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
