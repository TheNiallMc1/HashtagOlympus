using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemeterSummerAnimations : MonoBehaviour
{
    God_Demeter godBehaviour;
    Combatant godCombatant;

    AbilityManager ability01;
    AbilityManager ability02;

    // Start is called before the first frame update
    void Start()
    {
        godBehaviour = GetComponentInParent<God_Demeter>();
        godCombatant = GetComponentInParent<Combatant>();

        ability01 = godBehaviour.summerAbilities[0];
        ability02 = godBehaviour.summerAbilities[1];
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

    public void Ability01Effect()
    {
        ability01.ability.StartAbility();
    }


}
