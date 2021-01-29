using _Project.Scripts.AI.AiControllers;
using UnityEngine;

[CreateAssetMenu(fileName = "FreezeStatus", menuName = "Status Effect/Freeze", order = 1)]
public class Status_Freeze : StatusEffect
{
    [Header("Freeze Variables")] 
    private float baseSpeed;
    private AIBrain aiBrain;
    
    public override void TickEffect()
        {
            throw new System.NotImplementedException();
        }
    
        public override void EntryEffect()
        {
            if (affectedCombatant.targetType == Combatant.eTargetType.Enemy)
            {
                            
                aiBrain = affectedCombatant.GetComponent<AIBrain>();
                aiBrain.State = AIBrain.EState.Frozen;
            }
        }
    
        public override void ExitEffect()
        {
            aiBrain.State = AIBrain.EState.Moving;
        }
}
