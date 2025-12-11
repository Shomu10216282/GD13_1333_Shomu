using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    [Header("MainGame")]
    public string gameSceneName = "Game";

    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
