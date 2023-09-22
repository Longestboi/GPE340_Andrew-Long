using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPawn : Pawn
{

    private Animator animator;

    // MonoBehaviour

    public void Start()
    {
        // Get the animator
        animator = GetComponent<Animator>();
    }

    public override void Move(Vector3 direction)
    {

        /* Transform movingDirection to make the pawn move in relation to the screen
         * and not in relation to the player.
         */
        direction = transform.InverseTransformDirection(direction);

        // Clamp direction to 1 unit, normalizing the disparity between the input methods
        direction = Vector3.ClampMagnitude(direction, 1);

        // Take moving direction and make the character move at moveSpeed
        direction *= moveSpeed;

        // Send the direction value into the animator
        animator.SetFloat("Vertical", direction.z);
        animator.SetFloat("Horizontal", direction.x);
    }

    public override void Rotate(float direction)
    { 
        transform.Rotate(0, direction * roationSpeed * Time.deltaTime, 0);
    }

    public override void RotateToLookAt(Vector3 position)
    {
        // Find the vector from self to target.
        Vector3 vectorToLookDown = position - transform.position;
        // Find the rotation we would need to look down that vector
        Quaternion lookRotation = Quaternion.LookRotation(vectorToLookDown);
        // Rotate a bit towards that rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, roationSpeed * Time.deltaTime);

    }
    public void OnAnimatorMove()
    {
        // After the animation runs

        // Use root motion to move the game object
        transform.position += animator.deltaPosition;
        transform.rotation *= animator.deltaRotation;

        // If we have a NavMeshAgent on our controller,
        AiController aiController = controller as AiController;
        if (aiController != null) {
            // Move to the position of the NavMeshAgent, too
            transform.position = aiController.agent.nextPosition;            
        }

    }
}

