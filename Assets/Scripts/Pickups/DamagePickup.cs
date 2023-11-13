using UnityEngine;
using Utils;

public class DamagePickup : Pickup
{   
    #region Fields
    [Tooltip("The amount of damage that the pickup does")]
    /// <summary>The amount of damage that the pickup does</summary>
    public int damageAmount = 0;
    #endregion Fields

    #region MonoBehaviour
    void OnTriggerEnter(Collider other)
    {
        // Environmental story telling 
        Controller controller = isPlayerExclusive ?
            ControllerUTE.GetPlayerController(other.gameObject) :
            ControllerUTE.GetController(other.gameObject);
        Health pHealth;
        if (!controller || !controller.pawn) return;
        
        pHealth = controller.pawn.GetComponent<Health>();
        if (!pHealth) return;

        pHealth.Damage(damageAmount);

        Destroy(gameObject);
    }
    #endregion MonoBehaviour
}
