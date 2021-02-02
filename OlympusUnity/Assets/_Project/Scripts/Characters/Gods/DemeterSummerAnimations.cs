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
    [SerializeField]
    GameObject rightHand;

    [HideInInspector]
    Vector3 cornStartPosition;
    Quaternion cornStartRotation;

    // Start is called before the first frame update
    private void Start()
    {
        godBehaviour = GetComponentInParent<God_Demeter>();
        godCombatant = GetComponentInParent<Combatant>();

        abilities[0] = godBehaviour.summerAbilities[0];
        abilities[1] = godBehaviour.summerAbilities[1];

        cornStartPosition = cornMesh.transform.localPosition;
        cornStartRotation = cornMesh.transform.rotation;
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


    public void UnparentCorn()
    {
        cornMesh.transform.parent = godCombatant.transform;
        Rigidbody rb = cornMesh.GetComponent<Rigidbody>();

        // print(cornMesh.transform.position);
        cornMesh.transform.position = rightHand.transform.position;

        // print(cornMesh.transform.position);
        // rb.isKinematic = true;
        // rb.AddForce(0, 3f, 0);
        // Debug.Break();
    }


    public void DeactivateCornHealMesh()
    {
        cornMesh.SetActive(false);
    }

    public void ReparentCorn()
    {
        cornMesh.transform.parent = rightHand.transform;
        cornMesh.transform.localPosition = cornStartPosition;
        cornMesh.transform.rotation = cornStartRotation;
    }


    public void ActivateCornHealParticles()
    {
        cornHealParticles.SetActive(true);
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
