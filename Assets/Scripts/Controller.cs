using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{

    // Properties
    public Pawn pawn;
    
    /* Abstract Methods */
    /// <summary>Attach a controller to a pawn</summary>
    /// <param name="pawnToPossess">The pawn that will have this controller attached to it</param>
    public abstract void Possess(Pawn pawnToPossess);
    /// <summary>Detach a controller from a pawn</summary>
    /// <param name="pawnToUnpossess">The pawn that will have a controller removed from it</param>
    public abstract void Unpossess(Pawn pawnToUnpossess);
}
