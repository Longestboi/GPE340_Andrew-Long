using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{   
    #region WeaponEvents
    [Header("Weapon Events")]

    [Tooltip("Event that happens when the trigger is pulled")]
    /// <summary>Event that happens when the trigger is pulled</summary>
    public UnityEvent OnTriggerPull;
    
    [Tooltip("Event that happens when the trigger is held")]
    /// <summary>Event that happens when the trigger is held</summary>
    public UnityEvent OnTriggerHold;
    
    [Tooltip("Event that happens when the trigger is relased")]
    /// <summary>Event that happens when the trigger is relased</summary>
    public UnityEvent OnTriggerRelease;
    
    [Tooltip("Event that happens when the trigger is idle")]
    /// <summary>Event that happens when the trigger is idle</summary>
    public UnityEvent OnTriggerIdle;
    #endregion WeaponEvents

    #region Fields

    public WeaponType weaponType = WeaponType.None;
    [Tooltip("The position that things shoot out from")]
    /// <summary>The position that the things shoot out from</summary>
    public Transform firePosition;

    /// <summary>The owner of this weapon component</summary>
    public Controller owner;

    public Transform LHandPoint;
    public Transform RHandPoint;

    [Range(0, 100)]
    public float weaponAccuracy;
    private float maxWeaponAccuracy = 100;
    public float maxWeaponRotationOffset;
    #endregion Fields

    #region MonoBehaviour
    void Start()
    {
        Pawn pawn = transform.parent.GetComponent<Pawn>();
        if (!pawn) return;

        // Set the owner through the pawn
        owner = pawn.controller;

    }
    #endregion MonoBehaviour

    #region Weapon

    public float GetNextShotAccuracy()
    {
        // Get the accuracy percentage 
        float accuracyPercentage = 1 - (weaponAccuracy / maxWeaponAccuracy);

        float ownerAccuracy = 1 - (owner.accuracy / 100);

        // Get the roation from the accuracy percentage
        float maxRotationOffset = maxWeaponRotationOffset * accuracyPercentage * ownerAccuracy;
        
        // Randomize the rotation
        float accuracyRotationOffset = maxRotationOffset * Random.value;
        
        // accuracyRotationOffset -= accuracyRotationOffset / 2;

        // Randomize the direction of the rotation
        return (Random.value <= .5f) ? accuracyRotationOffset : -accuracyRotationOffset;
    }

    #endregion Weapon
}
