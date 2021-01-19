using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/SingleTargetAbility", order = 1)]
public class AbilitySingleTarget : SpecialAbility
{
    private bool healingSkill;
    // possible targets
    Combatant currentTarget;
    Camera mainCam;

    //protected override void Update()
    //{
    //    Debug.Log("Update running");

    //    if (targetSelectModeActive && rightClick)
    //    {
    //        Debug.Log("AAAAAA");
    //        Ray ray = mainCam.ScreenPointToRay(playerControls.Mouse.MousePos.ReadValue<Vector2>());
    //        RaycastHit hit;

    //        if (Physics.Raycast(ray, out hit, 100))
    //        {
    //            currentTarget = hit.transform.gameObject.GetComponent<Combatant>();
    //            if (currentTarget != null && abilityCanHit.Contains(currentTarget.targetType))
    //            {

    //                targetSelectModeActive = false;
                    
    //                ExecuteAbility();

    //                //foreach (Combatant.eTargetType eTargetType in abilityCanHit)
    //                //{
    //                //    if (currentTarget.targetType == eTargetType)
    //                //    {
    //                //        Debug.Log("Attacked " + hit.transform.gameObject.name);
    //                //        currentTarget.TakeDamage(abilityDamage);
    //                //    }
    //                //}
    //            }
    //        }
    //    }
    //}

    //public override void InitiateAbility()
    //{
    //    // Single target stuff
    //    // target = listoftargets[0]
    //    Debug.Log("execute ability");
    //    EnterTargetSelectionMode();
    //}

    //protected override void EnterTargetSelectionMode()
    //{
    //    // can click on model in world to target
    //    mainCam = Camera.main;
    //    Debug.Log("target selection mode");
    //    targetSelectModeActive = true;
    //}

    //protected override void ExecuteAbility()
    //{
    //    // Debug.Log("Attacked " + hit.transform.gameObject.name);
    //    currentTarget.TakeDamage(abilityDamage);
    //}
}