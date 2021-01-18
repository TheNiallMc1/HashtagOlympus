using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class God_Ares : GodBehaviour
{
    [SerializeField] private int currentRageCount;
    private int maxRageCount;

    private GodSpecialBar rageBar;

    public override void Start()
    {
        base.Start();

        // Set rage bar equal to the relevant special bar
        //rageBar = uiManager.specialBars[indexInGodList];
        //rageBar.Initialise(0);
    }
    
    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);
        RageUpdate(damageAmount);
    }

    private void RageUpdate(int damageAmount)
    {
        currentRageCount += damageAmount;
    }
}
