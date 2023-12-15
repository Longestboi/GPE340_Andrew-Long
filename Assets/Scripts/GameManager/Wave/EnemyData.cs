using UnityEngine;


// Custom class because Unity's sensitve ass cannot handle drawing
// a list of tuples without a custom property drawer
[System.Serializable]
public class EnemyData
{
    /// <summary>Enemy Prefab</summary>
    public GameObject prefab;
    
    [HideInInspector]
    /// <summary>Instanciated enemy object</summary>
    public GameObject enemy;
    
    /// <summary>AI Controller prefab</summary>
    public AiController aiControllerPrefab;

    [HideInInspector]
    /// <summary>Instanciated AI Controller object</summary>
    public AiController aiController;

    [HideInInspector]
    /// <summary>To Track whether the enemy has been killed</summary>
    public bool isDead = false;
    
    [HideInInspector]
    /// <summary>To check if this enemy has been instanciated yet</summary>
    public bool isInstanced = false;

    /// <summary>Construct the Enemy at a location and with a target</summary>
    /// <param name="position">Postion the enemy will spawn</param>
    /// <param name="target">Enemy AI's target</param>
    public void Construct(Transform position, Transform target)
    {
        // Instance the enemy
        enemy = Object.Instantiate(prefab, position);
        enemy.name = "Enemy";
        isInstanced = true;
        
        // Instance the Ai controller
        aiController = Object.Instantiate(aiControllerPrefab, position);
        aiController.name = "Controller";
        
        // Make the Ai controller possess the pawn
        aiController.Possess(enemy.GetComponent<Pawn>());
        aiController.targetTransform = target;
        
    }
}