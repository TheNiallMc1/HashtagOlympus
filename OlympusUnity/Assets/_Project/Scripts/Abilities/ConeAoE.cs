using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeAoE : MonoBehaviour
{
    private List<Combatant> combatantsInCone = new List<Combatant>();
    public List<Combatant> targetsInCone = new List<Combatant>();

    public List<Combatant.eTargetType> targetTypes;
    public float lifeTime = 10;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(90f, 0, 0);
        StartCoroutine(GetTargetsRoutine());
        StartCoroutine(DestroyCone());
    }

    public void OnTriggerEnter(Collider other)
    {
        Combatant combatant = other.gameObject.GetComponent<Combatant>();

        if (combatant != null)
        {
            combatantsInCone.Add(combatant);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Combatant combatant = other.gameObject.GetComponent<Combatant>();

        if (combatant != null && targetsInCone.Contains(combatant))
        {
            combatantsInCone.Remove(combatant);
        }
    }


    public void IncludeType(Combatant.eTargetType tType)
    {
        foreach (Combatant combatant in combatantsInCone)
        {
            if (combatant.targetType == tType)
            {
                targetsInCone.Add(combatant);
            }
        }
    }


    private List<Combatant> GetTargets()
    {
        foreach (Combatant.eTargetType type in targetTypes)
        {
            IncludeType(type);
        }

        return targetsInCone;
    }


    private IEnumerator GetTargetsRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        GetTargets();
        yield return null;
    }

    private IEnumerator DestroyCone()
    {
        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}