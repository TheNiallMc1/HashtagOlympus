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
    
    private Coroutine tickCoroutine;
    private Coroutine durationCoroutine;

    private bool effectInitialised;
    
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
        if (statusEffect.isTickType)
        {
            tickCoroutine = StartCoroutine(TickEffectCoroutine());
        }

        durationCoroutine = StartCoroutine(DurationCoroutine());
        
        ActivateEntryEffect();
        effectInitialised = true;
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
        
        thisCombatant.RemoveStatus(statusEffect);
        
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