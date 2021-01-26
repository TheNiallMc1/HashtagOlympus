using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEngine;

public abstract class SpecialAbility : ScriptableObject
{
    
    public string abilityName;
    public string abilityDescription;

    
    public enum eSelectionType { Single, CircleAoE, ConeAoE, Self }
    
    [Header("Ability Effects")]
    [SerializeField]
    public eSelectionType selectionType;

    public List<Combatant.eTargetType> abilityCanHit;
    
    
    public int abilityDamage = 50;
    public int abilityHealAmount = 50;
    public List<StatusEffect> statusEffects;
    
    [Header("Cooldown")]
    public int abilityCooldown;
    public int remainingCooldownTime;

    [Header("Circle AoE")]
    public float radius;

    [Header("Cone AoE")]
    public ConeAoE coneAoE;
    public bool coneAlreadyExists;
    public float coneBuffer;
    
    
    [HideInInspector] public List<Combatant> targets;
    [HideInInspector] public GodBehaviour thisGod;

    public abstract void StartAbility();
    public abstract void ExecuteAbility();
    public abstract void DealDamage(Combatant target);
    public abstract void RestoreHealth(Combatant target);
    public abstract void InflictStatusEffects(Combatant target);
    public abstract void EndAbility();

}
