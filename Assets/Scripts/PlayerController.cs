using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : Controller
{
    /// <summary>Flag that controls if the mouse will be used in the rotation</summary>
    public bool isMouseRotation;

    /// <summary>Reference to the main camera</summary>
    public Camera mainCamera = null;

    /// <summary>The height that the camera should be above the pawn</summary>
    public float heightAbovePawn;

    delegate void OnInteract();
    OnInteract interact;


    // MonoBehaviour Functions
    #region MonoBehaviour
    // Start is called before the first frame update
    void Start(){
        interact = InteractWithButtons;
    }

    // Update is called once per frame
    void Update()
    {
        // Do interaction
        if (Input.GetButton("Interact")) {
            interact();
        }

        // Do the movement and rotation code.
        DoMoveAndRotate();
        // Do the camera movement stuffs
        CameraUpdate();
    }
    #endregion MonoBehaviour

    // Base Controller functions
    #region Controller
    public override void Possess(Pawn pawn)
    {
        // throw new System.NotImplementedException();
    }

    public override void Unpossess(Pawn pawn)
    {
        // throw new System.NotImplementedException();
    }
    #endregion Controller

    void CameraUpdate()
    {
        // Stop camera update from running if there is no camera attached...
        if (mainCamera == null) { return; }
        
        // Get the camera rotation so we don't discard it
        mainCamera.transform.GetPositionAndRotation(out _, out Quaternion cameraRotation);

        // Dummy vars
        float fDummyVelo = 0.0f;
        Vector3 vDummyVelo = new Vector3();

        // Smooth damp the 
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
                    pawn.transform.position.z), ref vDummyVelo, 2f * Time.deltaTime
                ),
            cameraRotation
        );
    }

    void DoMoveAndRotate()
    {
        // Get the direction to move in
        Vector3 movingDirection = new Vector3(
            Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")
        );

        // Debug.Log(movingDirection);

        // Tell pawn to move based on inputs...
        pawn.Move(movingDirection);

        if (isMouseRotation)
        {
            Vector3 pointToLookAt;

            // Finding the point
            
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

    void InteractWithButtons() {
        foreach (RaycastHit h in Physics.SphereCastAll(pawn.transform.position, 1f, pawn.transform.forward))
        {
            IInteractable ii = h.transform.gameObject.GetComponent<IInteractable>();
            if (ii != null)
            {
                ii.OnInteraction();
            }
        }
    }
}
