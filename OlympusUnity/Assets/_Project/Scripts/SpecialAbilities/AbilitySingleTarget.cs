using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/SingleTargetAbility", order = 1)]
public class AbilitySingleTarget : SpecialAbility
{
    private bool healingSkill;
    // possible targets

    protected override void EnterTargetSelectionMode()
    {
        // can click on model in world to target
        Debug.Log("target selection mode");
    }

    public override void ExecuteAbility()
    {
        // Single target stuff
        // target = listoftargets[0]
        Debug.Log("execute ability");
        EnterTargetSelectionMode();
    }
}
