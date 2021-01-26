using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Passive", menuName = "Abilities/Passives/Inflict Status", order = 1)]
public class Passive_InflictStatus : PassiveAbility
{
    public override void AbilityEffect()
    {
        foreach (Combatant target in targets)
        {
            target.ApplyStatus(statusEffect);
            // Add target to list of affected targets if it isn't already in the list
            if (!targetsAffectedByStatus.Contains(target))
            {
                targetsAffectedByStatus.Add(target);
            }
            
        }
    }
}
