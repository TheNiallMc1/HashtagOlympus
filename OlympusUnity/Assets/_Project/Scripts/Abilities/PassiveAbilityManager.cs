using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Combatant))]
[RequireComponent(typeof(GodBehaviour))]
public class PassiveAbilityManager : MonoBehaviour
{
    [Header("Editor Gizmos")] 
    public bool displayRadius;
    public Color radiusColour;
    
    [Header("Ability Info")]
    public PassiveAbility ability;
    private Coroutine tickCoroutine;
    
    private List<Combatant> targets = new List<Combatant>();
    
    [Header("Status Effect Info")]
    public List<Combatant> targetsInflictedWithStatus;

    private void Awake()
    {
        ability = Instantiate(ability);
    }

    void Start()
    {
        ability.thisCombatant = GetComponent<Combatant>();
        ability.thisGod = GetComponent<GodBehaviour>();

        tickCoroutine = StartCoroutine(TickEffectCoroutine());
    }

    public void RemovePassiveAbility()
    {
        StopAllCoroutines();
        tickCoroutine = null;
        this.enabled = false;
    }

    void FindTargets()
    {
        Vector3 centre = ability.thisCombatant.colliderHolder.transform.position;
        
        Collider[] colliders = Physics.OverlapSphere(centre, ability.effectRadius);
        
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
    private IEnumerator TickEffectCoroutine()
    {
        yield return new WaitForSecondsRealtime(ability.tickInterval);
        FindTargets();
        ability.AbilityEffect();
        tickCoroutine = StartCoroutine(TickEffectCoroutine());
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
