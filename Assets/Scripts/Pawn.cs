using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    /* Properties */
    /// <summary>The current controller of this pawn</summary>
    public Controller controller;
    /// <summary>The speed the pawn should move at</summary>
    public float moveSpeed;
    /// <summary> The speed the pawn should rotate at</summary>
    public float roationSpeed;

    /* Methods */
    /// <summary>Move the pawn in the supplied direction</summary>
    /// <param name="direction">The direction to move in</param>
    public abstract void Move(Vector3 direction);
    /// <summary>Rotate to the given direction</summary>
    /// <param name="direction">The angle to ratate at</param>
    public abstract void Rotate(float direction);
    /// <summary>Rotate to look at a position</summary>
    /// <param name="position">Position to rotate towards</param>
    public abstract void RotateToLookAt(Vector3 position);
}
