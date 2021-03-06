﻿using UnityEngine;
using UnityEngine.AI;

public class DemeterWinterAnimations : MonoBehaviour
{
    private God_Demeter godBehaviour;
    private Combatant godCombatant;

    private readonly AbilityManager[] abilities = new AbilityManager[2];

    // Auto-Attacks
    [SerializeField] ParticleSystem leftHandEffect;
    [SerializeField] ParticleSystem rightHandEffect;
    [SerializeField] GameObject autoAttackBlast;
    private GameObject autoAttackBlastInstance;


    // Ability 01
    [SerializeField] 
    private GameObject icicleMesh;
    private GameObject icicleInstance;

    [SerializeField] GameObject groundIceEffect;
    private GameObject groundIceEffectInstance;

    // Ability 02
    [SerializeField]
    private GameObject icyWindParticles;
    ParticleSystem[] icyWindParticleSystems;

    [SerializeField]
    private GameObject icyWindCone;


    // Ultimate
    public GameObject ultimateParticlesBuildupPrefab;
    [HideInInspector] GameObject ultimateParticlesBuildupInstance;

    public GameObject ultimateParticlesExplodePrefab;
    [HideInInspector] GameObject ultimateParticlesExplodeInstance;

    // Start is called before the first frame update
    private void Start()
    {
        godBehaviour = GetComponentInParent<God_Demeter>();
        godCombatant = GetComponentInParent<Combatant>();

        abilities[0] = godBehaviour.winterAbilities[0];
        abilities[1] = godBehaviour.winterAbilities[1];

        icyWindParticleSystems = icyWindParticles.GetComponentsInChildren<ParticleSystem>();
    }


    // General
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
        
    }

    public void AnimationIsPlaying()
    {
        godBehaviour.attackAnimationIsPlaying = true;
    }
    public void AnimationIsFinished()
    {
        godBehaviour.attackAnimationIsPlaying = false;
    }
    

    public void LockMovement()
    {
        godBehaviour.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
    }

    public void UnlockMovement()
    {
        godBehaviour.gameObject.GetComponent<NavMeshAgent>().isStopped = false;
    }

    // Auto-attacks

    private void TurnOnHandsEffect()
    {
        leftHandEffect.Play();
        rightHandEffect.Play();
    }

    private void TurnOffHandsEffect()
    {
        leftHandEffect.Stop();
        rightHandEffect.Stop();
    }


    private void SpawnAutoBlast()
    {
        Combatant target = godBehaviour.currentAttackTarget;
        autoAttackBlastInstance = Instantiate(autoAttackBlast, target.transform.position, Quaternion.identity);
        Destroy(autoAttackBlastInstance, 1);
    }



    // Ability 1
    public void Ability01Start()
    {
        // This needs to hold for the length of the ability lifetime, and then end the ability
        abilities[0].ability.AbilityEffect();
    }


    public void ActivateIcicleMesh()
    {
        Combatant target = abilities[0].ability.targets[0];
        icicleInstance = Instantiate(icicleMesh, target.transform.position, Quaternion.identity);
        groundIceEffectInstance = Instantiate(groundIceEffect, target.transform.position, Quaternion.identity);


        Destroy(icicleInstance, 3f);
        Destroy(groundIceEffectInstance, 4.5f);

    }

    
    public void EndAbility01()
    {
        abilities[0].StartCooldown();
    }




    // Ability 2
    public void Ability02Effect()
    {
        // abilities[1].ChannelAbilityTick();
        // abilities[1].ability.StartAbility();
        abilities[1].ability.AbilityEffect();
    }
    
    public void ActivateIcyWindParticles()
    {

        foreach (ParticleSystem particleSystem in icyWindParticleSystems)
        {
            particleSystem.Play();
        }
    }
    

    public void StopIcyWindParticles()
    {
        foreach(ParticleSystem particleSystem in icyWindParticleSystems)
        {
            particleSystem.Stop();
        }
    }


    public void EndAbility02()
    {
        abilities[1].StartCooldown();
    }





    // Ultimate
    public void UltimateParticleBuildUp()
    {
        ultimateParticlesBuildupInstance = Instantiate(ultimateParticlesBuildupPrefab, transform.position, Quaternion.identity, transform);
        Destroy(ultimateParticlesBuildupInstance, 2.5f);
    }

    public void UltimateParticleExplode()
    {
        ultimateParticlesExplodeInstance = Instantiate(ultimateParticlesExplodePrefab, transform.position, Quaternion.identity, godBehaviour.transform);
        Destroy(ultimateParticlesExplodeInstance, 1.2f);
    }


    public void SwitchFormsAnim()
    {
        godBehaviour.SwitchForms();
    }
}
