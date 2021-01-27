using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God_Demeter : GodBehaviour
{
    [Header("Demeter General")] 
    public DemeterForm currentForm; 

    [Header("Summer Demeter")] 
    public GameObject summerModel;
    public List<AbilityManager> summerAbilities;

    [Header("Winter Demeter")] 
    public GameObject winterModel;
    public List<AbilityManager> winterAbilities;
    public PassiveAbilityManager winterPassive;

    public override void Start()
    {
        base.Start();
        SwitchToSummer();
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

        animator = winterModel.GetComponentInChildren<Animator>();

        // Disable summer abilities and activate winter abilities
        foreach (AbilityManager ability in winterAbilities)
        {
            ability.enabled = true;
        }
        
        foreach (AbilityManager ability in summerAbilities)
        {
            ability.enabled = false;
        }

        specialAbilities[0] = winterAbilities[0];
        specialAbilities[1] = winterAbilities[1];
        
        winterPassive.enabled = true;
    }

    void SwitchToSummer()
    {
        currentForm = DemeterForm.Summer;
            
        summerModel.SetActive(true);
        winterModel.SetActive(false);

        animator = summerModel.GetComponentInChildren<Animator>();
        
        // Disable winter abilities and activate summer abilities
        foreach (AbilityManager ability in winterAbilities)
        {
            ability.enabled = false;
        }
        
        foreach (AbilityManager ability in summerAbilities)
        {
            ability.enabled = true;
        }
        
        specialAbilities[0] = summerAbilities[0];
        specialAbilities[1] = summerAbilities[1];
        
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
            
            ultimateDecreaseCoroutine = StartCoroutine(UltimateDurationCoroutine());
        }
    }
}

public enum DemeterForm
{
    Summer,
    Winter
}