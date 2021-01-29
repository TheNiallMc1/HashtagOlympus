using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.AI.AiControllers;
using UnityEngine;

[CreateAssetMenu(fileName = "HangoverStatus", menuName = "Status Effect/Hangover", order = 1)]
public class Status_Hangover : StatusEffect
{
    private AIBrain aiBrain;
    private AIMovement movementMotor;
    
    public override void TickEffect()
        {
            throw new System.NotImplementedException();
        }
    
        public override void EntryEffect()
        {
            // Switch to moving state and stop all movement
            if (affectedCombatant.targetType == Combatant.eTargetType.Enemy)
            {
                aiBrain = affectedCombatant.gameObject.GetComponent<AIBrain>();
                movementMotor = affectedCombatant.gameObject.GetComponent<AIMovement>();
                
                aiBrain.State = AIBrain.EState.Moving;
                movementMotor.nav.isStopped = true;
            }
        }
    
        public override void ExitEffect()
        {
            if (affectedCombatant.targetType == Combatant.eTargetType.Enemy)
            {
                aiBrain.State = AIBrain.EState.Moving;
                movementMotor.nav.isStopped = false;
            }
        }
}
