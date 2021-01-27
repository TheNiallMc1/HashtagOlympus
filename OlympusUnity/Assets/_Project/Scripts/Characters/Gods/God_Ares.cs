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
        if (!usingUltimate)
        {
            ultimateCharge += amountToAdd;
            ultimateCharge = Mathf.Min(ultimateCharge, 100);
        }
    }

    public override void ActivateUltimate()
    {
        if (ultimateCharge > 0 && ultimateCharge < 100 && !usingUltimate) // If charged but not full
        {
            thisCombatant.ApplyStatus(rageStatus);
            lastActivatedRageType = rageStatus;

            //rageParticles.SetActive(true);
            
            ultimateDecreaseCoroutine = StartCoroutine(UltimateDurationCoroutine());

            usingUltimate = true;
        }
        
        if (ultimateCharge >= 100 && !usingUltimate)
        {
            thisCombatant.ApplyStatus(maxRageStatus);
            lastActivatedRageType = maxRageStatus;
            
            ultimateDecreaseCoroutine = StartCoroutine(UltimateDurationCoroutine());
            
            usingUltimate = true;
        }
    }

    public override void UltimateExitEffects()
    {
        thisCombatant.RemoveStatus(lastActivatedRageType);
    }
}
