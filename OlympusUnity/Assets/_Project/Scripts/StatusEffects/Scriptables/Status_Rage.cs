using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used for basic healing. Heals a character once, immediately, and then is removed
[CreateAssetMenu(fileName = "RageStatus", menuName = "Status Effect/Normal Rage", order = 1)]
public class Status_Rage : StatusEffect
{
    [Header("Rage Variables")]
    [SerializeField] protected int damageIncreasePercentage;
    [SerializeField] protected int damageReductionPercentage;
    
    public override void TickEffect()
    {
        throw new System.NotImplementedException();
    }

    public override void EntryEffect()
    {
        // Buff target stats
        Debug.Log("Begin rage on ares");
    }

    public override void ExitEffect()
    {
        // Reduce target stats to normal
        Debug.Log("Removed rage on ares");
    }
}