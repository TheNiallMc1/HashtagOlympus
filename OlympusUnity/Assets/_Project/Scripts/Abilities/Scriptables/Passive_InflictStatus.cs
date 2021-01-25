using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Passive", menuName = "Abilities/Passives/Inflict Status", order = 1)]
public class Passive_InflictStatus : PassiveAbility
{
    public override void AbilityEffect()
    {
        Debug.Log("Ability Effect");
        foreach (Combatant target in targets)
        {
            Debug.Log("Applied effect to " + target.gameObject.name);
            target.ApplyStatus(statusEffect);
        }
    }
}
