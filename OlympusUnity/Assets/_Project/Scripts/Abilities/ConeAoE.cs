using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml;
using UnityEngine;

public class ConeAoE : MonoBehaviour
{
    public List<Combatant> targetsInCone = new List<Combatant>();

    public List<Combatant.eTargetType> targetTypes;
    public float lifeTime = 10;
    public AbilityManager ability;

    private void Start()
    {
        // transform.rotation = Quaternion.Euler(90f, 0, 0);
        StartCoroutine(DestroyConeRoutine());
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("<color=blue> ConeAOE: Trigger entered by " + other.name + " </color>");
        
        Combatant combatant = other.gameObject.GetComponentInParent<Combatant>();
        Debug.Log("<color=blue> ConeAOE: Found combatant: " + combatant.name + " </color>");
        
        if (IsTargetValid(combatant))
        {
            targetsInCone.Add(combatant);
            Debug.Log("<color=green> ConeAOE: Added combatant to target list </color>");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Combatant combatant = other.gameObject.GetComponentInParent<Combatant>();
        Debug.Log("<color=red> ConeAOE: Trigger exited by " + other.name + " </color>");   
    }

    private bool IsTargetValid(Combatant currentTarget)
    {
        bool alreadyInList = targetsInCone.Contains(currentTarget);
        bool correctTargetType = targetTypes.Contains(currentTarget.targetType);

        return !alreadyInList && correctTargetType;
    }


    public void DestroyImmediate()
    {
        Destroy(gameObject);
    }

    private IEnumerator DestroyConeRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(lifeTime);

        ability.EndChannel();
    }
}