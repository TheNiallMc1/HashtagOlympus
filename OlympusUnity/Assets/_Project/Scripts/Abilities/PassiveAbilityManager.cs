using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Combatant))]
[RequireComponent(typeof(GodBehaviour))]
public class PassiveAbilityManager : MonoBehaviour
{
    [Header("Editor Gizmos")] 
    public bool displayRadius;
    public Color radiusColour;
    
    [Header("Ability Info")]
    public PassiveAbility ability;
    
    private List<Combatant> targets = new List<Combatant>();
    
    [Header("Status Effect Info")]
    public List<Combatant> targetsInflictedWithStatus;

    private void Awake()
    {
        ability = Instantiate(ability);
    }

    private void Start()
    {
        ability.thisCombatant = GetComponent<Combatant>();
        ability.thisGod = GetComponent<GodBehaviour>();
    }

    public void Initialise()
    {
        StartCoroutine(TickEffectCoroutine());
    }

    public void RemovePassiveAbility()
    {
        StopAllCoroutines();
        this.enabled = false;
    }

    private void FindTargets()
    {
        Vector3 centre = ability.thisCombatant.colliderHolder.transform.position;
        
        
        Collider[] colliders = Physics.OverlapSphere(centre, ability.effectRadius, ability.abilityLayerMask);

        int i = 0;
        foreach (Collider targetCollider in colliders)
        {
            Combatant currentTarget = targetCollider.gameObject.GetComponentInParent<Combatant>();

            if (isTargetValid(currentTarget))
            {
                targets.Add(currentTarget);
                ability.targets = targets;
            }
        }
    }
    
    public bool isTargetValid(Combatant currentTarget)
    {
        if(currentTarget != null)
        {
            bool isInList = targets.Contains(currentTarget);
            bool canBeHit = ability.abilityCanHit.Contains(currentTarget.targetType);

            return !isInList && canBeHit;
        }
        else
        {
            return false;
        }
    }
    
    // Every x seconds, generate targets and activate the ability effect 
    // ReSharper disable once FunctionRecursiveOnAllPaths
    public IEnumerator TickEffectCoroutine()
    {
        yield return new WaitForSecondsRealtime(ability.tickInterval);
        FindTargets();
        ability.AbilityEffect();
        StartCoroutine(TickEffectCoroutine());
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = radiusColour;
        
        float radius = ability.effectRadius;
        
        if (displayRadius)
        {
            Gizmos.DrawWireSphere(gameObject.transform.position, radius);
        }
        
    }
}
