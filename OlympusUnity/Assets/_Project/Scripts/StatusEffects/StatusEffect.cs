using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : ScriptableObject
{
    protected int statusDuration;
    
    public Combatant inflictedBy; // The combatant who applied this effect    
    public Combatant affectedCombatant; // The combatant this effect is applied to
    
    protected abstract void TickEffect(); // Apples once every x seconds as determined
    protected abstract void PersistentEffect(); // Applies constantly
    protected abstract void EntryEffect(); // Applies when the effect is applied
    protected abstract void ExitEffect(); // Applies when the effect is removed
}