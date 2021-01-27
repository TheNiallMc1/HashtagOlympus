using UnityEngine;

[CreateAssetMenu(fileName = "DamageReductionStatus", menuName = "Status Effect/Damage Reduction", order = 1)]
public class Status_DamageReduction : StatusEffect
{
    [Header("Damage Reduction Variables")] 
    [Range(1, 100)] public float damageReductionPercentage;
    
    public override void TickEffect()
        {
            throw new System.NotImplementedException();
        }
    
        public override void EntryEffect()
        {
            //affectedCombatant.damageReduction += damageReductionPercentage;
        }
    
        public override void ExitEffect()
        {
            //affectedCombatant.damageReduction -= damageReductionPercentage;
        }
}
