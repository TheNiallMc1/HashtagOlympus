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
    }

    public eTargetType targetType;

    [HideInInspector]
    public Dictionary<StatusEffect, StatusEffectManager> activeStatusEffects = new Dictionary<StatusEffect, StatusEffectManager>(); 
    
    public GameObject colliderHolder;

    [Header("Character Info")] 
    public string characterName;
    
    [Header("Combat Stats")]
    public int maxHealth;
    [HideInInspector] public int currentHealth;
    public int attackDamage;
    public int damageReduction;

    public void Start()
    {
        currentHealth = maxHealth;
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

        if (targetType == eTargetType.Player)
        {
            GetComponent<GodBehaviour>().OnDamageEvent(damageTaken);
        }
        
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
        if (targetType == eTargetType.Player)
        {
            GetComponent<GodBehaviour>().OnDeathEvent();
        }
        
        print(gameObject.name + " has been defeated");
        Destroy(gameObject);
    }
}