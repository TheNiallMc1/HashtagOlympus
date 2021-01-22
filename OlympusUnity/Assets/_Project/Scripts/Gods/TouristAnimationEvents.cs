using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TouristAnimationEvents : MonoBehaviour
{
    Combatant touristCombatant;
    AI_Brain aI_Brain;
    NavMeshAgent navMeshAgent;
    
    // Start is called before the first frame update
    void Start()
    {
        touristCombatant = GetComponentInParent<Combatant>();
        aI_Brain = GetComponentInParent<AI_Brain>();
        navMeshAgent = GetComponentInParent<AI_Movement>().nav;
    }

    
    // Animation Events
    public void TakeDamageAnimation()
    {
        Combatant target = aI_Brain.currentAttackTarget;
        if (target != null)
        {
            target.TakeDamage(touristCombatant.attackDamage);
        }
    }

    public void LockMovement()
    {
        navMeshAgent.isStopped = true;
        aI_Brain.AttackAnimationIsPlaying = true;
    }

    public void UnlockMovement()
    {
        navMeshAgent.isStopped = false;
        aI_Brain.AttackAnimationIsPlaying = false;
    }
}
