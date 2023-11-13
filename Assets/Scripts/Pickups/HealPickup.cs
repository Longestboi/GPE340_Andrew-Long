using UnityEngine;
using Utils;
public class HealthPickup : Pickup
{   
    #region Fields
    [Tooltip("The amount of health that the pickup gives")]
    /// <summary>The amount of health that the pickup gives</summary>
    public int healFactor = 10;
    #endregion Fields

    #region MonoBehaviour
    void OnTriggerEnter(Collider other)
    {
        Controller controller = isPlayerExclusive ? ControllerUTE.GetPlayerController(other.gameObject) : ControllerUTE.GetController(other.gameObject);
        Health pHealth;
        if (!controller || !controller.pawn) return;
        
        // Get pawn's health component from controller
        pHealth = controller.pawn.GetComponent<Health>();
        if (!pHealth) return;

        // Heal the pawn
        pHealth.Heal(healFactor);

        // KILL
        Destroy(gameObject);
    }
    #endregion MonoBehaviour
}
