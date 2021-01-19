using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EACH COMBATANT SHOULD HAVE SIMILAR METHODS AND VARIABLES REFERRING THEMSELVES
// THE DICTIONARY WILL REFER TO STATUS EFFECTS THAT THE COMBATANT CURRENTLY HAS ON THEMSELVES
public class TestScriptForAddingStatus : MonoBehaviour
{
    public Combatant target;
    public StatusEffect status;

    private Dictionary<StatusEffect, StatusEffectManager> statusOutputList = new Dictionary<StatusEffect, StatusEffectManager>(); // Dictionary of statuses this combatant has caused

    public void ApplyStatus()
    {
        // If the status we are trying to apply already exists on the target, dont add it
        if (statusOutputList.ContainsKey(status))
        {
            Debug.LogWarning("The status of " + status.name + " already exists on this entity");
        }
        else
        {
            StatusEffectManager newStatusManager = target.gameObject.AddComponent<StatusEffectManager>();
        
            statusOutputList.Add(status, newStatusManager); 
            // Add an entry in the dictionary, with this type of status as the key and the new manager component as the value
        
            newStatusManager.enabled = true;
            newStatusManager.statusEffect = status;
        }       
    }

    public void RemoveStatus()
    {
        // If the status already exists on the entity, remove it
        if (statusOutputList.ContainsKey(status))
        {
            // Get the value by its key (the status type) and then end the status
            statusOutputList.TryGetValue(status, out StatusEffectManager manager);
            manager?.EndStatus();

            // Remove from dictionary
            statusOutputList.Remove(status);
        }
        
        else
        {
            Debug.LogWarning("The status of " + status.name + " does not exist on this entity");
        }
    }
}