using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using UnityEngine;

[RequireComponent(typeof(Combatant))]
public class StatusEffectManager : MonoBehaviour
{
    [HideInInspector]
    public Combatant thisCombatant;
    public List<StatusEffect> activeEffects;

    private void Update()
    {
        
    }

    public void AddStatusEffect(StatusEffect effect)
    {
        
        if (activeEffects.Contains(effect))
        {
            Debug.LogWarning("AddStatusEffect(): The effect " + effect.name + " already exists on this entity");
        }
        else
        {
            effect.affectedCombatant = thisCombatant; // Set the effect's combatant
            activeEffects.Add(effect);
        }
    }
    
    public void RemoveStatusEffect(StatusEffect effect)
    {
        if (activeEffects.Contains(effect))
        {
            activeEffects.Remove(effect);
        }
        else
        {
            Debug.LogWarning("RemoveStatusEffect(): The effect " + effect.name + " does not exist on this entity");
        }
    }

    private void ActivateTickEffect()
    {
        
    }

    private void ActivateEntryEffect()
    {
        
    }

    private void ActivateExitEffect()
    {
        
    }
    
    
}