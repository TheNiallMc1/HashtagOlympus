using UnityEngine;

public class AresAnimations : MonoBehaviour
{
    GodBehaviour godBehaviour;
    Combatant godCombatant;
    
    AbilityManager[] abilities = new AbilityManager[2];
    
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
        abilities[0].StartCooldown();
    }
    
    public void Ability02Effect()
    {
        abilities[1].ability.StartAbility();
        abilities[1].StartCooldown();
    }
}
