using System.Collections;
using UnityEngine;

public class God_Dionysus : GodBehaviour
{
    [Header("Dionysus")] 
    public StatusEffect ultimatePartyStatus;
    public float ultimateReductionRate;
    
    private Coroutine ultimateCoroutine;
    
    // KEEP LIST OF EVERY ENEMY AFFECTED BY PARTY-TIME
    
    public void AddUltimateCharge(int chargeToAdd)
    {
        ultimateCharge += chargeToAdd;

        if (ultimateCharge > 100)
        {
            ultimateCharge = 100;
        }
    }
    
    public override void ActivateUltimate()
    {
        if (ultimateCharge >= 100 && !usingUltimate)
        {
            ultimateCharge = 100; // Set to 100 in case it somehow went over
            usingUltimate = true;

            attackingLocked = true;
            
            // ADD PASSIVE ABILITY TO INFLICTS PARTY-TIME ON NEARBY ENEMIES
            // CHANGE STATE TO "DANCING" SO HE CANNOT ATTACK
            
            ultimateCoroutine = StartCoroutine(UltimateDurationCoroutine());
        }
    }
    
    public override IEnumerator UltimateDurationCoroutine()
    {
        // Ultimate count goes down on a set time frame by 1
        yield return new WaitForSecondsRealtime(ultimateReductionRate);
        AddUltimateCharge(-1);
        
        // When ultimate hits zero, end the Ultimate
        if (ultimateCharge <= 0)
        {
            ultimateCharge = 0; // Just adjusting in case it falls below zero somehow

            usingUltimate = false;    
            attackingLocked = false;
            ultimateCoroutine = null;
        }
        else
        {
            ultimateCoroutine = StartCoroutine(UltimateDurationCoroutine());
        }
    }
}
