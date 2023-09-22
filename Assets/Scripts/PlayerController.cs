using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : Controller
{
    public bool isMouseRotation;

    public Camera mainCamera = null;
    public float heightAboveFloor;

    public override void Possess(Pawn pawn)
    {
        // throw new System.NotImplementedException();
    }

    public override void Unpossess(Pawn pawn)
    {
        // throw new System.NotImplementedException();
    }

    // MonoBehaviour Methods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Do the movement and rotation code.
        DoMoveAndRotate();

        // Do the camera movement stuffs
        CameraUpdate();
    }

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
            pawn.transform.position.y + heightAboveFloor,
            ref fDummyVelo, .02f
        );

        // Set the position of the camera in relation to the 
        mainCamera.transform.SetPositionAndRotation(
            Vector3.SmoothDamp(
                mainCamera.transform.position,
                // New camera position
                new Vector3(
                    pawn.transform.position.x,
                    heightBuffer,
                    pawn.transform.position.z), ref vDummyVelo, 0.1f
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
}
