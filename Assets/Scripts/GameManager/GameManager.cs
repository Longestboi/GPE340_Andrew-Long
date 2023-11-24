using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public WaveManager waveManager;
    public PauseManager pauseManager;
    public PlayerController playerController;

    [SerializeField]
    private List<WeaponIconStruct> _weaponIcons;
    public Dictionary<WeaponType, Sprite> WeaponIcons = new Dictionary<WeaponType, Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        // Get Submanagers
        pauseManager = GetComponentInChildren<PauseManager>();
        waveManager = GetComponentInChildren<WaveManager>();

        playerController = FindAnyObjectByType<PlayerController>();

        // Set the back references
        if (pauseManager != null) pauseManager.gameManager = this;
        if (waveManager != null) waveManager.gameManager = this;

        // Player UI
        // Debug.Log(name + ":GM:" + playerController.transform.GetChild(0).GetComponent<Canvas>());
        var temp = playerController.transform.GetChild(0).GetComponent<Canvas>();
        temp.gameObject.SetActive(true);

        foreach (WeaponIconStruct wis in _weaponIcons)
            WeaponIcons.Add(wis.weaponType, wis.image);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.lives <= 0 && !playerController.pawn) {
            Debug.Log("Game Over!");
            
            ExitGame();
        }
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        
        Application.Quit(0);
    }
}
