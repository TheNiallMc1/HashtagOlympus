using System.Collections.Generic;
using UnityEngine;

public class Combatant : MonoBehaviour
{
    public enum eTargetType
    {
        Player,
        Enemy,
        PMonument,
        DMonument,
        EMonument
    }

    public int respectOnKill;

    [SerializeField] private eTargetType _targetType;
    public eTargetType targetType { get { return _targetType; } set { _targetType = value; } }
    public Dictionary<StatusEffect, StatusEffectManager> activeStatusEffects = new Dictionary<StatusEffect, StatusEffectManager>(); 
    
    public GameObject colliderHolder;

    [Header("Character Info")] 
    public string characterName;
    
    [Header("Combat Stats")]
    public int maxHealth;
    public int currentHealth;
    public int attackDamage;
    [Range(0, 100)] public int damageReduction = 0;

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

    public void ApplyStatus(StatusEffect status, Combatant inflictedBy)
    {
        status.inflictedBy = inflictedBy;
        
        Debug.LogWarning("Begin applying status");
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
            newStatusManager.originalStatus = status;
        }       
    }

    public void RemoveStatus(StatusEffect status)
    {        
        // If the status already exists on this entity, remove it
        if (activeStatusEffects.ContainsKey(status))
        {
            Debug.Log("Begin remove status");
            
            // Get the value by its key (the status type) and then end the status
            if (activeStatusEffects.TryGetValue(status, out StatusEffectManager manager))
            {
                manager.ActivateExitEffect();
                activeStatusEffects.Remove(status);
            }
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
            if (activeStatusEffects.TryGetValue(status, out StatusEffectManager manager))
            {
                activeStatusEffects.Remove(status);
            }
        }
        
        else
        {
            Debug.LogWarning("The status of " + status.name + " does not exist on this entity");
        }
    }

    #endregion
    
    public void TakeDamage(int rawDamage)
    {
        currentHealth -= (rawDamage * (1 - (damageReduction/100)));
        
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            if (targetType == eTargetType.Player || targetType == eTargetType.Enemy)
            {
                Die();
            }
        }

        if (targetType == eTargetType.Player)
        {
            GetComponent<GodBehaviour>().OnDamageEvent(rawDamage);
        }
    }

    private void Die()
    {
        switch (targetType)
        {
            case eTargetType.Player:
                GetComponent<GodBehaviour>().OnDeathEvent();
                break;
            
            case eTargetType.Enemy:
                print(gameObject.name + " has been defeated");
                GetComponent<AIBrain>().OnDeathEvent();
                GameManager.Instance.AddRespect(respectOnKill);
                break;
        }
    }
}