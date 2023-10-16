using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Pawn), typeof(Health))]
public class CharacterPawnDeath : Death
{
    
    #region MonoBehaviour
    public void Start()
    {
        Health health = GetComponent<Health>();

        health.onDie.AddListener(OnDie); 
    }
    #endregion MonoBehaviour
    
    #region Death
    public override void OnDie()
    {
        CharacterPawn pawn = GetComponent<CharacterPawn>();
        Animator animator = GetComponent<Animator>();

        // Stop the pawn from moving
        pawn.canMove = false;
        // Set the animator to dead so the death animation plays
        animator.SetBool("isDead", true);
    }
    #endregion Death

    #region CharacterPawnDeath
    // <summary>Animation event to kill the pawn</summary>
    public void DoOnDie()
    {
        CharacterPawn pawn = GetComponent<CharacterPawn>();
        Health health = pawn.GetComponent<Health>();

        health.onDie.AddListener(() => {
            // Unpossess the pawn
            pawn.controller.Unpossess(pawn);
        });

        // Destroy
        Destroy(pawn.gameObject);
    }
    #endregion CharacterPawnDeath
}
