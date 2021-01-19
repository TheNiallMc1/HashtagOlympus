using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class God_Ares : GodBehaviour
{
    [Header("Ares")]
    [SerializeField] private int currentRageCount;
    private int maxRageCount;

    private StatusEffect lastActivatedRageType; // Stores the last rage type Ares used - maximum or normal

    [SerializeField] private StatusEffect rageStatus;
    [SerializeField] private StatusEffect maxRageStatus;

    [SerializeField] private float rageReductionRate;
    
    private Coroutine ultimateCoroutine;

    private GodSpecialBar rageBar;

    [Header("Ares TESTING")] 
    public TextMeshProUGUI ultimateCountText;
    public TextMeshProUGUI rageCountText;
    

    public override void Start()
    {
        base.Start();
        
        // Set rage bar equal to the relevant special bar

        currentRageCount = 0;
        maxRageCount = 100;
    }
    
    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);

        if (!usingUltimate)
        {
            RageUpdate(damageAmount);
        }
    }

    private void RageUpdate(int amountToAdd)
    {
        currentRageCount += amountToAdd;
        ultimateCharge = currentRageCount;

        ultimateCountText.text = ultimateCharge.ToString();
        rageCountText.text = currentRageCount.ToString();
        // For Ares, rage and ultimate charge are the same thing
    }

    public override void ActivateUltimate()
    {
        if (ultimateCharge > 0 && ultimateCharge < 100) // If charged but not full
        {
            thisCombatant.ApplyStatus(rageStatus);
            lastActivatedRageType = rageStatus;

            ultimateCoroutine = StartCoroutine(UltimateDurationCoroutine());

            usingUltimate = true;
        }
        
        if (ultimateCharge >= 100)
        {
            thisCombatant.ApplyStatus(maxRageStatus);
            lastActivatedRageType = maxRageStatus;
            
            ultimateCoroutine = StartCoroutine(UltimateDurationCoroutine());
            
            usingUltimate = true;
        }
    }

    public override IEnumerator UltimateDurationCoroutine()
    {
        // Every second, reduce Rage count by 1
        yield return new WaitForSecondsRealtime(rageReductionRate);
        RageUpdate(-1);

        // When rage hits zero, end the Ultimate
        if (currentRageCount <= 0)
        {
            currentRageCount = 0; // Just adjusting in case it falls below zero somehow
            
            thisCombatant.RemoveStatus(lastActivatedRageType);

            usingUltimate = false;    
            ultimateCoroutine = null;
        }
        else
        {
            ultimateCoroutine = StartCoroutine(UltimateDurationCoroutine());
        }
    }
}
