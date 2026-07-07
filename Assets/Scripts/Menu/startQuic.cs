using UnityEngine;
using UnityEngine.SceneManagement;
public class startQuic : MonoBehaviour
{
    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void OpenTwitter()
    {
        Application.OpenURL("https://x.com/foxy_runn");
    }

    public void QuitGame()
    {
        Application.Quit();
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
