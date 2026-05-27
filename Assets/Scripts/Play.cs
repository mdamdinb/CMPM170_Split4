using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void OnShootButtonClicked()
    {
        SceneManager.LoadScene("BrodyBlasterTest");
    }
}