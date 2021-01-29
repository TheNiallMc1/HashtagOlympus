using UnityEngine;
using UnityEngine.AI;

// Used for basic healing. Heals a character once, immediately, and then is removed
[CreateAssetMenu(fileName = "SlowStatus", menuName = "Status Effect/Slow", order = 1)]
public class Status_Slow : StatusEffect
{
    [Header("Slow Variables")]
    [SerializeField] 
    [Range(1, 100)] protected float speedReductionPercentage;
    private float baseSpeed;
    private float newSpeed;
    private NavMeshAgent navMeshAgent;
    
    public override void TickEffect()
    {
        throw new System.NotImplementedException();
    }

    public override void EntryEffect()
    {
        navMeshAgent = affectedCombatant.GetComponentInParent<NavMeshAgent>();
        baseSpeed = navMeshAgent.speed;
        float speedReduction = speedReductionPercentage / 100;
        newSpeed = baseSpeed * speedReduction;
        navMeshAgent.speed = newSpeed;
    }

    public override void ExitEffect()
    {
        navMeshAgent.speed = baseSpeed;
    }
}