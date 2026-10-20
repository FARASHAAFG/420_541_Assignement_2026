using UnityEngine;
using UnityEngine.SceneManagement; // needed for SceneManager
 
public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
 
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
