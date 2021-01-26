using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God_Dionysus : GodBehaviour
{
    [Header("Dionysus")] 
    public PassiveAbilityManager partyTimePassive;

    public List<Combatant> combatantsAffectedByPartyTime;

    public override void Start()
    {
        base.Start();
        partyTimePassive.enabled = false;
    }

    public override void ActivateUltimate()
    {
        if (ultimateCharge >= 100 && !usingUltimate)
        {
            ultimateCharge = 100; // Set to 100 in case it somehow went over
            usingUltimate = true;

            attackingLocked = true;

            partyTimePassive.enabled = true;
            
            ultimateDecreaseCoroutine = StartCoroutine(UltimateDurationCoroutine());
        }
    }

    public override void EndUltimate()
    {
        base.EndUltimate();
        partyTimePassive.enabled = false;
    }
}
