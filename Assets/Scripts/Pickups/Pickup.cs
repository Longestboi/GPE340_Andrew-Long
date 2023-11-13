using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Pickup : MonoBehaviour
{
    #region Fields
    [Tooltip("Allows for any pawn to interact with the pickup, or only the player")]
    /// <summary>Allows for any pawn to interact with the pickup, or only the player</summary>
    public bool isPlayerExclusive;
    #endregion Fields
}
