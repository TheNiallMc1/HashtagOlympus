using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used for Dionysus' Ultimate ability, this causes every enemy to follow him and then take damage when the ability ends
[CreateAssetMenu(fileName = "PartyStatus", menuName = "Status Effect/Party", order = 1)]
public class Status_Party : StatusEffect
{
    [Header("Party Variables")]
    
    [Tooltip("The damage this effect inflicts when it ends")] 
    [SerializeField] protected int burstDamage;
    
    public override void TickEffect()
    {
        
    }

    public override void EntryEffect()
    {
        // Set to follow Dionysus
    }

    public override void ExitEffect()
    {
        // Deal damage to everyone afflicted
        Debug.Log("party damage");
        affectedCombatant.TakeDamage(burstDamage);
    }
}