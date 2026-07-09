using UnityEngine;
using UnityEngine.SceneManagement;
public class zagl_endLAB : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(perehod), 3);
    }

    private void perehod()
    {
        SceneManager.LoadScene("end_cutscene");
    }
    
}
