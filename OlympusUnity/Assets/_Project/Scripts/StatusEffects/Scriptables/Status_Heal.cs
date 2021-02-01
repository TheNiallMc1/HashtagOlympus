using UnityEngine;

// Used for basic healing. Heals a character once, immediately, and then is removed
[CreateAssetMenu(fileName = "HealStatus", menuName = "Status Effect/Heal", order = 1)]
public class Status_Heal : StatusEffect
{
    [Header("Heal Variables")]
    [SerializeField] protected int healAmount;
    
    public override void TickEffect()
    {
        
    }

    public override void EntryEffect()
    {
        // Heal target
        affectedCombatant.RestoreHealth(healAmount);
    }

    public override void ExitEffect()
    {
    }
}