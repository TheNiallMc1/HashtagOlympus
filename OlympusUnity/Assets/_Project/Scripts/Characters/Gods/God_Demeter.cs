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

        summerAbilities[0].abilityStateName = "Demeter_S_Ability01";
        summerAbilities[1].abilityStateName = "Demeter_S_Ability02";

        winterAbilities[0].abilityStateName = "Demeter_W_Ability01";
        winterAbilities[1].abilityStateName = "Demeter_W_Ability02";
        winterAbilities[1].channelAnimTrigger = "Ability02_End";
        
        ultimateStartAnimTrigger = "UltimateActivate";
        ultimateFinishAnimTrigger = "UltimateFinish";
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
            ability.anim = animator;
        }
        
        foreach (AbilityManager ability in summerAbilities)
        {
            ability.enabled = false;
        }

        specialAbilities[0] = winterAbilities[0];
        specialAbilities[1] = winterAbilities[1];
        
        winterPassive.enabled = true;
        winterPassive.Initialise();
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
            ability.anim = animator;
        }
        
        specialAbilities[0] = summerAbilities[0];
        specialAbilities[1] = summerAbilities[1];

        specialAbilities[0].cooldownText.text = specialAbilities[0].ability.abilityName;
        
        winterPassive.enabled = false;
    }
    
    public override void ActivateUltimate()
    {
        if ( !CanUseAbility() )
        {
            return;
        }

        if (ultimateCharge >= 100)
        {
            ultimateCharge = 100; // Set to 100 in case it somehow went over

            currentState = GodState.usingUltimate;
            
            attackAnimationIsPlaying = false;
            animator.SetTrigger(ultimateStartAnimTrigger);
            
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