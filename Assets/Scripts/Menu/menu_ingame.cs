using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
public class menu_ingame : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas;

    [Header("Звук")]
    [SerializeField] private Slider volumeSlider;

    [Header("Графика")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    private bool isPaused;
    private Resolution[] resolutions;

    void Start()
    {
        if (pauseCanvas != null)
            pauseCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    void Pause()
    {
        isPaused = true;
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;

        SetupSliders();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        isPaused = false;
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void SetupSliders()
    {
        // ===== ГРОМКОСТЬ =====
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveAllListeners();
            volumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        // ===== РАЗРЕШЕНИЕ =====
        if (resolutionDropdown != null)
        {
            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();

            List<string> options = new List<string>();
            int savedIndex = PlayerPrefs.GetInt("ResolutionIndex", -1);
            int currentIndex = 0;

            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);

                if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                    currentIndex = i;
            }

            if (savedIndex >= 0 && savedIndex < resolutions.Length)
                currentIndex = savedIndex;

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentIndex;
            resolutionDropdown.RefreshShownValue();

            resolutionDropdown.onValueChanged.RemoveAllListeners();
            resolutionDropdown.onValueChanged.AddListener(SetResolution);
        }

        // ===== ФУЛЛСКРИН =====
        if (fullscreenToggle != null)
        {
            fullscreenToggle.onValueChanged.RemoveAllListeners();
            fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
            fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        }
    }

    void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    void SetResolution(int index)
    {
        Resolution res = resolutions[index];
        bool fullscreen = fullscreenToggle != null ? fullscreenToggle.isOn : Screen.fullScreen;
        Screen.SetResolution(res.width, res.height, fullscreen);
        PlayerPrefs.SetInt("ResolutionIndex", index);
    }

    void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
        PlayerPrefs.SetInt("Fullscreen", fullscreen ? 1 : 0);
    }
}
