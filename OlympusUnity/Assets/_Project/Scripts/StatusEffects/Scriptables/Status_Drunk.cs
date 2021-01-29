using _Project.Scripts.AI.AiControllers;
using UnityEngine;

// Used for Dionysus' Ultimate ability, this causes every enemy to follow him and then take damage when the ability ends
[CreateAssetMenu(fileName = "DrunkStatus", menuName = "Status Effect/Drunk", order = 1)]
public class Status_Drunk : StatusEffect
{
    private AIBrain aiBrain;
    
    public override void TickEffect()
    {
    }

    public override void EntryEffect()
    {
        // If the combatant is a tourist, make them drunk
        if (affectedCombatant.targetType == Combatant.eTargetType.Enemy)
        {
            aiBrain = affectedCombatant.gameObject.GetComponent<AIBrain>();
            aiBrain.State = AIBrain.EState.Drunk;
        }
    }

    public override void ExitEffect()
    {
        if (affectedCombatant.targetType == Combatant.eTargetType.Enemy)
        {
            aiBrain.State = AIBrain.EState.Moving;
        }
    }
}