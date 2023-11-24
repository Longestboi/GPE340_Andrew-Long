using UnityEngine;

[RequireComponent(typeof(Health))]
public class Death : MonoBehaviour
{
    #region MonoBehaviour
    public virtual void Start()
    {
        Health health = GetComponent<Health>();

        health.onDieOnce.AddListener(OnDie);   
    }
    #endregion MonoBehaviour
    
    #region Death
    /// <summary>The function that gets added to the onDie event</summary>
    public virtual void OnDie()
    {
        Destroy(gameObject);
    }
    #endregion
}
