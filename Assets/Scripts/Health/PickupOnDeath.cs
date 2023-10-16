using UnityEngine;

public class PickupOnDeath : Death
{
    #region Fields
    /// <summary>The pickup that will be instancated on death</summary>
    [Tooltip("The pickup that will be instancated on death")]
    public Pickup pickup = null;
    
    /// <summary>To ensure the OnDie function runs once.</summary>
    private bool onceClause = false;
    #endregion Fields

    #region Death
    public override void OnDie()
    {
        // Run once gaurd clause
        if (onceClause) return;

        // Instantiate the pickup from a prefab and get object's position
        Pickup pup = Instantiate(pickup);
        Vector3 pos = transform.position;

        // Set the height to 1 unit above this transform's position
        pos.y += 1f;
        pup.transform.position = pos;

        // Toggle flag
        onceClause = true;
    }
    #endregion Death
}
