using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown qualityDropdown;
    public TMP_Dropdown textureDropdown;
    public Slider volumeSlider;
    float currentVolume;
    Resolution[] resolutions;
    
    public GameObject mainScreen;
    public GameObject optionsScreen;
    public GameObject confirmMenu;
    
    public Button continueButton;
    public Button playButton;
    public Button optionsButton;
    public Button quitButton;

    private bool _confirmButtonsPressed;
    private bool _confirmDelete;

    private void Start()
    {
        resolutionDropdown.ClearOptions();
        var options = new List<string>();
        resolutions = Screen.resolutions;
        var currentResolutionIndex = 0;
        
        for (var i = 0; i < resolutions.Length; i++)
        {
            var option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) currentResolutionIndex = i;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
        LoadSettings(currentResolutionIndex);

        if (ES3.FileExists())
        {
            continueButton.interactable = true;
        }
        else
        {
            continueButton.interactable = false;
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Tavern");
    }

    public void StartNewGame()
    {
        if (ES3.FileExists())
        {
            ShowConfirmMenu();
            StartCoroutine(WaitForConfirmationThenStart());
        }
        else
        {
            SceneManager.LoadScene("Tavern");
        }
    }

    private IEnumerator WaitForConfirmationThenStart()
    {
        yield return new WaitUntil(() => _confirmButtonsPressed);

        if (_confirmDelete)
        {
            ES3.DeleteFile();
            SceneManager.LoadScene("Tavern");
        }
        else
        {
            _confirmButtonsPressed = false;
            HideConfirmMenu();
        }
    }

    public void ConfirmDelete(bool confirm)
    {
        _confirmButtonsPressed = true;
        _confirmDelete = confirm;
        HideConfirmMenu();
    }

    public void ShowConfirmMenu()
    {
        confirmMenu.SetActive(true);
    }

    public void HideConfirmMenu()
    {
        confirmMenu.SetActive(false);
    }

    public void ShowOptions()
    {
        mainScreen.SetActive(false);
        optionsScreen.SetActive(true);
    }

    public void HideOptions()
    {
        mainScreen.SetActive(true);
        optionsScreen.SetActive(false);
    }
    
    public void SetTextureQuality(int textureIndex)
    {
        QualitySettings.masterTextureLimit = textureIndex;
        qualityDropdown.value = 6;
    }

    public void SetQuality(int qualityIndex)
    {
        if (qualityIndex != 6) // if the user is not using 
            //any of the presets
            QualitySettings.SetQualityLevel(qualityIndex);
        switch (qualityIndex)
        {
            case 0: // quality level - very low
                textureDropdown.value = 3;
                break;
            case 1: // quality level - low
                textureDropdown.value = 2;
                break;
            case 2: // quality level - medium
                textureDropdown.value = 1;
                break;
            case 3: // quality level - high
                textureDropdown.value = 0;
                break;
            case 4: // quality level - very high
                textureDropdown.value = 0;
                break;
            case 5: // quality level - ultra
                textureDropdown.value = 0;
                break;
        }
        
        qualityDropdown.value = qualityIndex;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, 
            resolution.height, Screen.fullScreen);
    }
     	
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        currentVolume = volume;
    }

    public void SetSliderPercent(float volume)
    {
        volumeSlider.value = currentVolume / 1f;
    }
    
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    
    public void SaveSettings()
    {
        PlayerPrefs.SetInt("QualitySettingPreference", qualityDropdown.value);
        PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
        PlayerPrefs.SetInt("TextureQualityPreference", textureDropdown.value);
        PlayerPrefs.SetInt("FullscreenPreference", Convert.ToInt32(Screen.fullScreen));
        PlayerPrefs.SetFloat("VolumePreference", 
            currentVolume); 
    }
    
    public void LoadSettings(int currentResolutionIndex)
    {
        if (PlayerPrefs.HasKey("QualitySettingPreference")) qualityDropdown.value = PlayerPrefs.GetInt("QualitySettingPreference");
        else qualityDropdown.value = 3;
        if (PlayerPrefs.HasKey("ResolutionPreference")) resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference");
        else resolutionDropdown.value = currentResolutionIndex;
        if (PlayerPrefs.HasKey("TextureQualityPreference")) textureDropdown.value = PlayerPrefs.GetInt("TextureQualityPreference");
        else textureDropdown.value = 0;
        if (PlayerPrefs.HasKey("FullscreenPreference")) Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
        else
            Screen.fullScreen = true;
        if (PlayerPrefs.HasKey("VolumePreference")) volumeSlider.value = PlayerPrefs.GetFloat("VolumePreference");
        else volumeSlider.value = PlayerPrefs.GetFloat("VolumePreference");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
