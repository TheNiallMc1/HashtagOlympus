﻿using UnityEngine;

public class AresAnimations : MonoBehaviour
{
    GodBehaviour godBehaviour;
    Combatant godCombatant;
    
    AbilityManager[] abilities = new AbilityManager[2];

    public GameObject ultimateParticleEffects;
    
    // Start is called before the first frame update
    void Start()
    {
        godBehaviour = GetComponentInParent<GodBehaviour>();
        godCombatant = GetComponentInParent<Combatant>();
        
        abilities[0] = godBehaviour.specialAbilities[0];
        abilities[1] = godBehaviour.specialAbilities[1];
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
        abilities[0].ability.StartAbility();
    }
    
    public void EndAbility01()
    {
        abilities[0].StartCooldown();
        godBehaviour.currentState = GodState.idle;
    }
    
    public void Ability02Effect()
    {
        abilities[1].ability.StartAbility();
    }
    
    public void EndAbility02()
    {
        abilities[1].StartCooldown();
        godBehaviour.currentState = GodState.idle;
    }

    public void ActivateUltimateParticles()
    {
        ultimateParticleEffects.SetActive(true);
    }
    
    public void DeactivateUltimateParticles()
    {
        ultimateParticleEffects.SetActive(false);
    }
}
