using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour
{
    #region Fields
    [Header("UI")]
    public Button start;
    public Button options;
    public Button exit;

    [Header("Audio Sources")]
    public AudioSource hover;
    public AudioSource click;

    [Header("Prefabs")]
    public Object optionsUI;
    public Object startingLevel;
    #endregion Fields

    #region MonoBehaviour
    // Start is called before the first frame update
    void Start()
    {
        // If any are null, meaning they are not set in editor, exit this function
        if (!start || !options || !exit || !click || !hover) return;

        start.onClick.AddListener(click.Play);
        options.onClick.AddListener(click.Play);
        exit.onClick.AddListener(click.Play);

        start.onClick.AddListener(StartGame);
        options.onClick.AddListener(Options);
        exit.onClick.AddListener(Exit);
    }
    #endregion MonoBehaviour

    #region MenuScreen
    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/Level 1");
    }

    public void Options()
    {
        Instantiate(optionsUI, transform.parent);
    }

    public void Exit()
    {
        Util.ExitGame();
    }
    #endregion MenuScreen
}
