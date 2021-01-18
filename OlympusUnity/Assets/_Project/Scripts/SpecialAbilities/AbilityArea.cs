using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/AoEAbility", order = 1)]
public class AbilityArea : SpecialAbility
{
    protected override void EnterTargetSelectionMode()
    {
        // overlap sphere detects targets within radius
    }

    public override void ExecuteAbility()
    {
        // AoE stuff goes here
    }
}
