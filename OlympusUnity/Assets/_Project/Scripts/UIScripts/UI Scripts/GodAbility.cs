using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodAbility 
{
    public String abilityName;
    public enum AbilityType
    {
        Ability1,
        Ability2,
        UltimateAbility
    };
    
    public AbilityType currentAbilityType;
    public bool isUnlocked;

    public enum AbilityEffectType
    {
        AoE,
        Ranged,
        Melee,
        Special

    };

    public AbilityEffectType currentEffectType;

    public String abilityDescription;

}
