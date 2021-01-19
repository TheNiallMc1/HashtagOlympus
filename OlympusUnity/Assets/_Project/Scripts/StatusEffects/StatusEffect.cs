using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : ScriptableObject
{
    [Header("Duration")]
    [SerializeField] [Tooltip("Set to zero to make an instant cast status, like Heal")]
    public float statusDuration;
    public bool isInfinite;

    [Header("Tick Behaviour")] 
    public bool isTickType;
    public float tickInterval;

    
    [HideInInspector] public Combatant inflictedBy; // The combatant who applied this effect    
    [HideInInspector] public Combatant affectedCombatant; // The combatant this effect is applied to
    
    public abstract void TickEffect(); // Apples once every x seconds as determined
    public abstract void EntryEffect(); // Applies when the effect is applied
    public abstract void ExitEffect(); // Applies when the effect is removed
}