using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    #region Fields
    /// <summary>The waves that are controlled by this wave manager</summary>
    public List<WaveData> waves;
    /// <summary>All spawn areas in this level</summary>
    public List<SpawnArea> spawnAreas;

    /// <summary>Current wave</summary>
    public int currentWave = 0;
    /// <summary>Reference to the game manager, mostly for player references</summary>
    public GameManager gameManager;
    #endregion Fields

    #region MonoBehaviour
    // Start is called before the first frame update
    void Start()
    {
        spawnAreas = FindObjectsByType<SpawnArea>(FindObjectsSortMode.None).ToList();

        waves.ForEach(x => x.waveManager = this);
    }

    // Update is called once per frame
    void Update()
    {   
        waves[currentWave].InitEnemiesUntilLimit(
            spawnAreas.OrderBy(x => Guid.NewGuid()).FirstOrDefault().transform
        );
        waves[currentWave].UpdateEnemies();
    }
    #endregion MonoBehaviour
}
