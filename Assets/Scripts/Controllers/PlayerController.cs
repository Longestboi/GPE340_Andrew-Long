using UnityEngine;

public class PlayerController : Controller
{
    #region Fields
    [Header("Camera data")]
    /// <summary>Reference to the main camera</summary>
    [SerializeField, Tooltip("Reference to the main camera")]
    private Camera mainCamera = null;

    /// <summary>Flag that controls if the mouse will be used in the rotation</summary>
    [Tooltip("Toggle if the player uses mouse controls to rotate")]
    public bool isMouseRotation;

    /// <summary>The height that the camera should be above the pawn</summary>
    [Tooltip("The height that the camera should be above the pawn")]
    public float heightAbovePawn;

    public GameObject playerPrefab;

    /// <summary>Number of lives that the player has</summary>
    public int lives = 3;

    public Transform playerRespawnPoint;

    private bool _needsRespawn = false;
    #endregion

    #region MonoBehaviour
    // Start is called before the first frame update
    void Start()
    {
        if (pawn) { 
            Possess(pawn);
        }

        // Set the position of the camera
        if (mainCamera && pawn)
        {
            mainCamera.transform.GetPositionAndRotation(out _, out Quaternion cameraRotation);

            mainCamera.transform.SetPositionAndRotation(
                new Vector3(
                    pawn.transform.position.x, heightAbovePawn,
                    pawn.transform.position.z
                ),
                cameraRotation
            );
        };
    }

    // Update is called once per frame
    void Update()
    {
        DoPlayerRespawn();
        // Do nothing when we aren't possessing a pawn.
        if (!pawn || pawn.isDead) return;

        // Do the movement and rotation code.
        DoMoveAndRotate();
        // Do the camera movement stuffs
        CameraUpdate();

        DoWeaponTrigger();
    }
    #endregion MonoBehaviour

    #region Controller
    // Base Controller functions
    public override void Possess(Pawn pawnToPossess){
        pawn = pawnToPossess;

        // Set the controller of the pawn to us...
        pawn.controller = GetComponent<Controller>();

        pawn.weapon.owner = this;
        
        // Make sure the rigidbody does not change our position, only use root motion and the agent
        pawn.GetComponent<Rigidbody>().isKinematic = true;

        pawn.GetComponent<Health>().onDieOnce.AddListener(DecrementLife);
    }

    public override void Unpossess(Pawn pawn){}
    #endregion Controller

    #region PlayerController
    private void CameraUpdate()
    {
        // Stop camera update from running if there is no camera attached...
        if (!mainCamera) return;
        
        // Get the camera rotation so we don't discard it
        mainCamera.transform.GetPositionAndRotation(out _, out Quaternion cameraRotation);

        // Dummy vars
        float fDummyVelo = 0.0f;
        Vector3 vDummyVelo = new Vector3();

        // Smooth damp the camera height
        float heightBuffer = Mathf.SmoothDamp(
            mainCamera.transform.position.y,
            pawn.transform.position.y + heightAbovePawn,
            ref fDummyVelo, 2f * Time.deltaTime
        );

        // Set the position of the camera in relation to the 
        mainCamera.transform.SetPositionAndRotation(
            Vector3.SmoothDamp(
                mainCamera.transform.position,
                // New camera position
                new Vector3(
                    pawn.transform.position.x,
                    heightBuffer,
                    pawn.transform.position.z
                ),
                ref vDummyVelo, 2f * Time.deltaTime
            ),
            cameraRotation
        );
    }

    private void DoMoveAndRotate()
    {        
        // Get the direction to move in
        Vector3 movingDirection = new Vector3(
            Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")
        );

        // Tell pawn to move based on inputs...
        pawn.Move(movingDirection);

        if (isMouseRotation)
        {
            Vector3 pointToLookAt;

            // Create a plane at the feet of the pawn
            Plane thePlane = new Plane(Vector3.up, pawn.transform.position);
            
            // create a ray from the mouse pointer in the direction of the camera
            Ray theRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            
            // Find where they intersect
            float intersectionDistance = 0f;
            thePlane.Raycast(theRay, out intersectionDistance);
            pointToLookAt = theRay.GetPoint(intersectionDistance);

            pawn.RotateToLookAt(pointToLookAt);
        }
        else
        {
            // Tell rotate to move based on inputs...
            pawn.Rotate(Input.GetAxis("Rotate"));
        }
    }

    private void DoWeaponTrigger()
    {
        if (pawn.weapon == null || Time.timeScale == 0) return;

        if(Input.GetButtonDown("Fire1"))
            pawn.weapon.OnTriggerPull.Invoke();
        else if (Input.GetButton("Fire1"))
            pawn.weapon.OnTriggerHold.Invoke();
        else if (Input.GetButtonUp("Fire1"))
            pawn.weapon.OnTriggerRelease.Invoke();
        else
            pawn.weapon.OnTriggerIdle.Invoke();
    }

    public void DoPlayerRespawn()
    {
        if (_needsRespawn && !pawn)
        {
            pawn = Instantiate(playerPrefab.GetComponent<Pawn>(), playerRespawnPoint);
            Possess(pawn);
            _needsRespawn = false;
        }
    }

    public void DecrementLife()
    {
        lives--;
        _needsRespawn = true;
    }

    #endregion PlayerController
}
