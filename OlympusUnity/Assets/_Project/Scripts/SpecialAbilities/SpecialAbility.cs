using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SpecialAbility : ScriptableObject
{
    public string abilityName;
    public string abilityDescription;

    [SerializeField] protected int abilityDamage;
    [SerializeField] protected int abilityCooldown;
    [SerializeField] protected int abilityRange;

    [SerializeField] protected List<GameObject> listOfTargets;
    // Status Effects

    
    
    // Target Selection
    protected abstract void EnterTargetSelectionMode();
    
    public abstract void ExecuteAbility();
}
