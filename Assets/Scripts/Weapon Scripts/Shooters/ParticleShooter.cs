using System.Collections.Generic;
using UnityEngine;
using Utils;

// [RequireComponent(typeof(ParticleSystem))]
public class ParticleShooter : Shooter
{
    #region Fields
    private ParticleSystem part;
    #endregion Fields

    #region Monobehaviour
    // Start is called before the first frame update
    void Start()
    {
        part = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other) {
        Controller controller = ControllerUTE.GetController(other.gameObject);
        Health pHealth;
        if (!controller || !controller.pawn) return;
        
        pHealth = controller.pawn.GetComponent<Health>();
        if (!pHealth) return;

        pHealth.Damage(damageAmount);
    }
    #endregion Monobehaviour

    #region Shooter
    public override void Shoot()
    {
        part.Emit(1);
    }
    #endregion Shooter
}
