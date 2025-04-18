using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public Slider volumeSlider; 
    public TMP_Dropdown resolutionDropdown; 
    public Toggle fullscreenToggle;
    public GameObject settingsPanel;

    private Resolution[] resolutions;

    void Start()
    {
        
        volumeSlider.value = AudioListener.volume;


        resolutionDropdown.ClearOptions();
        var options = new System.Collections.Generic.List<string>();
        var customResolutions = new Resolution[]
            {
        new Resolution { width = 1280, height = 720 },
        new Resolution { width = 1600, height = 900 },
        new Resolution { width = 1920, height = 1080 },
        new Resolution { width = 2560, height = 1440 }
            };

        resolutions = customResolutions;

        foreach (var res in resolutions)
        {
            options.Add(res.width + " x " + res.height);
        }

        resolutionDropdown.AddOptions(options);

        
        int currentResolutionIndex = GetCurrentResolutionIndex();
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        
        fullscreenToggle.isOn = Screen.fullScreen;
    }

    
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    
    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }


    private int GetCurrentResolutionIndex()
    {
        int currentWidth = Screen.width;
        int currentHeight = Screen.height;
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == currentWidth && resolutions[i].height == currentHeight)
                return i;
        }
        return 0; 
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}