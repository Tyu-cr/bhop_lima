using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSlider;
    private Resolution[] _resolutions;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown qualityDropdown;

    private void Start()
    {
        var currentQuality = QualitySettings.GetQualityLevel();
        qualityDropdown.value = currentQuality;
        qualityDropdown.RefreshShownValue();

        Screen.fullScreen = true;

        masterVolumeSlider.onValueChanged.AddListener(OnSoundValue);
        OnSoundValue(masterVolumeSlider.value);

        _resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        var options = new List<string>();
        var current = 0;
        for (var i = 0; i < _resolutions.Length; i++)
        {
            var option = _resolutions[i].width + " x " + _resolutions[i].height;
            options.Add(option);
            if (_resolutions[i].width == Screen.width &&
                _resolutions[i].height == Screen.height)
            {
                current = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = current;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int index)
    {
        var resolution = _resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void OnSoundValue(float volume)
    {
        volume = Mathf.Clamp01(volume);
        AudioListener.volume = volume;
    }

    public void Fullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    public void Quality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}