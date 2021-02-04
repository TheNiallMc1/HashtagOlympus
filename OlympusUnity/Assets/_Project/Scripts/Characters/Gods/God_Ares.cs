using UnityEngine;

public sealed class God_Ares : GodBehaviour
{
    [Header("Ares")] 
    [SerializeField] private int rageGainPerDamage;
    [SerializeField] private StatusEffect rageStatus;
    [SerializeField] private StatusEffect maxRageStatus;

    public GameObject rageParticleEffects;
    
    private StatusEffect lastActivatedRageType; // Stores the last rage type Ares used - maximum or normal
    
    private Coroutine ultimateCoroutine;
    private bool inRageMode;
    public Renderer aresEyes;
    public Material whiteEyesMat;

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
        RageUpdate(rageGainPerDamage);
    }


    private void RageUpdate(int amountToAdd)
    {
        if (!inRageMode)
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
            inRageMode = true;
            thisCombatant.ApplyStatus(rageStatus, thisCombatant);
            lastActivatedRageType = rageStatus;

            attackAnimationIsPlaying = false;
            animator.Play(ultimateStartAnimTrigger);
            
            StartCoroutine(UltimateDurationCoroutine()); 
        }
        
        if (ultimateCharge >= 100)
        {
            inRageMode = true;
            thisCombatant.ApplyStatus(maxRageStatus, thisCombatant);
            lastActivatedRageType = maxRageStatus;
            
            attackAnimationIsPlaying = false;
            animator.Play(ultimateStartAnimTrigger);
            
            StartCoroutine(UltimateDurationCoroutine());
        }
    }

    protected override void EndUltimate()
    {
        UltimateExitEffects();
    }
    
    public void UltimateExitEffects()
    {
        inRageMode = false;
        thisCombatant.RemoveStatus(lastActivatedRageType);

        rageParticleEffects.SetActive(false);
        aresEyes.sharedMaterial = whiteEyesMat;

        base.EndUltimate();
    }
}
