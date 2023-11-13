using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    #region Fields
    /// <summary>Movement speed of the projectile</summary>
    public float speed;
    
    [HideInInspector]
    // The thing that shot this projectile
    public Controller instigator;
    
    [HideInInspector]
    /// <summary>The owner of this projectile</summary>
    public Shooter owner;
    #endregion Fields

    #region Projectile
    /// <summary>Move the projectile</summary>
    public abstract void Move();
    #endregion Projectile
}
