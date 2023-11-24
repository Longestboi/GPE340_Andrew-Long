using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;


[System.Serializable]
public class WaveData
{
    #region Fields
    [Header("Wave")]
    [Tooltip("The number of enemies that can spawn at one time")]
    /// <summary>The number of enemies that can spawn at one time</summary>
    public uint currentSpawnedEnemyLimit;
    public int numSpawnedEnemies = 0;

    [Tooltip("The enemies that will be spawned during this wave")]
    /// <summary>The enemies that will be spawned during this wave</summary>
    public List<EnemyData> enemies;

    [HideInInspector]
    public WaveManager waveManager;
    #endregion Fields

    #region WaveData
    /// <summary>Check to see if the current wave is complete</summary>
    /// <returns>Whether the wave has been completed</returns>
    public bool IsWaveCompleted()
    {
        // Filter list of enemies if they're dead,
        // if the length of the list trueis zero, the wave is over
        return enemies.Where(x => x.isDead == false).Count() == 0;
    }

    /// <summary>Init an enemy until the enemy limit is hit</summary>
    /// <param name="position">The position that enemies will spawn</param>
    public void InitEnemiesUntilLimit(Transform position)
    {
        if (numSpawnedEnemies == currentSpawnedEnemyLimit) return;
        var f = enemies.FirstOrDefault(x => !x.isInstanced);
        if (f == null) return;
        
        f.Construct(position, waveManager.gameManager.playerController.pawn.transform);
    }

    /// <summary>Update enemy data like death and initialization stuff</summary>
    public void UpdateEnemies()
    {
        // Possibly one of the worst ways of doing this, but LINQ!!1

        // Destroy the enemy if it's dead
        Predicate<EnemyData> OnRemove = (EnemyData ed) => {
            bool shouldDie = ed.isDead && ed.isInstanced;

            if (shouldDie)
                Object.Destroy(ed.aiController);

            return shouldDie;
        };

        // Count the current number of enemies
        numSpawnedEnemies = enemies.Count(x => x.isInstanced);
        // Determine if the enemy is dead
        enemies.ForEach(ed => ed.isDead = ed.enemy == null && ed.isInstanced);
        // Remove the enemy if it's dead and initialized
        numSpawnedEnemies -= enemies.RemoveAll(OnRemove);
    }
    #endregion WaveData
}
