using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RagdollController))]
public class RagdollDeath : Death
{    
    #region Fields
    public float delayDestroyTime = 0;
    #endregion Fields

    #region Death
    /// <summary>The function that gets added to the onDie event</summary>
    public override void OnDie()
    {
        RagdollController rc = GetComponent<RagdollController>();
        Pawn pawn = GetComponent<Pawn>();

        pawn.isDead = true;
        rc.EnableRagdoll();
        StartCoroutine(DoDestroy());
    }

    IEnumerator DoDestroy()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayDestroyTime);
            Destroy(gameObject);
        }
    }
    #endregion Death
}
