using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    public Button exitButton;

    private PlayerUI playerUI;

    // Start is called before the first frame update
    void Start()
    {
        playerUI = FindAnyObjectByType<PlayerUI>();
        
        if (playerUI == null) return;
        
        playerUI.gameObject.SetActive(false);

        exitButton.onClick.AddListener(GoToMenu);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Scenes/Main Menu");
    }
}
