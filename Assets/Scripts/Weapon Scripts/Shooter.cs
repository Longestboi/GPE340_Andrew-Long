using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public abstract class Shooter : MonoBehaviour
{
    #region Fields
    [HideInInspector]
    public Weapon owner;

    [Tooltip("Number of bullets per second"), Range(0, 50)]
    public float fireRate;
    #endregion Fields

    #region MonoBehaviour
    private void Start() {
        owner = GetComponent<Weapon>();
    }
    #endregion MonoBehaviour
    
    #region Shooter
    public abstract void Shoot();
    #endregion Shooter
}
