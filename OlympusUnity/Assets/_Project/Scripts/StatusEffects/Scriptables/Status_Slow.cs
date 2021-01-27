using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used for basic healing. Heals a character once, immediately, and then is removed
[CreateAssetMenu(fileName = "SlowStatus", menuName = "Status Effect/Slow", order = 1)]
public class Status_Slow : StatusEffect
{
    [Header("Slow Variables")]
    [SerializeField] 
    [Range(1, 100)] protected float speedReductionPercentage;
    private float baseSpeed;
    private float newSpeed;
    
    public override void TickEffect()
    {
        throw new System.NotImplementedException();
    }

    public override void EntryEffect()
    {
        // baseSpeed = affectedCombatant.navMeshAgent.speed;
        // newSpeed = speed / speedReductionPercentage
        // affectedCombatant.navMeshAgent.speed = newSpeed 
    }

    public override void ExitEffect()
    {
        // affectedCombatant.navMeshAgent.speed = baseSpeed
    }
}