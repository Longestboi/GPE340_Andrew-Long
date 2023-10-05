using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    #region Fields
    /// <summary>Movement speed of the projectile</summary>
    public float speed;
    /// <summary>Damage delt by the projectile</summary>
    public float damageDone;
    [HideInInspector]
    /// <summary>The owner of this projectile</summary>
    public Controller owner;
    #endregion Fields

    #region Projectile
    /// <summary>Move the projectile</summary>
    public abstract void Move();
    #endregion Projectile
}
