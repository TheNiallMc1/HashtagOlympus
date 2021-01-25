using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God_Demeter : GodBehaviour
{
    [Header("Demeter General")] 
    public DemeterForm currentForm; 
    public float ultimateReductionRate;
    
    private Coroutine ultimateCoroutine;

    [Header("Summer Demeter")] 
    public GameObject summerModel;
    public List<AbilityManager> summerAbilities;

    [Header("Winter Demeter")] 
    public GameObject winterModel;
    public List<AbilityManager> winterAbilities;
    public AbilityManager winterPassive;
        
    public void AddUltimateCharge(int chargeToAdd)
    {
        ultimateCharge += chargeToAdd;

        if (ultimateCharge > 100)
        {
            ultimateCharge = 100;
        }
    }
    
    void SwitchForms()
    {
        switch (currentForm)
        {
            case DemeterForm.Summer:
                SwitchToWinter();
                break;
            
            case DemeterForm.Winter:
                SwitchToSummer();
                break;
        }
    }

    void SwitchToWinter()
    {
        currentForm = DemeterForm.Winter;
            
        summerModel.SetActive(false);
        winterModel.SetActive(true);

        animator = winterModel.GetComponent<Animator>();

        // Disable summer abilities and activate winter abilities
        foreach (AbilityManager ability in winterAbilities)
        {
            ability.enabled = true;
        }
        
        foreach (AbilityManager ability in summerAbilities)
        {
            ability.enabled = false;
        }
        
        winterPassive.enabled = true;
    }

    void SwitchToSummer()
    {
        currentForm = DemeterForm.Summer;
            
        summerModel.SetActive(true);
        winterModel.SetActive(false);

        animator = summerModel.GetComponent<Animator>();
        
        // Disable winter abilities and activate summer abilities
        foreach (AbilityManager ability in winterAbilities)
        {
            ability.enabled = false;
        }
        
        foreach (AbilityManager ability in summerAbilities)
        {
            ability.enabled = true;
        }
        
        winterPassive.enabled = false;
    }
    
    public override void ActivateUltimate()
    {
        if (ultimateCharge >= 100 && !usingUltimate)
        {
            ultimateCharge = 100; // Set to 100 in case it somehow went over
            usingUltimate = true;

            attackingLocked = true;
            
            SwitchForms();
            
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

public enum DemeterForm
{
    Summer,
    Winter
}