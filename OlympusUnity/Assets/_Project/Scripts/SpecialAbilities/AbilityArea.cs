using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/AoEAbility", order = 1)]
public class AbilityArea : SpecialAbility
{
    [SerializeField] protected List<GameObject> listOfTargets;

    //protected override void Update()
    //{

    //}

    //public override void InitiateAbility()
    //{
    //    // Debug.Log("ability executed");
    //    EnterTargetSelectionMode();

    //}

    //protected override void EnterTargetSelectionMode()
    //{
    //    // overlap sphere detects targets within radius
    //    targetSelectModeActive = true;
    //}
    

    //protected override void ExecuteAbility()
    //{
    //    throw new System.NotImplementedException();
    //}
}
