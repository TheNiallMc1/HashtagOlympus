using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveAbility : ScriptableObject
{
    public string abilityName;
    public string abilityDescription;

    [Header("Ability Effects")]
    public List<Combatant.eTargetType> abilityCanHit;
    public LayerMask abilityLayerMask;
    public float effectRadius;
    public float tickInterval;
    public StatusEffect statusEffect;
    [HideInInspector] public List<Combatant> targetsAffectedByStatus = new List<Combatant>();

    [HideInInspector] public Combatant thisCombatant;
    [HideInInspector] public List<Combatant> targets;
    [HideInInspector] public GodBehaviour thisGod;

    public abstract void AbilityEffect();
}