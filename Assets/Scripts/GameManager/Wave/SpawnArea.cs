using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    #region Fields
    private Bounds bounds;
    public Vector3 spawnArea;
    #endregion Fields

    #region MonoBehaviour
    void Start()
    {
        bounds.center = spawnArea;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, spawnArea);
        // Blue with half alpha
        Gizmos.color = new Color(0, 0, 1, .5f);
        Gizmos.DrawCube(transform.position, spawnArea);
    }
    #endregion MonoBehaviour

    #region PlayerSpawnArea
    public Vector3 GetRandomWorldPositionInArea()
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
    #endregion PlayerSpawnArea
}
