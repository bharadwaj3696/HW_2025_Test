using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] TextMeshProUGUI scoreText;

    int score = 0;

    void Awake()
    {
        Instance = this;
        UpdateUI();
    }

    public void AddScore()
    {
        score++;
        UpdateUI();
    }

    public int GetScore()
    {
        return score;
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score : " + score;
    }
}
