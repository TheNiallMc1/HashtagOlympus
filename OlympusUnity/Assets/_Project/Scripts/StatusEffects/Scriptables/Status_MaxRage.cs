﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used for basic healing. Heals a character once, immediately, and then is removed
[CreateAssetMenu(fileName = "MaxRageStatus", menuName = "Status Effect/Maximum Rage", order = 1)]
public class Status_MaxRage : StatusEffect
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
        Debug.Log("Begin MAX rage on ares");
    }

    public override void ExitEffect()
    {
        // Reduce target stats to normal
        Debug.Log("Removed MAX rage on ares");
    }
}