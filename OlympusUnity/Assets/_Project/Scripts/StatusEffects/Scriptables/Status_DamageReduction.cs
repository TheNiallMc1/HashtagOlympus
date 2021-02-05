using UnityEngine;

[CreateAssetMenu(fileName = "DamageReductionStatus", menuName = "Status Effect/Damage Reduction", order = 1)]
public class Status_DamageReduction : StatusEffect
{
    [Header("Damage Reduction Variables")] 
    [Range(1, 100)] public int damageReductionPercentage;
    
    public override void TickEffect()
        {
            throw new System.NotImplementedException();
        }
    
        public override void EntryEffect()
        {
            affectedCombatant.damageReduction += damageReductionPercentage;

            if(affectedCombatant.GetComponent<MonumentStates>() != null)
            {
                affectedCombatant.GetComponent<MonumentStates>().ActivateDefenceParticles();
            }
        }
    
        public override void ExitEffect()
        {
            affectedCombatant.damageReduction -= damageReductionPercentage;

            if (affectedCombatant.GetComponent<MonumentStates>() != null)
            {
                affectedCombatant.GetComponent<MonumentStates>().DeactivateDefenceParticles();
            }
    }
}
