using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Combatant))]
public class PassiveAbilityManager : MonoBehaviour
{
    public PassiveAbility ability;
    private Coroutine tickCoroutine;

    private List<Combatant> targets = new List<Combatant>();

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

    void FindTargets()
    {
        Debug.Log("FINDING TARGETS");
        Vector3 centre = ability.thisCombatant.colliderHolder.transform.position;
        
        Collider[] colliders = Physics.OverlapSphere(centre, ability.effectRadius);
        print(colliders[0].gameObject.name);
        
        foreach (Collider targetCollider in colliders)
        {
            Combatant currentTarget = targetCollider.gameObject.GetComponent<Combatant>();

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
            Debug.Log("target is invalid");
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
        Gizmos.color = new Color(0.23f, 0.57f, 1f);
        Gizmos.DrawWireSphere(transform.position, ability.effectRadius);
    }
}
