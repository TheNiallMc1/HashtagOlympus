using _Project.Scripts.AI.AiControllers;
using UnityEngine;

[CreateAssetMenu(fileName = "HangoverStatus", menuName = "Status Effect/Hangover", order = 1)]
public class Status_Hangover : StatusEffect
{
    private AIBrain aiBrain;
    private AIMovement movementMotor;

    public int hangoverDamage = 50;
    
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

                affectedCombatant.TakeDamage(hangoverDamage);

                aiBrain.State = AIBrain.EState.Hangover;
                aiBrain._isHungover = true;

                movementMotor.nav.isStopped = true;
                movementMotor.animator.SetTrigger("Hangover");
        }
        }
    
        public override void ExitEffect()
        {
            if (affectedCombatant.targetType == Combatant.eTargetType.Enemy)
            {
                movementMotor.animator.ResetTrigger("Hangover");
                movementMotor.animator.SetTrigger("SoberedUp");
                movementMotor.animator.ResetTrigger("SoberedUp");

                aiBrain._isHungover = false;
                aiBrain.State = AIBrain.EState.Moving;
                movementMotor.nav.isStopped = false;
            }
        }
}
