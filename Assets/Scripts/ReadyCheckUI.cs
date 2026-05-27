using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ReadyCheckUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject readyCheckPanel;
    [SerializeField] private Image timerRing;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private Button readyButton;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private float countdownDuration = 10f;

    private Coroutine countdownCoroutine;
    private bool playerIsReady = false;

    void Start()
    {
        readyCheckPanel.SetActive(false);
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);
    }

    public void StartReadyCheck()
    {
        readyCheckPanel.SetActive(true);
        playerIsReady = false;

        // Reset ring to full
        timerRing.fillAmount = 1f;

        if (countdownCoroutine != null) StopCoroutine(countdownCoroutine);
        countdownCoroutine = StartCoroutine(RunCountdown());
    }

    private IEnumerator RunCountdown()
    {
        float elapsed = 0f;

        while (elapsed < countdownDuration)
        {
            elapsed += Time.deltaTime;
            float remaining = countdownDuration - elapsed;

            // Drain the ring from full to empty
            timerRing.fillAmount = 1f - (elapsed / countdownDuration);
            countdownText.text = Mathf.CeilToInt(remaining).ToString();

            yield return null;
        }

        // Timer ran out without player hitting ready — go back to main menu
        OnTimerExpired();
    }

    // Hook this up to ReadyButton's OnClick in the Inspector
    public void OnReadyButtonPressed()
    {
        if (playerIsReady) return;
        playerIsReady = true;

        // Stop the countdown
        if (countdownCoroutine != null) StopCoroutine(countdownCoroutine);

        // Confirm visuals
        timerRing.fillAmount = 1f;
        countdownText.text = "GO!";
        readyButton.interactable = false;

        StartCoroutine(LoadGameAfterDelay(0.8f));
    }

    private IEnumerator LoadGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("BrodyBlasterTest");
    }

    private void OnTimerExpired()
    {
        readyCheckPanel.SetActive(false);
    }
}