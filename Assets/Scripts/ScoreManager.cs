using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private int currentScore = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        currentScore += points;
        Debug.Log($"Score added: {points}. Total score: {currentScore}");
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void ResetScore()
    {
        currentScore = 0;
    }
}
