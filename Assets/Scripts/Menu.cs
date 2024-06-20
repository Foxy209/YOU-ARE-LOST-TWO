using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
public class Menu : MonoBehaviour
{
    public void Ext()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
    public void Lsc(string s) => SceneManager.LoadScene(s);
}
