using System.Collections;
using UnityEngine;

public class God_Ares : GodBehaviour
{
    [Header("Ares")]
    [SerializeField] private StatusEffect rageStatus;
    [SerializeField] private StatusEffect maxRageStatus;
    
    [SerializeField] private float rageReductionRate;
    
    private StatusEffect lastActivatedRageType; // Stores the last rage type Ares used - maximum or normal

    public GameObject rageParticles;
    
    private Coroutine ultimateCoroutine;
    
    public override void Start()
    {
        base.Start();
        ultimateCharge = 0;
    }

    public override void OnDamageEvent(int damageTaken)
    {
        RageUpdate(damageTaken);
    }


    private void RageUpdate(int amountToAdd)
    {
        ultimateCharge += amountToAdd;
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

    private void EndUltimate()
    {
        ultimateCoroutine = null;
        usingUltimate = false;
        rageParticles.SetActive(false);
    }

    public override IEnumerator UltimateDurationCoroutine()
    {
        // Reduce ultimate charge by 1 on interval
        yield return new WaitForSecondsRealtime(rageReductionRate);
        RageUpdate(-1);

        // When rage/ultimate charge hits zero, end the Ultimate and remove the rage status
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
