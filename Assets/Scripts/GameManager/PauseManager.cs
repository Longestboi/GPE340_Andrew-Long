using UnityEngine;

public class PauseManager: MonoBehaviour
{

    #region Fields
    /// <summary>Prefab of the pause menu</summary>
    public GameObject pauseMenuUIPrefab;
    /// <summary>Instanciated pause menu</summary>
    private GameObject pauseMenuUI;

    /// <summary>Reference to the game manager</summary>
    public GameManager gameManager;

    /// <summary>Used to track if the game is paused or not</summary>
    private bool _isPaused;
    #endregion Fields

    #region Properties
    /// <summary>Property to set the paused state</summary>
    /// <value>Boolean: true - pause, false - unpause</value>
    public bool pause {
        get { return _isPaused;}
        set { TogglePause(value); }
    }
    #endregion Properties

    #region MonoBehaviour
    void Start()
    {
        pauseMenuUI = Instantiate(pauseMenuUIPrefab, transform);

        pauseMenuUI.GetComponent<PauseUI>().pauseManager = this;
    }

    void Update()
    {
        // Use get button down to constant pausing and unpausing 
        if (Input.GetButtonDown("Pause") && !_isPaused)
            pauseMenuUI.SetActive(true);
        else if (Input.GetButtonDown("Pause") && _isPaused)
            pauseMenuUI.SetActive(false);
    }
    #endregion MonoBehaviour


    #region PauseManager
    /// <summary>Pause the game using timeScale</summary>
    public void Pause()
    {
        _isPaused = true;
        Time.timeScale = 0;
    }

    /// <summary>Unpause the game using timeScale</summary>
    public void Unpause()
    {
        _isPaused = false;
        Time.timeScale = 1;
    }

    /// <summary>Toggle if the game is paused based on the passed boolean</summary>
    /// <param name="pause">If true, pause the game else unpause</param>
    private void TogglePause(bool pause)
    {
        if (_isPaused = pause)
            Pause();
        else
            Unpause();
    }
    #endregion PauseManager
}
