using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

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

    public float shootingDistance = 3.5f;
    #endregion Fields

    #region MonoBehaviour
    // Start is called before the first frame update
    void Start(){
        if (pawn) Possess(pawn);
    }

    // Update is called once per frame
    void Update()
    {

        if (!pawn || !agent || !targetTransform) return;

        // Seek if the targetTransform exists.
        if (targetTransform) DoSeek();
        
        float distanceBetweenAiAndPlayer = Vector3.Distance(
            pawn.transform.position, targetTransform.position
        );

        if (distanceBetweenAiAndPlayer < shootingDistance && !pawn.isDead)
            DoShooting(true);
        else
            DoShooting(false);
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

    public void DoShooting(bool isShooting)
    {
        if (!pawn.weapon) return;
        var player = targetTransform.gameObject.GetComponent<CharacterPawn>();

        if (player.isDead)
        {
            if (pawn.weapon.GetComponent<Shooter>().IsShooting())
                pawn.weapon.OnTriggerRelease.Invoke();
            return;
        }
        // Debug.Log(test.name + ":e:" + test.canBeControlled);

        if (isShooting)
            pawn.weapon.OnTriggerPull.Invoke();
        else
            pawn.weapon.OnTriggerRelease.Invoke();
    }

    public void EquipWeaponFromPrefab()
    {

    }
    #endregion AiController
}
