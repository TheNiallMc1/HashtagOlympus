using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used for basic healing. Heals a character once, immediately, and then is removed
[CreateAssetMenu(fileName = "MaxRageStatus", menuName = "Status Effect/Maximum Rage", order = 1)]
public class Status_MaxRage : StatusEffect
{
    [Header("Rage Variables")]
    [SerializeField] protected int attackDamageIncrease;
    [SerializeField] protected int damageReductionPercentage;
    private int baseDamageReduction;
    private int baseDamage;
    
    public override void TickEffect()
    {
        throw new System.NotImplementedException();
    }

    public override void EntryEffect()
    {
        // Buff target stats
        baseDamage = affectedCombatant.attackDamage;
        affectedCombatant.attackDamage += attackDamageIncrease;
        
        baseDamageReduction = affectedCombatant.damageReduction;
        affectedCombatant.damageReduction = damageReductionPercentage;
    }

    public override void ExitEffect()
    {
        // Reduce target stats to normal
        affectedCombatant.attackDamage = baseDamage;
        affectedCombatant.damageReduction = baseDamageReduction;
    }
}