using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SpecialAbility : ScriptableObject
{
    
    public string abilityName;
    public string abilityDescription;

    public List<StatusEffect> statusEffects;
    public List<Combatant> targets;

    public Combatant thisGod; // The god this ability is attached to

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
    public int abilityHealAmount = 50;

    public int abilityCooldown;
    public int remainingCooldownTime;

    [SerializeField] protected int abilityRange;

    [Header("Circle AoE")]
    public Vector3 centre;
    public float radius;

    [Header("Cone AoE")]
    public ConeAoE coneAoE;
    public bool coneAlreadyExists;
    public float coneBuffer;


    public abstract void InitiateAbility();
    public abstract void ExecuteAbility();
    public abstract void DealDamage(Combatant target);
    public abstract void RestoreHealth(Combatant target);
    public abstract void InflictStatusEffects(Combatant target);
    public abstract void BeginCooldown();


}
