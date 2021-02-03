using _Project.Scripts.AI.AiControllers;
using UnityEngine;

// Used for Dionysus' Ultimate ability, this causes every enemy to follow him and then take damage when the ability ends
[CreateAssetMenu(fileName = "PartyStatus", menuName = "Status Effect/Party", order = 1)]
public class Status_Party : StatusEffect
{
    [Header("Party Variables")] [Tooltip("The damage this effect inflicts when it ends")] [SerializeField]
    protected int burstDamage;
    public StatusEffect hangoverStatus;

    private AIBrain aiBrain;
    private AIMovement movementMotor;

    public override void TickEffect()
    {
        if (affectedCombatant.targetType == Combatant.eTargetType.Enemy)
        {
            aiBrain.currentFollowTarget = inflictedBy;
            aiBrain.State = AIBrain.EState.Party;
            movementMotor.nav.SetDestination(inflictedBy.transform.position);
        }
    }

    public override void EntryEffect()
    {
        // If the combatant is a tourist, make them enter party state
        if (affectedCombatant.targetType == Combatant.eTargetType.Enemy)
        {
            aiBrain = affectedCombatant.gameObject.GetComponent<AIBrain>();
            movementMotor = affectedCombatant.gameObject.GetComponent<AIMovement>();
            
            aiBrain.currentFollowTarget = inflictedBy;
            aiBrain.State = AIBrain.EState.Party;
            aiBrain.ActivateParty();
        }
    }

    public override void ExitEffect()
    {
        if (affectedCombatant.targetType == Combatant.eTargetType.Enemy)
        {
            affectedCombatant.ApplyStatus(hangoverStatus, inflictedBy);
        }
    }
}