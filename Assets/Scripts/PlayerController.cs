using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{

    public bool isMouseRotation;

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
        // Debug.Log(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        // Get the direction to move in
        Vector3 movingDirection = new Vector3(
            Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")
        );

        /* Transform movingDirection to make the pawn move in relation to the screen
         * and not in relation to the player.
         */
        movingDirection = pawn.transform.InverseTransformDirection(movingDirection);

        // Tell pawn to move based on inputs...
        pawn.Move(movingDirection);

        if (isMouseRotation)
        {
            Vector3 pointToLookAt;

            // Finding the point
            
            // Create a plane at the feet of the pawn
            Plane thePlane = new Plane(Vector3.up, pawn.transform.position);
            
            // create a ray from the mouse pointer in the direction of the camera
            Ray theRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            
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
