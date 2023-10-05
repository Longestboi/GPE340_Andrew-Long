using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class ProjectileBullet : Projectile
{
    #region Fields
    /// <summary>Rigidbody reference</summary>
    private Rigidbody rb;

    /// <summary>The time it takes for the projectile destroy itself</summary>
    [Tooltip("The time it takes for the projectile destroy itself")]
    public float timeToDestroy;
    #endregion Fields

    #region MonoBehaviour
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Start a coroutine to kill self after x seconds
        StartCoroutine(KillAfterSeconds(timeToDestroy));
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other) {
        // TODO: hurt player on collision
        Destroy(gameObject);
    }
    #endregion MonoBehaviour

    #region Projectile
    public override void Move() {
        // Move the projectile forward at speed
        rb.MovePosition(rb.position + (transform.forward * speed * Time.deltaTime));
    }
    #endregion Projectile

    #region ProjectileBullet
    /// <summary>Watchdog timer for the bullet projectile</summary>
    /// <param name="time">The amount of time to wait to destroy the bullet</param>
    IEnumerator KillAfterSeconds(float time)
    {
        // Wait
        yield return new WaitForSeconds(time);
    
        // Kill
        Destroy(gameObject);
    }
    #endregion ProjectileBullet
}
