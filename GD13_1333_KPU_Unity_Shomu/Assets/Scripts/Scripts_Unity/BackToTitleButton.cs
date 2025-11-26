using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToTitleButton : MonoBehaviour
{
    [Header("MainMenu")]
    public string titleSceneName = "MainMenu";

    public void OnBackToTitleButtonPressed()
    {
        SceneManager.LoadScene(titleSceneName);
    }
}
