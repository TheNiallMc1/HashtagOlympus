using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Combatant))]
public class StatusEffectManager : MonoBehaviour
{
    public StatusEffect originalStatus;
    public StatusEffect instancedStatus;
    public float remainingDuration;
    
    private Coroutine tickCoroutine = null;
    private Coroutine durationCoroutine = null;
    
    [HideInInspector]
    public Combatant thisCombatant;

    private void Start()
    {
        thisCombatant = GetComponent<Combatant>();

        instancedStatus = Instantiate(originalStatus);
        
        InitialiseStatus();
    }
    
    // We initialise each new effect that is added, starting its tick cycle and activating the entry effect
    private void InitialiseStatus()
    {        
        instancedStatus.affectedCombatant = thisCombatant;
        
        // Tick
        if (instancedStatus.isTickType)
        {
            ActivateTickEffect();
        }
        
        // Duration
        if (instancedStatus.isInfinite) // If infinite, call enter event and wait to be removed to do exit
        {
            ActivateEntryEffect();
        }
        
        else if (instancedStatus.statusDuration == 0) // If it is set to zero, just fire the enter and exit effects straight away
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
        instancedStatus.TickEffect();
        tickCoroutine = StartCoroutine(TickEffectCoroutine());
    }

    private void ActivateEntryEffect()
    {
        instancedStatus.EntryEffect();
    }

    public void ActivateExitEffect()
    {
        instancedStatus.ExitEffect();
        EndStatus();
    }

    private IEnumerator TickEffectCoroutine()
    {
        yield return new WaitForSecondsRealtime(instancedStatus.tickInterval);
        ActivateTickEffect();
    }
    
    private IEnumerator DurationCoroutine()
    {
        Debug.Log("Begin duration coroutine");
        yield return new WaitForSecondsRealtime(instancedStatus.statusDuration);
        thisCombatant.RemoveStatus(originalStatus);
    }
    
}