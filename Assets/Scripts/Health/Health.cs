using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{   
    #region HealEvents
    [Header("Events")]
    [SerializeField, Tooltip("Executed everytime the component is healed")]
    public UnityEvent onHeal;
    [SerializeField, Tooltip("Executed everytime the component is damaged")]
    public UnityEvent onDamage;
    [SerializeField, Tooltip("Executed when the component is killed on every update")]
    public UnityEvent onDie;
    [SerializeField, Tooltip("Executed when the component is killed once")]
    public UnityEvent onDieOnce;
    
    private bool _hasDied;
    #endregion HealEvents
    
    #region Fields
    [Header("Fields")]
    [Tooltip("The value that the health is set to when the component is initialized")]
    /// <summary>Value that the health is set to</summary>
    public int baseHealth = 100;

    [Tooltip("Modifyable health value")]
    /// <summary>Health</summary>
    public int health = 100;

    private float minDecayFactor = 0.005f;
    private float maxDecayFactor = 0.01f;

    /// <summary>Coroutine for the health decrementer</summary>
    private Coroutine healthDec;
    #endregion Fields;

    // Start is called before the first frame update
    void Start()
    {
        health = baseHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // If health is 0 or below, invoke the death
        if(health <= 0) onDie.Invoke();

        if (health <= 0 && !_hasDied)
        {
            onDieOnce.Invoke();
            _hasDied = true;
        }

        // Do exponential decay when capable
        if(health > baseHealth && healthDec == null)
        {
            healthDec = StartCoroutine(decHealth());
        } 
    }

    IEnumerator decHealth()
    {
        // TODO: Refactor this garbage
        while (health > baseHealth)
        {
            float healthRatio = health / baseHealth;

            float decayFactor = Mathf.Lerp(maxDecayFactor, minDecayFactor, healthRatio);

            // Ensure the decay factor is between the given numbers
            decayFactor = Mathf.Clamp(decayFactor, minDecayFactor, maxDecayFactor);

            // Make sure the health never goes below the base health...
            health = Math.Max(baseHealth, health - Mathf.RoundToInt(decayFactor * health));

            // Return to the update
            yield return null;
        }

        healthDec = null;
        yield break;
    }

    public void Damage(int damage)
    {
        // Stop negative damage
        damage = Math.Max(damage, 0);

        // Stop health from going below 0 
        health = Math.Max(health - damage, 0);

        // SendMessage("OnDamage", SendMessageOptions.DontRequireReceiver);
        onDamage.Invoke();
    }

    public void Heal(int healFactor)
    {
        health += healFactor;

        // SendMessage("OnHeal", SendMessageOptions.DontRequireReceiver);
        onHeal.Invoke();
    }
}
