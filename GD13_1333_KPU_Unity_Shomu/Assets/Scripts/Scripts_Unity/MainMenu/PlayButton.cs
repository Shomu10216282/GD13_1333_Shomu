using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public string sceneToLoad = "GameScene";

    public void OnPlayButtonPressed()   
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
