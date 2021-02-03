using _Project.Scripts.AI.AiControllers;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "FreezeStatus", menuName = "Status Effect/Freeze", order = 1)]
public class Status_Freeze : StatusEffect
{
    [Header("Freeze Variables")]
    private float baseSpeed;
    private AIBrain aiBrain;
    private AIMovement movementMotor;

    [SerializeField] GameObject iceBlock;
    private GameObject iceBlockInstance;

    public override void TickEffect()
    {
        throw new System.NotImplementedException();
    }

    public override void EntryEffect()
    {
        if (affectedCombatant.targetType == Combatant.eTargetType.Enemy)
        {
            movementMotor = affectedCombatant.GetComponent<AIMovement>();
            aiBrain = affectedCombatant.GetComponent<AIBrain>();
            aiBrain.State = AIBrain.EState.Frozen;
            aiBrain.isFrozen = true;
            iceBlockInstance = Instantiate(iceBlock, affectedCombatant.transform.position, Quaternion.identity, affectedCombatant.transform);
        }
    }

    public override void ExitEffect()
    {
        movementMotor.animator.speed = 1;
        aiBrain.isFrozen = false;
        aiBrain.State = AIBrain.EState.Moving;
        Destroy(iceBlockInstance);
        iceBlockInstance = null;
    }
}