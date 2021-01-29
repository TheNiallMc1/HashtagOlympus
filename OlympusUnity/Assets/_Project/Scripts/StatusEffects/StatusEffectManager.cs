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
        
        
        ActivateEntryEffect();

        // Tick
        if (instancedStatus.isTickType)
        {
            ActivateTickEffect();
        }
        
        // Duration
        if (!instancedStatus.isInfinite)
        {
            durationCoroutine = StartCoroutine(DurationCoroutine());
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