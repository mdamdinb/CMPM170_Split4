using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private ReadyCheckUI readyCheckUI;

    public void OnPlayButtonPressed()
    {
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);

        if (readyCheckUI != null)
            readyCheckUI.StartReadyCheck();
    }
}