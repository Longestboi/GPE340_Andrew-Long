using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Health))]
public class CharacterPawn : Pawn
{
    #region Fields
    /// <summary>Reference to the animator</summary>
    private Animator animator;
    #endregion Fields

    #region MonoBehaviour
    public void Start()
    {
        // Get the animator
        animator = GetComponent<Animator>();
        animator.applyRootMotion = true;
    }

    public void OnAnimatorMove()
    {
        if (!animator || isDead) return;
        // Use root motion to move the game object
        transform.position += animator.deltaPosition;
        transform.rotation *= animator.deltaRotation;

        // If we have a NavMeshAgent on our controller,
        AiController aiController = controller as AiController;
        if (aiController && aiController.agent)
        {
            // Move to the position of the NavMeshAgent, too
            transform.position = aiController.agent.nextPosition;
        }
    }

    public void OnAnimatorIK(int layerIndex)
    {
        // If no weapon exists, set weights to zero
        if (!weapon) {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.0f);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.0f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0.0f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0.0f);
            return;
        }

        // Anon function to cut down on duplicate code
        // (I think it's as fast as duplicate code)
        Action<AvatarIKGoal, Transform, float, float> SetIKPositionAndWeight =
        (AvatarIKGoal goal, Transform goalTransform, float posWeight, float posRot) =>
        {
            animator.SetIKPosition(goal, goalTransform.position);
            animator.SetIKPositionWeight(goal, posWeight);
            animator.SetIKRotation(goal, goalTransform.rotation);
            animator.SetIKRotationWeight(goal, posRot);
        };

        // Set positions and weights for the hands
        if (weapon.RHandPoint)
            SetIKPositionAndWeight(AvatarIKGoal.RightHand, weapon.RHandPoint, 1.0f, 1.0f);

        if (weapon.LHandPoint)
            SetIKPositionAndWeight(AvatarIKGoal.LeftHand, weapon.LHandPoint, 1.0f, 1.0f);
        
    }
    #endregion MonoBehaviour

    #region Pawn
    public override void Move(Vector3 direction)
    {
        if (isDead || !animator) return;
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
        if (isDead) return;
        transform.Rotate(0, direction * roationSpeed * Time.deltaTime, 0);
    }

    public override void RotateToLookAt(Vector3 position)
    {
        if (isDead) return;
        // Find the vector from self to target.
        Vector3 vectorToLookDown = position - transform.position;
        // Find the rotation we would need to look down that vector
        Quaternion lookRotation = Quaternion.LookRotation(vectorToLookDown);
        // Rotate a bit towards that rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, roationSpeed * Time.deltaTime);
    }
    #endregion Pawn
}

