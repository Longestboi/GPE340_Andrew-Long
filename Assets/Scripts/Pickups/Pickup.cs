using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Pickup : MonoBehaviour
{
    #region Fields
    [Tooltip("Allows for any pawn to interact with the pickup, or only the player")]
    /// <summary>Allows for any pawn to interact with the pickup, or only the player</summary>
    public bool isPlayerExclusive;
    #endregion Fields

    #region Pickup
    protected Controller GetController(Collider other)
    {
        // Get the pawn
        CharacterPawn pawn = other.GetComponent<CharacterPawn>();

        // If pawn doesn't have a controller, return null. 
        if (!pawn || !pawn.controller) return null;

        // Get the Controller from the pawn
        return pawn.controller.GetComponent<Controller>();
    }

    protected Controller GetPlayerController(Collider other)
    {
        // Get the controller
        Controller controller = GetController(other);
        if (!controller) return null;

        // If the controller is not a PlayerController, it'll be null
        return controller.GetComponent<PlayerController>();
    }
    #endregion Pickup
}
