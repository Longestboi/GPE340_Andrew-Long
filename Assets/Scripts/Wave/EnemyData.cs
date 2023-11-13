using UnityEngine;


// Custom class because Unity's sensitve ass cannot handle drawing
// a list of tuples without a custom property drawer
[System.Serializable]
public class EnemyData
{
    public GameObject prefab;
    [HideInInspector]
    public GameObject enemy;
    public AiController aiControllerPrefab;

    [HideInInspector]
    public AiController aiController;

    [HideInInspector]
    public bool isDead = false;
    [HideInInspector]
    public bool isInstanced = false;

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