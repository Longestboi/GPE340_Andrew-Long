using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    #region Fields
    /// <summary>The Resume Button</summary>
    public Button unpause;
    /// <summary>The Options Button</summary>
    public Button options;
    /// <summary>The Exit to Menu Button</summary>
    public Button exit;

    /// <summary>The pause manager so we can pause</summary>
    public PauseManager pauseManager;

    #endregion Fields

    #region MonoBehaviour
    void Start()
    {
        // Get the PauseManager
        pauseManager = FindAnyObjectByType<PauseManager>();
        
        unpause.onClick.AddListener(Unpause);
        options.onClick.AddListener(SpawnOptions);
        exit.onClick.AddListener(DoExit);

        // Disable PauseUI to not immedately activate the pause menu on load
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        // Stop if pause manager isn't a thing
        if (!pauseManager) return;
        
        // Pause the game
        pauseManager.pause = true;
    }

    void OnDisable()
    {
        Unpause();
    }

    void OnDestroy()
    {
        Unpause();
    }
    #endregion MonoBehaviour

    #region PauseUI
    public void Unpause()
    {
        if (!pauseManager) return;
        // Unpause the game
        pauseManager.pause = false;
        
        // Deactivate the PauseUI
        gameObject.SetActive(false);
    }

    public void SpawnOptions()
    {
        Debug.Log(name + " TODO: implement the settings screen");
    }

    public void DoExit()
    {
        pauseManager.gameManager.ExitGame();
    }
    #endregion PauseUI
}
