using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
public class graffic : MonoBehaviour
{
    [Header("Разрешение")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    [Header("Режим экрана")]
    [SerializeField] private Toggle fullscreenToggle;

    private Resolution[] resolutions;
    private int currentResolutionIndex;
    private bool isFullscreen;

    void Start()
    {
        // Загружаем сохранённое
        isFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        currentResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", -1);

        // Получаем список разрешений
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // Ищем сохранённое разрешение
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height &&
                currentResolutionIndex == -1)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        if (fullscreenToggle != null)
            fullscreenToggle.isOn = isFullscreen;
    }

    public void SetResolution(int index)
    {
        currentResolutionIndex = index;
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, isFullscreen);
        PlayerPrefs.SetInt("ResolutionIndex", index);
    }

    public void SetFullscreen(bool fullscreen)
    {
        isFullscreen = fullscreen;
        Screen.fullScreen = fullscreen;
        PlayerPrefs.SetInt("Fullscreen", fullscreen ? 1 : 0);
    }
}
