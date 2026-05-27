using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject readyCheckUI;

    public void OnPlayButtonPressed()
    {
        readyCheckUI.SetActive(true);
    }
}