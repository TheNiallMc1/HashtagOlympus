using UnityEngine;
using UnityEngine.AI;

public class DemeterSummerAnimations : MonoBehaviour
{
    private God_Demeter godBehaviour;
    private Combatant godCombatant;

    private readonly AbilityManager[] abilities = new AbilityManager[2];

    // Auto-Attacks
    [SerializeField] ParticleSystem leftHandEffect;
    [SerializeField] ParticleSystem rightHandEffect;
    [SerializeField] GameObject autoAttackBlast;
    private GameObject autoAttackBlastInstance;

    // Ability 1

    [SerializeField] private GameObject monumentHealParticles;


    // Ability 2
    [SerializeField] GameObject cornMesh;
    [SerializeField] GameObject cornHealParticles;
    public GameObject healParticlesObj;
    [HideInInspector] GameObject healEffectInstance;

    public GameObject ultimateParticlesBuildupPrefab;
    [HideInInspector] GameObject ultimateParticlesBuildupInstance;

    public GameObject ultimateParticlesExplodePrefab;
    [HideInInspector] GameObject ultimateParticlesExplodeInstance;

    // Start is called before the first frame update
    private void Start()
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


    // Monument Heal Effects
    public void Ability01Effect()
    {
        abilities[0].ability.AbilityEffect();
    }


    public void ActivateMonumentHealParticles()
    {
        monumentHealParticles.SetActive(true);
    }
    
    public void DeactivateMonumentHealParticles()
    {
        monumentHealParticles.SetActive(false);
    }
    

    public void EndAbility01()
    {
        abilities[0].StartCooldown();
        godBehaviour.currentState = GodState.idle;
    }
    






    // Corn Heal Effects
    public void Ability02Start()
    {
        abilities[1].ability.AbilityEffect();
    }
    


    public void ActivateCornHealMesh()
    {
        cornMesh.SetActive(true);
    }


    public void ActivateCornParticles()
    {
        cornHealParticles.SetActive(true);
    }


    public void DeactivateCornHealMesh()
    {
        cornMesh.SetActive(false);
    }

    public void DeactivateCornParticles()
    {
        cornHealParticles.SetActive(false);
    }


    public void ActivateTargetHealParticles()
    {
        Combatant target = abilities[1].ability.targets[0];

        GameObject healEffectInstance = Instantiate(healParticlesObj, target.transform.position, Quaternion.identity, target.transform);
        Destroy(healEffectInstance, 2f);
    }

    public void DeactivateCornHealParticles()
    {
        // cornHealParticles.SetActive(false);
    }
    


    public void EndAbility02()
    {
        abilities[1].StartCooldown();
        godBehaviour.currentState = GodState.idle;
    }


    public void UltimateParticleBuildUp()
    {
        ultimateParticlesBuildupInstance = Instantiate(ultimateParticlesBuildupPrefab, transform.position, Quaternion.identity, transform);
        Destroy(ultimateParticlesBuildupInstance, 2f);
    }

    public void UltimateParticleExplode()
    {
        ultimateParticlesExplodeInstance = Instantiate(ultimateParticlesExplodePrefab, transform.position, Quaternion.identity, godBehaviour.transform);
        Destroy(ultimateParticlesExplodeInstance, 2f);
    }




    public void SwitchFormsAnim()
    {
        godBehaviour.SwitchForms();
    }
}
