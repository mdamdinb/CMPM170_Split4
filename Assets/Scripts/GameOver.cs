using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance { get; private set; }

    [Header("Overlay Root")]
    public GameObject overlayPanel;       

    [Header("Text References")]
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI newHighScoreBanner; 

    [Header("Buttons")]
    public Button mainMenuButton;
    public Button playAgainButton;       

    [Header("Settings")]
    public string mainMenuSceneName = "MainMenu";

    private const string HIGH_SCORE_KEY = "HighScore";

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        if (overlayPanel != null)
            overlayPanel.SetActive(false);
    }

    void Start()
    {
        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(GoToMainMenu);

        if (playAgainButton != null)
            playAgainButton.onClick.AddListener(PlayAgain);
    }

    public void ShowGameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        int currentScore = ScoreManager.Instance != null ? ScoreManager.Instance.GetScore() : 0;
        int highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        bool isNewHigh = currentScore > highScore;

        if (isNewHigh)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, highScore);
            PlayerPrefs.Save();
        }

        if (currentScoreText != null) currentScoreText.text = currentScore.ToString();
        if (highScoreText != null) highScoreText.text = highScore.ToString();
        if (newHighScoreBanner != null) newHighScoreBanner.gameObject.SetActive(isNewHigh);

        overlayPanel.SetActive(true);
        StartCoroutine(AnimateIn());

        Time.timeScale = 0f;
    }

    public void HideGameOver()
    {
        Time.timeScale = 1f;
        overlayPanel.SetActive(false);
    }


    public void GoToMainMenu()
    {
        if (ScoreManager.Instance != null) ScoreManager.Instance.ResetScore();
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void PlayAgain()
    {
        if (ScoreManager.Instance != null) ScoreManager.Instance.ResetScore();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    IEnumerator AnimateIn()
    {
        CanvasGroup[] groups = overlayPanel.GetComponentsInChildren<CanvasGroup>();
        foreach (var cg in groups) cg.alpha = 0f;

        yield return new WaitForSecondsRealtime(0.1f);

        foreach (var cg in groups)
        {
            StartCoroutine(FadeIn(cg, 0.5f));
            yield return new WaitForSecondsRealtime(0.12f);
        }
    }

    IEnumerator FadeIn(CanvasGroup cg, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            cg.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
        cg.alpha = 1f;
    }
}