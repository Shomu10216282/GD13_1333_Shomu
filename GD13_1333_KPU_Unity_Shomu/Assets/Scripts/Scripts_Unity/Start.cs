using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public string sceneToLoad = "MainGame";

    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}