using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemeterWinterAnimations : MonoBehaviour
{
    God_Demeter godBehaviour;
    Combatant godCombatant;

    private AbilityManager icicleAbilty;
    private AbilityManager freezeAbility;

    // Start is called before the first frame update
    void Start()
    {
        godBehaviour = GetComponentInParent<God_Demeter>();
        godCombatant = GetComponentInParent<Combatant>();

        icicleAbilty = godBehaviour.winterAbilities[0];
        freezeAbility = godBehaviour.winterAbilities[1];
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
    
    public void IcicleAbilityEffect()
    {
        Debug.Log("Icicle ability animation event");
        //Spawn icicle
        icicleAbilty.ability.StartAbility();
        icicleAbilty.StartCooldown();
    }
    
    public void StartFreezeAbility()
    {
        Debug.Log("Freeze ability animation event");
        // This needs to hold for the length of the ability lifetime, and then end the ability
        freezeAbility.ability.StartAbility();
        freezeAbility.StartCooldown();
    }
}
