using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class God_Ares : GodBehaviour
{
    [Header("Ares")]
    private StatusEffect lastActivatedRageType; // Stores the last rage type Ares used - maximum or normal

    [SerializeField] private StatusEffect rageStatus;
    [SerializeField] private StatusEffect maxRageStatus;

    [SerializeField] private float rageReductionRate;
    
    private Coroutine ultimateCoroutine;

    private GodSpecialBar rageBar;

    public GameObject rageParticles;

    [Header("Ares TESTING")] 
    public TextMeshProUGUI ultimateCountText;
    public TextMeshProUGUI rageCountText;
    

    public override void Start()
    {
        base.Start();
        
        // Set rage bar equal to the relevant special bar

        ultimateCharge = 0;
    }

    public override void OnDamageEvent(int damageTaken)
    {
        RageUpdate(damageTaken);
    }


    private void RageUpdate(int amountToAdd)
    {
        ultimateCharge += amountToAdd;

       // rageCountText.text = ultimateCharge.ToString();
        // For Ares, rage and ultimate charge are the same thing
    }

    public override void ActivateUltimate()
    {
        if (ultimateCharge > 0 && ultimateCharge < 100 && !usingUltimate) // If charged but not full
        {
            thisCombatant.ApplyStatus(rageStatus);
            lastActivatedRageType = rageStatus;

            rageParticles.SetActive(true);
            
            ultimateCoroutine = StartCoroutine(UltimateDurationCoroutine());

            usingUltimate = true;
        }
        
        if (ultimateCharge >= 100 && !usingUltimate)
        {
            thisCombatant.ApplyStatus(maxRageStatus);
            lastActivatedRageType = maxRageStatus;
            
            ultimateCoroutine = StartCoroutine(UltimateDurationCoroutine());
            
            usingUltimate = true;
        }
    }

    public void EndUltimate()
    {
        ultimateCoroutine = null;
        usingUltimate = false;
        rageParticles.SetActive(false);
    }

    public override IEnumerator UltimateDurationCoroutine()
    {
        // Every second, reduce Rage count by 1
        yield return new WaitForSecondsRealtime(rageReductionRate);
        RageUpdate(-1);

        // When rage hits zero, end the Ultimate
        if (ultimateCharge <= 0)
        {
            ultimateCharge = 0; // Just adjusting in case it falls below zero somehow
            
            thisCombatant.RemoveStatus(lastActivatedRageType);
            
            EndUltimate();
        }
        else
        {
            ultimateCoroutine = StartCoroutine(UltimateDurationCoroutine());
        }
    }
}
