using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanShooter : Shooter
{

    #region Shooter
    public override void Shoot()
    {
        // If I have not owner or firePosition, don't shoot
        if (!owner || !owner.firePosition) return;

        // Prepare for raycast consequences
        RaycastHit hitInfo;
        
        // Cast ray, if it hit anything do something
        if (
            Physics.Raycast(
                owner.firePosition.position,
                owner.firePosition.forward,
                out hitInfo
            )
        ){
            // Get health component from raycast info
            Health health = hitInfo.collider.GetComponent<Health>();
            if (!health) return;

            // Damage the target
            health.Damage(damageAmount);
        }

        // Debug draw
        Debug.DrawRay(owner.firePosition.position, owner.firePosition.forward, Color.red, 0.01f, true);
    }
    #endregion Shooter
}
