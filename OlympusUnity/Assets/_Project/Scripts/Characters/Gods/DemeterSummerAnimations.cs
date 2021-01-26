using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemeterSummerAnimations : MonoBehaviour
{
    GodBehaviour godBehaviour;
    Combatant godCombatant;

    // Start is called before the first frame update
    void Start()
    {
        godBehaviour = GetComponentInParent<GodBehaviour>();
        godCombatant = GetComponentInParent<Combatant>();
    }


    // Animation Events
    public void TakeDamageAnimation()
    {
        Combatant target = godBehaviour.currentAttackTarget;
        if (target != null)
        {
            target.TakeDamage(godCombatant.attackDamage);
        }
    }

    public void Dead()
    {
        Debug.Log(godCombatant.name + " has died");
    }

    public void AnimationIsPlaying()
    {
        godBehaviour.attackAnimationIsPlaying = true;
    }
    public void AnimationIsFinished()
    {
        godBehaviour.attackAnimationIsPlaying = false;
    }
}
