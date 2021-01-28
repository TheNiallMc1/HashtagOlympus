﻿using UnityEngine;

public class DionysusAnimations : MonoBehaviour
{
    God_Dionysus godBehaviour;
    Combatant godCombatant;

    readonly AbilityManager[] abilities = new AbilityManager[2];
    public GameObject wineParticlesObj;
    private ParticleSystem wineParticleSystem;

    public GameObject healParticlesObj;
    private ParticleSystem healParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        wineParticlesObj.SetActive(false);
        healParticlesObj.SetActive(false);
        
        wineParticleSystem = wineParticlesObj.GetComponent<ParticleSystem>();
        healParticleSystem = healParticlesObj.GetComponent<ParticleSystem>();
        
        godBehaviour = GetComponentInParent<God_Dionysus>();
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
        Debug.Log("Executing ability from animation");
        abilities[0].ability.StartAbility();
    }

    public void ActivateWineParticles()
    {
        wineParticlesObj.SetActive(true);
        //wineParticleSystem.Play();
    }
    
    public void DeactivateWineParticles()
    {
        wineParticlesObj.SetActive(false);
    }

    public void EndAbility01()
    {
        godBehaviour.currentState = GodState.idle;
        Debug.Log("set to idle");
        abilities[0].StartCooldown();
        Debug.Log("Starting cooldown");
    }

    public void Ability02Effect()
    {
        Debug.Log("Executing ability from animation");
        abilities[1].ability.StartAbility();
    }

    public void ActivateHealParticles()
    {
        healParticlesObj.SetActive(true);
        healParticleSystem.Play();
    }
    
    public void DeactivateHealParticles()
    {
        healParticleSystem.Clear();
        healParticleSystem.Stop();
        healParticlesObj.SetActive(false);
    }
    
    public void EndAbility02()
    {
        godBehaviour.currentState = GodState.idle;
        abilities[1].StartCooldown();
    }
}
