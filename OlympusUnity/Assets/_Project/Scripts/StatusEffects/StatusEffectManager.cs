using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Combatant))]
public class StatusEffectManager : MonoBehaviour
{
    public StatusEffect statusEffect;
    public float remainingDuration;
    
    private Coroutine tickCoroutine = null;
    private Coroutine durationCoroutine = null;
    
    [HideInInspector]
    public Combatant thisCombatant;

    private void Start()
    {
        thisCombatant = GetComponent<Combatant>();

        statusEffect = Instantiate(statusEffect);
        
        InitialiseStatus();
    }
    
    // We initialise each new effect that is added, starting its tick cycle and activating the entry effect
    private void InitialiseStatus()
    {
        statusEffect.affectedCombatant = thisCombatant;
        
        // Tick
        if (statusEffect.isTickType)
        {
            ActivateTickEffect();
        }
        
        // Duration
        if (statusEffect.isInfinite) // If infinite, call enter event and wait to be removed to do exit
        {
            ActivateEntryEffect();
        }
        
        else if (statusEffect.statusDuration == 0) // If it is set to zero, just fire the enter and exit effects straight away
        {
            ActivateEntryEffect();
            ActivateExitEffect();
        }
        
        else // Start the duration coroutine 
        {
            durationCoroutine = StartCoroutine(DurationCoroutine());
            ActivateEntryEffect();
        }
    }

    public void EndStatus()
    {
        if (durationCoroutine != null)
        {
            StopCoroutine(durationCoroutine);
        }
        
        if (tickCoroutine != null)
        {
            StopCoroutine(tickCoroutine);
        }
        
        Debug.Log("Ending status");
        Destroy(this);
    }
    
    
    private void ActivateTickEffect()
    {
        statusEffect.TickEffect();
        tickCoroutine = StartCoroutine(TickEffectCoroutine());
    }

    private void ActivateEntryEffect()
    {
        statusEffect.EntryEffect();
    }

    public void ActivateExitEffect()
    {
        statusEffect.ExitEffect();
        EndStatus();
    }

    private IEnumerator TickEffectCoroutine()
    {
        yield return new WaitForSecondsRealtime(statusEffect.tickInterval);
        ActivateTickEffect();
    }
    
    private IEnumerator DurationCoroutine()
    {
        yield return new WaitForSecondsRealtime(statusEffect.statusDuration);
        thisCombatant.RemoveStatus(statusEffect);
    }
    
}