using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    protected PlayerControls playerControls;
    public bool leftClick;
    public bool rightClick;
    private Vector2 mousePosition;

    public GodBehaviour godBehaviour;
    public Camera mainCam;
    public SpecialAbility currentAbility;

    public List<Combatant> targets = new List<Combatant>();

    public bool targetSelectModeActive = false;

    public List<SpecialAbility> specialAbilities;
    

    private void Awake()
    {
        godBehaviour = GetComponent<GodBehaviour>();
        mainCam = Camera.main;

        playerControls = new PlayerControls();
        playerControls.Enable();

        playerControls.Mouse.LeftClick.started += ctx => leftClick = true;
        playerControls.Mouse.RightClick.started += ctx => rightClick = true;

        playerControls.Mouse.MousePos.performed += ctx => mousePosition = ctx.ReadValue<Vector2>();

        playerControls.Mouse.LeftClick.canceled += ctx => leftClick = false;
        playerControls.Mouse.RightClick.canceled += ctx => rightClick = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (targetSelectModeActive)
        {
            switch (currentAbility.selectionType)
            {
                case SpecialAbility.eSelectionType.Single:
                    SingleTargetSelect();
                    break;

                case SpecialAbility.eSelectionType.CircleAoE:
                    AoECircleSelect();
                    break;

                case SpecialAbility.eSelectionType.ConeAoE:
                    AoEConeSelect();
                    break;

                case SpecialAbility.eSelectionType.Self:
                    SelfSelect();
                    break;
            }
        }
    }


    public void InitiateAbility(int abilityIndex)
    {
        currentAbility = specialAbilities[abilityIndex];

        if(currentAbility != null)
        {
            targetSelectModeActive = true;
        }
    }
    

    private void ExecuteAbility()
    {
        foreach(Combatant target in targets)
        {
            target.TakeDamage(currentAbility.abilityDamage);
        }
        targets.Clear();

    }
    

    void SingleTargetSelect()
    {

        Combatant currentTarget;

        Ray ray = mainCam.ScreenPointToRay(playerControls.Mouse.MousePos.ReadValue<Vector2>());
        RaycastHit hit;

        if (rightClick)
        {
            if (Physics.Raycast(ray, out hit, 100))
            {
                currentTarget = hit.transform.gameObject.GetComponent<Combatant>();

                if (currentTarget != null && currentAbility.abilityCanHit.Contains(currentTarget.targetType))
                {
                    targetSelectModeActive = false;

                    targets.Add(currentTarget);
                    ExecuteAbility();
                }
            }
        }
        
    }


    private void AoECircleSelect()
    {
        throw new NotImplementedException();
    }

    private void AoEConeSelect()
    {
        throw new NotImplementedException();
    }

    private void SelfSelect()
    {
        throw new NotImplementedException();
    }

}
