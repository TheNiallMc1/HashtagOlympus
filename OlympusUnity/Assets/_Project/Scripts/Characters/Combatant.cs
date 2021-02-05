using System.Collections.Generic;
using _Project.Scripts.AI.AiControllers;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Rendering;

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

    public float healthPercentage;
    
    [Header("Character Info")] 
    public string characterName;
    public Sprite characterSprite;

    public GameObject targetIcon; // Shows when the combatant can be targeted during ability
    public GameObject circleMarker;
    private Vector3 circleStartingSize;
    public GameObject coneMarker;
    
    [Header("Combat Stats")]
    public float maxHealth;
    public float currentHealth;
    public HealthBar healthBar;
    public int attackDamage;
    [Range(0, 100)] public float damageReduction;

    public void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(100);
        }
        
        
        if (circleMarker != null)
        {
            circleStartingSize = new Vector3(circleMarker.transform.localScale.x, circleMarker.transform.localScale.y, circleMarker.transform.localScale.z);
        }
    }
    
    public void RestoreHealth(int healthRecovered)
    {
        currentHealth += healthRecovered;
        currentHealth = Mathf.Min(currentHealth, maxHealth); 
        if (healthBar != null)
        {
            healthPercentage = (currentHealth / maxHealth) * 100;
            healthBar.UpdateHealthBar(healthPercentage);
        }

        if (targetType == eTargetType.PMonument)
        {
            GetComponent<MonumentStates>().UpdateStates();
        }
    }

    public void ActivateTargetIcon()
    {
        if (targetIcon == null)
        {
            return;
        }
        
        targetIcon.SetActive(true);
    }
    
    public void DeactivateTargetIcon()
    {
        if (targetIcon == null)
        {
            return;
        }
        
        targetIcon.SetActive(false);
    }
    
    public void ActivateCircleAreaMarker(float circleRange)
    {
        if (circleMarker == null)
        {
            return;
        }
        
        circleMarker.transform.localScale = circleStartingSize * circleRange;
        circleMarker.SetActive(true);
    }
    
    public void DeactivateCircleAreaMarker()
    {
        if (circleMarker == null)
        {
            return;
        }
        
        circleMarker.SetActive(false);
        circleMarker.transform.localScale = circleStartingSize;
    }

    public void ActivateConeAreaMarker()
    {
        if (coneMarker == null)
        {
            return;
        }
        
        coneMarker.SetActive(true);
    }
    
    public void DeactivateConeAreaMarker()
    {
        if (coneMarker == null)
        {
            return;
        }
        
        coneMarker.SetActive(false);
    }

    #region Status Effects

    public void ApplyStatus(StatusEffect status, Combatant inflictedBy)
    {
        status.inflictedBy = inflictedBy;

        if (currentHealth <= 0)
        {
            return;
        }

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
            // ReSharper disable once UnusedVariable
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
    
    public void TakeDamage(float rawDamage)
    {
        float damageAfterReduction = rawDamage * (1 - damageReduction/100);
        currentHealth -= damageAfterReduction;
        
        if (healthBar != null)
        {
            healthPercentage = (currentHealth / maxHealth ) * 100;
            healthBar.UpdateHealthBar(healthPercentage);
        }
        
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            
            if (healthBar != null)
            {
                healthPercentage = (currentHealth / maxHealth ) * 100;
                healthBar.UpdateHealthBar(healthPercentage); 
            }
            
            if (targetType == eTargetType.Player || targetType == eTargetType.Enemy)
            {
                Die();
            }

            if (targetType == eTargetType.PMonument)
            {
                GetComponent<MonumentStates>().UpdateStates();
            }
        }

        if (targetType == eTargetType.Player)
        {
            GetComponent<GodBehaviour>().OnDamageEvent(damageAfterReduction);
        }

        if(targetType == eTargetType.PMonument)
        {
            GetComponent<MonumentStates>().UpdateStates();
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
                GetComponent<AIBrain>().OnDeathEvent();
                GameManager.Instance.AddRespect(respectOnKill);
                break;
        }
    }
}