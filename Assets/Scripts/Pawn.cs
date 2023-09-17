using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    // Properties
    public Controller controller;

    public float moveSpeed;
    public float roationSpeed;

    // Methods
    public abstract void Move(Vector3 direction);
    public abstract void Rotate(float direction);
    public abstract void RotateToLookAt(Vector3 position);
}
