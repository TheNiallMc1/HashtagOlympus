using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemeterSummerAnimations : MonoBehaviour
{
    God_Demeter godBehaviour;
    Combatant godCombatant;

    AbilityManager[] abilities = new AbilityManager[2];

    // Start is called before the first frame update
    void Start()
    {
        godBehaviour = GetComponentInParent<God_Demeter>();
        godCombatant = GetComponentInParent<Combatant>();

        abilities[0] = godBehaviour.summerAbilities[0];
        abilities[1] = godBehaviour.summerAbilities[1];
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

    public void AbilityEffect(int abilityIndex)
    {
        Debug.Log("Executing ability from animation");
        abilities[abilityIndex].ability.StartAbility();
        abilities[abilityIndex].StartCooldown();
    }


}
