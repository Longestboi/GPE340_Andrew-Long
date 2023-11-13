using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public abstract class Shooter : MonoBehaviour
{
    #region Fields
    [HideInInspector]
    /// <summary>The owner of this shooter</summary>
    public Weapon owner;

    [Tooltip("Number of bullets per second"), Range(0, 50)]
    /// <summery>The rate fire in bullets per second</summary>
    public int fireRate;

    /// <summary>Damage delt by the shooter</summary>
    public int damageAmount;

    /// <summary>Tracks if the shooter is shooting</summary>
    private bool isShooting = false;
    #endregion Fields

    #region MonoBehaviour
    private void Start()
    {   
        // Set the owner of this shooter
        owner = GetComponent<Weapon>();
    }
    #endregion MonoBehaviour
    
    #region Shooter
    /// <summary>The function that shoots anything from the shooter</summary>
    public abstract void Shoot();

    /// <summary>Start the coroutine for automatic shooting</summary>
    public void StartShootAuto()
    {
        // Stops the player from shooting at 2X the firerate when holding both inputs.
        if (isShooting) return;
        isShooting = true;

        // Start the coroutine to shoot automatically
        StartCoroutine(ShootAtInterval());
    }

    /// <summary>Stop the coroutine for automatic shooting</summary>
    public void StopShootAuto()
    {
        // Stop code from running if shooter isn't shooting
        if (!isShooting) return;
        isShooting = false;
    }

    /// <summary>The coroutine for automatic shooting</summary>
    private IEnumerator ShootAtInterval()
    {
        while(isShooting)
        {
            Shoot();
            
            // Wait for the firerate interval to elapse
            yield return new WaitForSecondsRealtime(1f / fireRate);
        }
    }

    public bool IsShooting() {
        return isShooting;
    }
    #endregion Shooter
}
