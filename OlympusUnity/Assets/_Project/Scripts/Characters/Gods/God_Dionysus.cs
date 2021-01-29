﻿using System.Collections.Generic;
using UnityEngine;

public class God_Dionysus : GodBehaviour
{
    [Header("Dionysus")] 
    public PassiveAbilityManager partyTimePassiveManager;
    private StatusEffect partyStatus;
    private PassiveAbility partyPassiveAbility;

    public List<Combatant> combatantsAffectedByPartyTime;

    public override void Start()
    {
        base.Start();
        partyTimePassiveManager.enabled = false;

        specialAbilities[0].abilityStateName = "Dionysus_Ability01";
        specialAbilities[1].abilityStateName = "Dionysus_Ability02";

        ultimateStartAnimTrigger = "Dionysus_Ultimate";
        ultimateFinishAnimTrigger = "UltimateFinish";

        partyStatus = partyTimePassiveManager.ability.statusEffect;
    }

    public override void ActivateUltimate()
    {
        if ( !CanUseAbility() )
        {
            return;
        }
        
        if (ultimateCharge >= 100)
        {
            ultimateCharge = 100; // Set to 100 in case it somehow went over
            currentState = GodState.usingUltimate;

            attackAnimationIsPlaying = false;
            //animator.SetTrigger(ultimateStartAnimTrigger);
            animator.Play(ultimateStartAnimTrigger);
            
            partyTimePassiveManager.enabled = true;
            partyPassiveAbility = partyTimePassiveManager.ability;
            
            partyTimePassiveManager.Initialise();
            
            ultimateDecreaseCoroutine = StartCoroutine(UltimateDurationCoroutine());
        }
    }

    protected override void EndUltimate()
    {
        partyTimePassiveManager.RemovePassiveAbility();
        
        animator.SetTrigger(ultimateFinishAnimTrigger);
    }

    public override void UltimateExitEffects()
    {
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
