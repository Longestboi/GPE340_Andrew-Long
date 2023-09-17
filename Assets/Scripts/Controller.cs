using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{

    // Properties
    public Pawn pawn;
    
    // Abstract Methods
    public abstract void Possess(Pawn pawn);
    public abstract void Unpossess(Pawn pawn);
}
