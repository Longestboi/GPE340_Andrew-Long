using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    #region Fields
    [Header("UI")]
    /// <summary>Dropdown for the resolution settings</summary>
    public TMP_Dropdown resolutionDd;
    /// <summary>Dropdown for the quality settings</summary>
    public TMP_Dropdown qualityDd;
    /// <summary>Toggle button for fullscreen setting</summary>
    public Toggle fullscreenTg;

    [Space]
    /// <summary>Slider for the Master volume setting</summary>
    public Slider masterVolume;
    /// <summary>Slider for the Music volume setting</summary>
    public Slider musicVolume;
    /// <summary>Slider for the Sound volume setting</summary>
    public Slider soundVolume;

    [Space]
    /// <summary>Button that exits the options menu</summary>
    public Button exitButton;
    /// <summary>Hacky way of exiting the menu if clicking outside the options area</summary>
    public Button backgroundCapture;

    [Header("Data")]
    /// <summary>Audio mixer so volume levels can be set</summary>
    public AudioMixer audioMixer;
    #endregion Fields

    #region MonoBehaviour
    // Start is called before the first frame update
    void Start()
    {
        // Get the volume levels
        float maVol = PlayerPrefs.GetFloat("MasterVolume");
        float muVol = PlayerPrefs.GetFloat("MusicVolume");
        float soVol = PlayerPrefs.GetFloat("SoundVolume");

        // List of all resolutions
        List<string> resolutions = new List<string>();
        // Parse resolution to strings 
        Screen.resolutions.ToList().ForEach(x => resolutions.Add($"{x.width}x{x.height}"));
        resolutions.Reverse();

        // Add all resolutions to resolution drop down 
        resolutionDd.AddOptions(resolutions);

        // Find the current resolution's index, then set the drop down value to it.
        resolutionDd.options.ForEach(x => {
            if (x.text == PlayerPrefs.GetString("Resolution"))
                resolutionDd.value = resolutionDd.options.IndexOf(x);
        });

        List<string> qSettings = QualitySettings.names.ToList();
        qSettings.Reverse();

        // Add all quality settings to the quality drop down
        qualityDd.AddOptions(qSettings);

        // Find the current quality's index, then set the drop down value to it.
        qualityDd.options.ForEach(x => {
            if (x.text == PlayerPrefs.GetString("Quality"))
                qualityDd.value = qualityDd.options.IndexOf(x);
        });

        // Set slider position
        masterVolume.value = maVol;
        musicVolume.value = muVol;
        soundVolume.value = soVol;

        // Set audio levels from PlayerPrefs
        audioMixer.SetFloat("MasterVolume", maVol);
        audioMixer.SetFloat("MusicVolume", muVol);
        audioMixer.SetFloat("SoundVolume", soVol);

        // Set the listeners
        resolutionDd.onValueChanged.AddListener(SetResolution);
        qualityDd.onValueChanged.AddListener(SetQuality);
        fullscreenTg.onValueChanged.AddListener(SetFullscreen);

        masterVolume.onValueChanged.AddListener(SetMasterVolume);
        musicVolume.onValueChanged.AddListener(SetMusicVolume);
        soundVolume.onValueChanged.AddListener(SetSoundVolume);
        
        exitButton.onClick.AddListener(ExitOptions);
        backgroundCapture.onClick.AddListener(ExitOptions);
    }
    #endregion MonoBehavior

    #region OptionsUI
    /// <summary>Callback for setting the resolution of the game</summary>
    /// <param name="index">The index of the selected element from the drop down</param>
    public void SetResolution(int index)
    {
        Util.SetResolutionFromString(resolutionDd.options[index].text);
    }
    
    /// <summary>Callback for setting the quality of the game</summary>
    /// <param name="index">The index of the selected element from the drop down</param>
    public void SetQuality(int index)
    {
        Util.SetQualityFromString(qualityDd.options[index].text);
    }

    /// <summary>Callback for setting the application to fullscreen</summary>
    /// <param name="fullscreen">true - fullscreen, false - windowed</param>
    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
        PlayerPrefs.SetString("Fullscreen", fullscreen ? "True" : "False");
    }

    /// <summary>Callback for setting the volume of the Master group in the audio mixer</summary>
    /// <param name="vol">Volume in Decibels</param>
    public void SetMasterVolume(float vol)
    {
        if (vol <= -40) 
            audioMixer.SetFloat("MasterVolume", -100f);
        else
            audioMixer.SetFloat("MasterVolume", vol);

        PlayerPrefs.SetFloat("MasterVolume", vol);
    }
    
    /// <summary>Callback for setting the volume of the Music group in the audio mixer</summary>
    /// <param name="vol">Volume in Decibels</param>
    public void SetMusicVolume(float vol)
    {
        if (vol <= -40) 
            audioMixer.SetFloat("MusicVolume", -100f);
        else
            audioMixer.SetFloat("MusicVolume", vol);
        
        PlayerPrefs.SetFloat("MusicVolume", vol);
    }
    
    /// <summary>Callback for setting the volume of the Sound group in the audio mixer</summary>
    /// <param name="vol">Volume in Decibels</param>
    public void SetSoundVolume(float vol)
    {
        if (vol <= -40) 
            audioMixer.SetFloat("SoundVolume", -100f);
        else
            audioMixer.SetFloat("SoundVolume", vol);

        PlayerPrefs.SetFloat("SoundVolume", vol);
    }

    /// <summary>Callback for exiting the options menu</summary>
    public void ExitOptions()
    {
        Destroy(gameObject);
    }
    #endregion MonoBehaviour
}
