using UnityEngine;

public class DemeterSummerAnimations : MonoBehaviour
{
    private God_Demeter godBehaviour;
    private Combatant godCombatant;

    private readonly AbilityManager[] abilities = new AbilityManager[2];

    [SerializeField] 
    private GameObject monumentHealParticles;
    [SerializeField]
    private GameObject cornMesh;
    [SerializeField]
    private GameObject cornHealParticles;

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
    
    
    public void Ability01Effect()
    {
        abilities[0].ability.StartAbility();
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
    
    public void Ability02Start()
    {
        abilities[1].ability.StartAbility();
    }
    
    public void ActivateCornHealMesh()
    {
        cornMesh.SetActive(true);
    }

    public void ActivateCornHealParticles()
    {
        cornHealParticles.SetActive(true);
    }
    
    public void DeactivateCornHealMesh()
    {
        cornMesh.SetActive(false);
    }
    
    public void DeactivateCornHealParticles()
    {
        cornHealParticles.SetActive(false);
    }
    
    public void EndAbility02()
    {
        abilities[1].StartCooldown();
        godBehaviour.currentState = GodState.idle;
    }
}
