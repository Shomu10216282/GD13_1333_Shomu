using UnityEngine;
using UnityEngine.SceneManagement;

public class  MonoBehaviour
{
    public string sceneToLoad = "MainGame";

    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}