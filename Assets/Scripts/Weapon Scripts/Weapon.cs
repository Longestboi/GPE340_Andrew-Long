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
    [Tooltip("The position that things shoot out from")]
    /// <summary>The position that the things shoot out from</summary>
    public Transform firePosition;

    /// <summary>The owner of this weapon component</summary>
    public Controller owner;
    #endregion Fields

    #region MonoBehaviour
    void Start() {
        Pawn pawn = transform.parent.GetComponent<Pawn>();
        if (!pawn) return;

        // Set the owner through the pawn
        owner = pawn.controller;
    }
    #endregion MonoBehaviour
}
