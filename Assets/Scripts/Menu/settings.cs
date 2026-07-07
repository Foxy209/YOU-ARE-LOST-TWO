using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class settings : MonoBehaviour
{
    public static settings Instance;

    [SerializeField] private Slider volumeSlider;

    private float volume = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        volume = PlayerPrefs.GetFloat("MasterVolume", 1f);
    }

    void Start()
    {
        if (volumeSlider != null)
            volumeSlider.value = volume;

        ApplyVolume();
    }

    public void SetVolume(float value)
    {
        volume = value;
        PlayerPrefs.SetFloat("MasterVolume", value);
        ApplyVolume();
    }

    void ApplyVolume()
    {
        AudioListener.volume = volume;
    }
}
