using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AbilityExample", menuName = "Abilities/Test", order = 1)]
public class Ability_TestAbility : SpecialAbility
{
    public override void BeginCooldown()
    {
        throw new System.NotImplementedException();
    }

    public override void DealDamage(Combatant target)
    {
        target.TakeDamage(abilityDamage);
    }

    public override void ExecuteAbility()
    {
        foreach(Combatant target in targets)
        {
            DealDamage(target);
            Debug.Log("Attacked " + target.gameObject.name);
        }
        targets.Clear();
    }

    public override void InflictStatusEffects()
    {
        throw new System.NotImplementedException();
    }

    public override void InitiateAbility()
    {
        throw new System.NotImplementedException();
    }

}
