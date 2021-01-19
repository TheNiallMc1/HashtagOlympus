using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used for Dionysus' Ultimate ability, this causes every enemy to follow him and then take damage when the ability ends
[CreateAssetMenu(fileName = "Party Status Effect", menuName = "Status Effect/Party", order = 1)]
public class Status_Party : StatusEffect
{
    protected override void TickEffect()
    {
        throw new System.NotImplementedException();
    }

    protected override void PersistentEffect()
    {
        // Constantly set navDestination to position of combatant who inflicted this effect
        
        // affectedCombatant.navDestination = inflictedBy.navDestination
    }

    protected override void EntryEffect()
    {
        throw new System.NotImplementedException();
    }

    protected override void ExitEffect()
    {
        // Deal damage to everyone afflicted
        
        // affectedCombatant.takeDamage(damageAmount)
    }
}