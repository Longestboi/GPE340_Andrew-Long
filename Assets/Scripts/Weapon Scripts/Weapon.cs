using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{   
    #region WeaponEvents
    public UnityEvent OnTriggerPull;
    public UnityEvent OnTriggerHold;
    public UnityEvent OnTriggerRelease;

    public UnityEvent OnTriggerIdle;
    #endregion WeaponEvents

    #region Fields
    public Transform firePosition;
    #endregion Fields
}
