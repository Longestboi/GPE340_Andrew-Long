using UnityEngine;
using UnityEngine.AI;

public class AiController : Controller
{
    #region Fields
    [HideInInspector]
    public NavMeshAgent agent;
    public float stoppingDistance = 1;
    public Transform targetTransform;
    private Vector3 desiredVelocity = Vector3.zero;
    #endregion Fields

    #region MonoBehaviour
    // Start is called before the first frame update
    void Start(){
        if (pawn != null) Possess(pawn);
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(targetTransform.position);

        desiredVelocity = agent.desiredVelocity;

        pawn.Move(desiredVelocity);

        pawn.RotateToLookAt(targetTransform.position);
    }
    #endregion MonoBehaviour

    #region Controller
    public override void Possess(Pawn pawnToPossess)
    {
        pawn = pawnToPossess;

        // Get NavMeshAgent from pawn.
        agent = pawn.GetComponent<NavMeshAgent>();

        // Add agent if there is none
        if (agent == null) {
            agent = pawn.gameObject.AddComponent<NavMeshAgent>();
        }

        // Set the stopping distance
        agent.stoppingDistance = stoppingDistance;

        // set agent speed and rotation speed 
        agent.speed = pawn.moveSpeed;
        agent.angularSpeed = pawn.roationSpeed;

        // Disable movement and rotation from the NavMeshAgent
        agent.updatePosition = false;
        agent.updateRotation = false;
        
        // Make sure the rigidbody does not change our position, only use root motion and the agent
        pawn.GetComponent<Rigidbody>().isKinematic = true;
    }

    public override void Unpossess(Pawn pawnToUnpossess)
    {
        pawn.GetComponent<Rigidbody>().isKinematic = false;
        pawn = null;
        Destroy(agent);
    }
    #endregion Controller

}
