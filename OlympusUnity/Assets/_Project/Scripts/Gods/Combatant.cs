using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combatant : MonoBehaviour
{
    public enum eTargetType
    {
        Player,
        Enemy,
        PMonument,
        EMonument
    };

    [SerializeField] private eTargetType _targetType;
    public eTargetType targetType { get { return _targetType; } set { _targetType = value; } }
    public Dictionary<StatusEffect, StatusEffectManager> activeStatusEffects = new Dictionary<StatusEffect, StatusEffectManager>(); 
    // Dictionary of statuses this combatant has had inflicted on them
    
    public int maxHealth = 100;
    public int currentHealth = 100;
    public int attackStat = 10;

    // Start is called before the first frame update 
    void Start()
    {

    }

    // Update is called once per frame 
    void Update()
    {

    }

    public void RestoreHealth(int healthRecovered)
    {
        currentHealth += healthRecovered;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }

    #region Status Effects

    public void ApplyStatus(StatusEffect status)
    {
        // If the status we are trying to apply already exists on this combatant, dont add it
        if (activeStatusEffects.ContainsKey(status))
        {
            Debug.LogWarning("The status of " + status.name + " already exists on this entity");
        }
        else
        {
            StatusEffectManager newStatusManager = gameObject.AddComponent<StatusEffectManager>();
        
            activeStatusEffects.Add(status, newStatusManager); 
            // Add an entry in the dictionary, with this type of status as the key and the new manager component as the value
        
            newStatusManager.enabled = true;
            newStatusManager.statusEffect = status;
        }       
    }

    public void RemoveStatus(StatusEffect status)
    {
        // If the status already exists on this entity, remove it
        if (activeStatusEffects.ContainsKey(status))
        {
            // Get the value by its key (the status type) and then end the status
            activeStatusEffects.TryGetValue(status, out StatusEffectManager manager);
            manager?.EndStatus();

            // Remove from dictionary
            activeStatusEffects.Remove(status);
        }
        
        else
        {
            Debug.LogWarning("The status of " + status.name + " does not exist on this entity");
        }
    }

    // Same as remove status, but doesnt call "EndStatus", to avoid a loop for status effects like Heal
    public void RemoveStatusFromList(StatusEffect status) 
    {
        // If the status already exists on this entity, remove it
        if (activeStatusEffects.ContainsKey(status))
        {
            // Get the value by its key (the status type) and then end the status
            activeStatusEffects.TryGetValue(status, out StatusEffectManager manager);

            // Remove from dictionary
            activeStatusEffects.Remove(status);
        }
        
        else
        {
            Debug.LogWarning("The status of " + status.name + " does not exist on this entity");
        }
    }

    #endregion
    
    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            if (targetType == eTargetType.Player || targetType == eTargetType.Enemy)
            {
                Die();
            }
            if(targetType == eTargetType.PMonument)
            {
                // state change
            }

        }
    }

    public void Die()
    {
        print(gameObject.name + " has been defeated");
        Destroy(gameObject);
    }
}