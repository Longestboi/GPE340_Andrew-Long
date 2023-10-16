using System.Collections;
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

    public float respawnTimer;

    public Transform respawnLocation;

    public Pawn aiPawnPrefab;

    private Coroutine respawnRoutine;
    #endregion Fields

    #region MonoBehaviour
    // Start is called before the first frame update
    void Start(){
        if (pawn) Possess(pawn);
    }

    // Update is called once per frame
    void Update()
    {
        if (respawnRoutine == null && !pawn)
        {
            respawnRoutine = StartCoroutine(Respawn());
        }

        if (!pawn || !agent) return;

        // Seek if the targetTransform exists.
        if (targetTransform) DoSeek();
    }
    #endregion MonoBehaviour

    #region Controller
    public override void Possess(Pawn pawnToPossess)
    {
        pawn = pawnToPossess;

        // Set the controller of the pawn to us...
        pawn.controller = GetComponent<Controller>();

        // Get NavMeshAgent from pawn.
        agent = pawn.GetComponent<NavMeshAgent>();

        // Add agent if there is none
        if (!agent)
        {
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
        if (pawn)
        {
            pawn.GetComponent<Rigidbody>().isKinematic = false;
            // pawn.controller = null;
            pawn = null;
        }
        
        Destroy(agent);
    }
    #endregion Controller

    #region AiController

    void DoSeek()
    {
        if (!agent.isOnNavMesh) return;
        agent.SetDestination(targetTransform.position);

        desiredVelocity = agent.desiredVelocity;

        pawn.Move(desiredVelocity);

        pawn.RotateToLookAt(targetTransform.position);
    }

    IEnumerator Respawn()
    {
        while(true)
        {
            if (pawn) yield return null; 
            // Wait for the respawn timer
            yield return new WaitForSecondsRealtime(respawnTimer);
            
            Possess(Instantiate(aiPawnPrefab, respawnLocation));

            StopCoroutine(respawnRoutine);
            respawnRoutine = null;
            yield break;
        }
        
    }
    #endregion AiController
}
