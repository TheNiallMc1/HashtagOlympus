using UnityEngine;

public class God_Ares : GodBehaviour
{
    [Header("Ares")]
    [SerializeField] private StatusEffect rageStatus;
    [SerializeField] private StatusEffect maxRageStatus;

    public GameObject rageParticleEffects;
    
    private StatusEffect lastActivatedRageType; // Stores the last rage type Ares used - maximum or normal
    
    private Coroutine ultimateCoroutine;
    
    public override void Start()
    {
        base.Start();
        ultimateCharge = 0;
        specialAbilities[0].abilityStateName = "Ares_Ability01";
        specialAbilities[1].abilityStateName = "Ares_Ability02";
        
        ultimateStartAnimTrigger = "Ares_Ultimate";
        ultimateFinishAnimTrigger = "UltimateFinish";
    }

    public override void OnDamageEvent(int damageTaken)
    {
        RageUpdate(damageTaken);
    }


    private void RageUpdate(int amountToAdd)
    {
        if (currentState != GodState.usingUltimate)
        {
            ultimateCharge += amountToAdd;
            ultimateCharge = Mathf.Min(ultimateCharge, 100);
        }
    }

    public override void ActivateUltimate()
    {
        if ( !CanUseAbility() )
        {
            return;
        }
        
        // If charged, but not full activate standard rage status. If full, activate maximum rage status
        
        if (ultimateCharge > 0 && ultimateCharge < 100)
        {
            
            thisCombatant.ApplyStatus(rageStatus, thisCombatant);
            lastActivatedRageType = rageStatus;

            attackAnimationIsPlaying = false;
            animator.Play(ultimateStartAnimTrigger);
            
            ultimateDecreaseCoroutine = StartCoroutine(UltimateDurationCoroutine()); 
        }
        
        if (ultimateCharge >= 100)
        {
            
            thisCombatant.ApplyStatus(maxRageStatus, thisCombatant);
            lastActivatedRageType = maxRageStatus;
            
            attackAnimationIsPlaying = false;
            animator.Play(ultimateStartAnimTrigger);
            
            ultimateDecreaseCoroutine = StartCoroutine(UltimateDurationCoroutine());
        }
    }
    
    public override void UltimateExitEffects()
    {
        thisCombatant.RemoveStatus(lastActivatedRageType);
        rageParticleEffects.SetActive(false);
        base.EndUltimate();
    }
}
