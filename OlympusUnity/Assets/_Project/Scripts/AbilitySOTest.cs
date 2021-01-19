using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class. Each ability in the game is a subclass of this
public abstract class AbilitySOTest : ScriptableObject
{
    public float abilityCooldown; // Cooldown in base class cuz everything has it
    public List<StatusEffect> statusEffects;
    
    public Combatant thisGod; // The god this ability is attached to

    // Method for each step of the ability, fill these with the appropriate logic in the subclasses. Don't need to use all of them
    public abstract void InitiateAbility();
    public abstract void DealDamage();
    public abstract void InflictStatusEffects();
    public abstract void BeginCooldown();
}