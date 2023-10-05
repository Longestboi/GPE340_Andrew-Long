using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : Shooter
{
    #region Fields
    /// <summary>The projectile that this shooter shoots</summary>
    public Projectile projectilePrefab;

    /// <summary>Tracks if the shooter is shooting</summary>
    private Coroutine shootingCoroutine = null;
    #endregion Fields

    #region Shooter
    public override void Shoot() {
        // Null gaurd clause
        if (owner == null || owner.firePosition == null) return;

        // Gen the projectile at fireposition
        Projectile proj = Instantiate<Projectile>(
            projectilePrefab, owner.firePosition.position,
            owner.firePosition.rotation
        );
    }
    #endregion Shooter

    #region ProjectileShooter
    /// <summary>Start the coroutine for automatic shooting</summary>
    public void StartShootAuto() {
        // Stops the player from shooting at 2X the firerate when holding both inputs.
        if (shootingCoroutine != null) return;

        // Start the coroutine to shoot automatically
        shootingCoroutine = StartCoroutine(ShootAtInterval());
    }

    /// <summary>Stop the coroutine for automatic shooting</summary>
    public void StopShootAuto() {
        // Stop code from running if there is no coroutine
        if (shootingCoroutine == null) return;

        StopCoroutine(shootingCoroutine);

        // Stops the player from shooting at 2X the firerate when holding both inputs.
        shootingCoroutine = null;
    }

    /// <summary>The coroutine for automatic shooting</summary>
    private IEnumerator ShootAtInterval() {
        while(true) {
            // Wait for the firerate interval to elapse
            yield return new WaitForSeconds(1 / fireRate);

            Shoot();
        }
    }
    #endregion ProjectileShooter
}
