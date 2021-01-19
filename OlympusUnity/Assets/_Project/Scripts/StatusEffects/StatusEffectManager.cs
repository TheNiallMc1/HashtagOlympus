using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using UnityEngine;

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
        
        InitialiseStatus();
    }
    
    // We initialise each new effect that is added, starting its tick cycle and activating the entry effect
    private void InitialiseStatus()
    {
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
        
        if (statusEffect.statusDuration == 0) // If it is set to zero, just fire the enter and exit effects straight away
        {
            ActivateEntryEffect();
            EndStatus();
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
        
        ActivateExitEffect();
        
        thisCombatant.RemoveStatusFromList(statusEffect);
        
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

    private void ActivateExitEffect()
    {
        statusEffect.ExitEffect();
        Debug.Log("Status effect ended");
    }

    private IEnumerator TickEffectCoroutine()
    {
        yield return new WaitForSecondsRealtime(statusEffect.tickInterval);
        ActivateTickEffect();
    }
    
    private IEnumerator DurationCoroutine()
    {
        Debug.Log("duration coroutine started");
        yield return new WaitForSecondsRealtime(statusEffect.statusDuration);
        EndStatus();
    }
    
}