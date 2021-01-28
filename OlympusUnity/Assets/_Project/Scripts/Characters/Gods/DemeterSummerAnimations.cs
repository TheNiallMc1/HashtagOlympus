using UnityEngine;

public class DemeterSummerAnimations : MonoBehaviour
{
    God_Demeter godBehaviour;
    Combatant godCombatant;

    AbilityManager[] abilities = new AbilityManager[2];

    // Start is called before the first frame update
    void Start()
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
    
    public void Ability02Start()
    {
        abilities[1].ability.StartAbility();
    }
    
    public void EndAbility02()
    {
        abilities[1].StartCooldown();
        godBehaviour.currentState = GodState.idle;
    }


}
