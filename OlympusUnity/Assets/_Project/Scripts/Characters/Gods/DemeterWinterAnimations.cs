using UnityEngine;
using UnityEngine.AI;

public class DemeterWinterAnimations : MonoBehaviour
{
    private God_Demeter godBehaviour;
    private Combatant godCombatant;

    private readonly AbilityManager[] abilities = new AbilityManager[2];

    [SerializeField] 
    private GameObject icicleMesh;
    private GameObject icicleInstance;
    [SerializeField]
    private GameObject icyWindParticles;
    [SerializeField]
    private GameObject icyWindCone;

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




    public void Ability01Start()
    {
        // This needs to hold for the length of the ability lifetime, and then end the ability
        abilities[0].ability.AbilityEffect();
    }

    public void ActivateIcicleMesh()
    {
        icicleInstance = Instantiate(icicleMesh);

        Vector3 enemyPosition = abilities[0].lastSingleTarget.transform.position;
        Vector3 newPosition = new Vector3(enemyPosition.x, 0, enemyPosition.z);
        
        icicleInstance.transform.position = newPosition;
    }
    
    public void DeactivateIcicleMesh()
    {
        Destroy(icicleInstance);
    }
    
    public void EndAbility01()
    {
        abilities[0].StartCooldown();
    }
    
    public void Ability02Effect()
    {
        // abilities[1].ChannelAbilityTick();
        // abilities[1].ability.StartAbility();
        abilities[1].ability.AbilityEffect();
    }
    
    public void ActivateIcyWindParticles()
    {
        icyWindParticles.SetActive(true);
        // icyWindCone.SetActive(true);
    }
    
    public void DeactivateIcyWindParticles()
    {
        icyWindParticles.SetActive(false);
        // icyWindCone.SetActive(false);
    }
    
    public void EndAbility02()
    {
        abilities[1].StartCooldown();
    }


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
