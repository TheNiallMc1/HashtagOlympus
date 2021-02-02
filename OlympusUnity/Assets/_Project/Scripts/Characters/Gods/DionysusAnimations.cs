using UnityEngine;

public class DionysusAnimations : MonoBehaviour
{
    private God_Dionysus godBehaviour;
    private Combatant godCombatant;

    private readonly AbilityManager[] abilities = new AbilityManager[2];
    public GameObject wineParticlesObj;

    public GameObject healParticlesObj;
    private ParticleSystem healParticleSystem;

    // Start is called before the first frame update
    private void Start()
    {
        wineParticlesObj.SetActive(false);
        healParticlesObj.SetActive(false);
        
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
        abilities[0].ability.AbilityEffect();
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
        abilities[0].StartCooldown();
    }

    public void Ability02Effect()
    {
        abilities[1].ability.AbilityEffect();
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

    public void EndUltimateEffect()
    {
        godBehaviour.UltimateExitEffects();
    }
}
