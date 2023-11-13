using System.Collections;
using UnityEngine;

public class ProjectileShooter : Shooter
{
    #region Fields
    [Tooltip("The prefab that will be shot")]
    /// <summary>The projectile that this shooter shoots</summary>
    public Projectile projectilePrefab;
    #endregion Fields

    #region Shooter
    public override void Shoot()
    {
        // Null gaurd clause
        if (!owner || !owner.firePosition) return;

        // Gen the projectile at fireposition
        Projectile proj = Instantiate(
            projectilePrefab, owner.firePosition.position,
            owner.firePosition.rotation
        );

        proj.transform.Rotate(0, owner.GetNextShotAccuracy(), 0);

        // Set the owner of this projectile
        proj.owner = GetComponent<Shooter>();
        // What hell have I brought upon this land?
        // Get the owner of the weapon that hosts this shooter
        proj.instigator = owner.owner;
    }
    #endregion Shooter
}
