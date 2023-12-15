using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Canvas Elements")]
    public GameObject introScreen;
    public GameObject menuScreen;

    [Header("Sub Scenes")]
    public GameObject introScene;
    public GameObject menuScene;

    [Header("Data")]
    private Camera introCamera;
    private Camera menuCamera;

    public AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        SetSettingsFromPlayerPrefs();

        /* Debug.Log(
            $"Resolution: {PlayerPrefs.GetString("Resolution")}\n" +
            $"Quality: {PlayerPrefs.GetString("Quality")}\n" +
            $"Fullscreen: {PlayerPrefs.GetString("Fullscreen")}\n\n" +
            $"Master Volume: {PlayerPrefs.GetFloat("MasterVolume")}\n" +
            $"Music Volume: {PlayerPrefs.GetFloat("MusicVolume")}\n" +
            $"Sound Volume: {PlayerPrefs.GetFloat("SoundVolume")}"
        ); */

        // Get the Cameras in each 'scene'
        introCamera = introScene.GetComponentInChildren<Camera>();
        menuCamera = menuScene.GetComponentInChildren<Camera>();

        // Set the menu camera to inactive, we don't want two audio listeners...
        menuCamera.gameObject.SetActive(false);
        // Add swap Scene to intro button
        introScreen.GetComponentInChildren<Button>().onClick.AddListener(SwapToMenuScene);
    }

    private void SetSettingsFromPlayerPrefs()
    {
        // Set the settings from PlayerPrefs...
        Screen.fullScreen = (PlayerPrefs.GetString("Fullscreen", "True") == "True") ? true : false;
        Util.SetResolutionFromString(PlayerPrefs.GetString("Resolution"));

        string quality = PlayerPrefs.GetString("Quality");
        Util.SetQualityFromString((quality != "") ? quality : "Ultra");

        audioMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
        audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        audioMixer.SetFloat("SoundVolume", PlayerPrefs.GetFloat("SoundVolume"));
    }

    public void SwapToMenuScene()
    {
        // Set the intro stuff to inactive
        introCamera.gameObject.SetActive(false);
        introScreen.gameObject.SetActive(false);
        
        // Set the menu stuff to active
        menuCamera.gameObject.SetActive(true);
        menuScreen.gameObject.SetActive(true);
    }
}
