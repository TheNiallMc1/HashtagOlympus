using UnityEngine;


[CreateAssetMenu(fileName = "AbilityExample", menuName = "Abilities/New Ability", order = 1)]
public class Ability_SO : SpecialAbility
{
    public override void AbilityEffect()
    {
        ExecuteAbility();
    }
    
    public override void ExecuteAbility()
    {
        foreach (Combatant target in targets)
        {
            DealDamage(target);
            InflictStatusEffects(target);
            RestoreHealth(target);
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
        foreach (StatusEffect statusEffect in statusEffects)
        {
            target.ApplyStatus(statusEffect, thisGod.gameObject.GetComponent<Combatant>());
        }
    }
    
    public override void EndAbility()
    {
        
    }
}
