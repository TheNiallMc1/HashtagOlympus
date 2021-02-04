using System.Collections.Generic;
using UnityEngine;

public sealed class God_Dionysus : GodBehaviour
{
    [Header("Dionysus")] 
    public PassiveAbilityManager partyTimePassiveManager;
    private PassiveAbility partyPassiveAbility;

    public List<Combatant> combatantsAffectedByPartyTime;

    [Header("Testing Dionysus")] 
    public bool inTestMode;
    public GameObject passiveIcon;

    public override void Start()
    {
        base.Start();
        partyTimePassiveManager.enabled = false;

        specialAbilities[0].abilityStateName = "Dionysus_Ability01";
        specialAbilities[1].abilityStateName = "Dionysus_Ability02";

        ultimateStartAnimTrigger = "Dionysus_Ultimate";
        ultimateFinishAnimTrigger = "UltimateFinish";
    }

    public override void ActivateUltimate()
    {
        if ( !CanUseAbility() )
        {
            return;
        }
        
        if (ultimateCharge >= 100)
        {
            if (inTestMode)
            {
                passiveIcon.SetActive(false);
            }
            
            usingUltimate = true;
            ultimateCharge = 100; // Set to 100 in case it somehow went over

            attackAnimationIsPlaying = false;
            //animator.SetTrigger(ultimateStartAnimTrigger);
            animator.Play(ultimateStartAnimTrigger);
            
            partyTimePassiveManager.enabled = true;
            partyPassiveAbility = partyTimePassiveManager.ability;
            
            partyTimePassiveManager.Initialise();
            
            StartCoroutine(UltimateDurationCoroutine());
        }
    }

    protected override void EndUltimate()
    {
        animator.SetTrigger(ultimateFinishAnimTrigger);
    }

    public void UltimateExitEffects()
    {
        if (inTestMode)
        {
            passiveIcon.SetActive(true);
        }
        
        partyTimePassiveManager.RemovePassiveAbility();
        
        // Take and store the list of targets affected by the status
        combatantsAffectedByPartyTime = partyPassiveAbility.targetsAffectedByStatus;
        
        foreach (Combatant target in combatantsAffectedByPartyTime)
        {
            target.RemoveStatus(partyPassiveAbility.statusEffect);
        }
        
        combatantsAffectedByPartyTime.Clear();
        
        base.EndUltimate();
    }
}
