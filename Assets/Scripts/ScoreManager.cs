using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private int _score = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public int  GetScore()          => _score;
    public void ResetScore()        => _score = 0;
    public void AddScore(int pts)   => _score += pts;
    public void SetScore(int score) => _score = score;
}