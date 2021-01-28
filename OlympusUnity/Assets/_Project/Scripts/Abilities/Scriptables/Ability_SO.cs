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
        Debug.Log(thisGod.name + " cast " + abilityName);
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
        Debug.Log(target.characterName + " took " + abilityDamage + " damage");
    }
    
    public override void RestoreHealth(Combatant target)
    {
        target.RestoreHealth(abilityHealAmount);
        Debug.Log(target.characterName + " restored " + abilityHealAmount + " health");
    }

    public override void InflictStatusEffects(Combatant target)
    {
        foreach (StatusEffect statusEffect in statusEffects)
        {
            target.ApplyStatus(statusEffect, thisGod.gameObject.GetComponent<Combatant>());
            Debug.Log(target.gameObject.name + " was inflicted with " + statusEffect.name);
        }
    }
    
    public override void EndAbility()
    {
        
    }
}
