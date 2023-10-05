using UnityEngine;

public abstract class Controller : MonoBehaviour
{

    #region Fields
    /// <summary>The pawn that will be controlled by this controller</summary>
    [Header("Controller Base Class:"), Tooltip("The pawn that will be controlled by this controller")]
    public Pawn pawn;
    #endregion Fields
    
    #region Controller
    /// <summary>Attach a controller to a pawn</summary>
    /// <param name="pawnToPossess">The pawn that will have this controller attached to it</param>
    public abstract void Possess(Pawn pawnToPossess);

    /// <summary>Detach a controller from a pawn</summary>
    /// <param name="pawnToUnpossess">The pawn that will have a controller removed from it</param>
    public abstract void Unpossess(Pawn pawnToUnpossess);
    #endregion Controller
}
