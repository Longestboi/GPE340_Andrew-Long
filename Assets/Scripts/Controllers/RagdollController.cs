using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private Rigidbody mainRigidbody;
    private Collider mainCollider;
    private Rigidbody[] rigidbodies;
    private Collider[] colliders;
    private Animator animator;

    public bool isRagdolled = false;

    // Start is called before the first frame update
    void Start()
    {
        mainRigidbody = GetComponent<Rigidbody>();
        mainCollider = GetComponent<Collider>();
        animator = GetComponent<Animator>();

        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        
        if (isRagdolled) EnableRagdoll();
        else DisableRagdoll();
    }

    public void ToggleRagdoll()
    {
        if (isRagdolled) EnableRagdoll();
        else DisableRagdoll();
    }

    public void EnableRagdoll()
    {

        foreach (Collider collider in colliders)
            collider.enabled = true;

        foreach (Rigidbody rigidbody in rigidbodies)
            rigidbody.isKinematic = false;


        mainRigidbody.isKinematic = true;
        mainCollider.enabled = false;

        animator.enabled = false;
        isRagdolled = true;
    }

    public void DisableRagdoll()
    {
        foreach (Collider collider in colliders)
            collider.enabled = false;

        foreach (Rigidbody rigidbody in rigidbodies)
            rigidbody.isKinematic = true;

        mainRigidbody.isKinematic = false;
        mainCollider.enabled = true;

        animator.enabled = true;
        isRagdolled = false;
    }
}
