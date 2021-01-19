using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SpecialAbility : ScriptableObject
{
    
    public string abilityName;
    public string abilityDescription;

    public List<Combatant.eTargetType> abilityCanHit;
    
    public enum eSelectionType
    {
        Single,
        CircleAoE,
        ConeAoE,
        Self
    }

    [SerializeField] private eSelectionType _selectionType;
    public eSelectionType selectionType { get { return _selectionType; } set { _selectionType = value; } }

    public int abilityDamage = 50;
    public int abilityCooldown;
    [SerializeField] protected int abilityRange;



    // Status Effects



    // Target Selection

    //public void ExecuteAbility()
    //{

    //}

}
