using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God_Dionysus : GodBehaviour
{
    [Header("Dionysus")] 
    public PassiveAbilityManager partyTimePassiveManager;
    public StatusEffect partyStatus;
    private PassiveAbility partyPassiveAbility;

    public List<Combatant> combatantsAffectedByPartyTime;

    public override void Start()
    {
        base.Start();
        partyTimePassiveManager.enabled = false;
    }

    public override void ActivateUltimate()
    {
        if (ultimateCharge >= 100 && !usingUltimate)
        {
            ultimateCharge = 100; // Set to 100 in case it somehow went over
            usingUltimate = true;

            attackingLocked = true;

            partyTimePassiveManager.enabled = true;
            partyPassiveAbility = partyTimePassiveManager.ability;
            
            partyTimePassiveManager.Initialise();
            
            ultimateDecreaseCoroutine = StartCoroutine(UltimateDurationCoroutine());
        }
    }

    public override void EndUltimate()
    {
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
