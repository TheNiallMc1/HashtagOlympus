using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AbilityExample", menuName = "Abilities/New Ability", order = 1)]
public class Ability_SO : SpecialAbility
{
    public override void StartAbility()
    {
        ExecuteAbility();
    }
    
    public override void ExecuteAbility()
    {
        Debug.Log(abilityName);
        foreach (Combatant target in targets)
        {
            DealDamage(target);
            InflictStatusEffects(target);
            RestoreHealth(target);
            Debug.Log(target.gameObject.name + " took " + abilityDamage + " damage, has " + target.currentHealth + " remaining");
        }
        targets.Clear();
    }
    
    public override void DealDamage(Combatant target)
    {
        target.TakeDamage(abilityDamage);
    }
    
    public override void RestoreHealth(Combatant target)
    {
        target.RestoreHealth(abilityHealAmount);
    }

    public override void InflictStatusEffects(Combatant target)
    {
        //foreach (StatusEffect statusEffect in statusEffects)
        //{
        //    target.ApplyStatus(statusEffect);
        //    Debug.Log(target.gameObject.name + " was infliced with " + statusEffect.name);
        //}
    }
    
    public override void EndAbility()
    {
        
    }
}
