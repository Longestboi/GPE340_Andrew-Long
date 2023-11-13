using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    #region Fields
    public List<WaveData> waves;
    public List<SpawnArea> spawnAreas;

    public int currentWave = 0;
    public GameManager gameManager;
    #endregion Fields

    #region MonoBehaviour
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        spawnAreas = FindObjectsByType<SpawnArea>(FindObjectsSortMode.None).ToList();
        gameManager.waveManager = this;

        waves.ForEach(x => x.waveManager = this);
    }

    // Update is called once per frame
    void Update()
    {   
        waves[currentWave].InitAllotedEnemies(
            spawnAreas.OrderBy(x => Guid.NewGuid()).FirstOrDefault().transform
        );
        waves[currentWave].UpdateEnemies();
    }
    #endregion MonoBehaviour

    #region WaveManager
    #endregion WaveManager
}
