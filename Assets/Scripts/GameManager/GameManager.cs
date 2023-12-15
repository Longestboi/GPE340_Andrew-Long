using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public WaveManager waveManager;
    public PauseManager pauseManager;
    public PlayerController playerController;

    [SerializeField]
    private List<WeaponIconStruct> _weaponIcons;
    public Dictionary<WeaponType, Sprite> WeaponIcons = new Dictionary<WeaponType, Sprite>();

    public bool hasWon;

    public Object winScreen;

    // Start is called before the first frame update
    void Start()
    {
        if (instance) DestroyImmediate(gameObject);
        else instance = this;

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
        if (
            waveManager.currentWave == waveManager.waves.Count - 1 &&
            waveManager.waves[waveManager.currentWave].enemies.Count == 0 &&
            !hasWon
            )
        {
            Instantiate(winScreen, GetComponentInChildren<Canvas>().transform);
            playerController.Unpossess(playerController.pawn);
            hasWon = true;
        }
        

        if (playerController.lives <= 0 && !playerController.pawn && !hasWon) {
            Debug.Log("Game Over!");
            
            Util.ExitGame();
        }
    }
}
