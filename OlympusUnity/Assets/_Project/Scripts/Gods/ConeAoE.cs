using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeAoE : MonoBehaviour
{
    List<Combatant> combatantsInCone = new List<Combatant>();
    List<Combatant> targets = new List<Combatant>();

    private DanielTestingKeys testKeys;
    private bool key1;
    private bool key2;
    private bool key3;

    private void Awake()
    {
        testKeys = new DanielTestingKeys();
        testKeys.Enable();

        testKeys.TestKeys.TestKey1.started += ctx => key1 = true;
        testKeys.TestKeys.TestKey2.started += ctx => key2 = true;
        testKeys.TestKeys.TestKey3.started += ctx => key3 = true;

        testKeys.TestKeys.TestKey1.canceled -= ctx => key1 = false;
        testKeys.TestKeys.TestKey2.canceled -= ctx => key2 = false;
        testKeys.TestKeys.TestKey3.canceled -= ctx => key3 = false;
    }

    // Start is called before the first frame update 
    void Start()
    {
        transform.rotation = Quaternion.Euler(90f, 0, 0);
    }

    // Update is called once per frame 
    void Update()
    {
        if (key1)
        {
            IncludeType(Combatant.TargetType.Enemy);
            IncludeType(Combatant.TargetType.EMonument);
            GetTargets();
            key1 = false;
        }

        if (key2)
        {

            key2 = false;
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        // List<Combatant> targets = new List<Combatant>(); 

        Combatant combatant = other.gameObject.GetComponent<Combatant>();

        if (combatant != null)
        {
            combatantsInCone.Add(combatant);
        }

        // return targets; 
    }

    public void IncludeType(Combatant.TargetType tType)
    {
        foreach (Combatant combatant in combatantsInCone)
        {
            if (combatant.targetType == tType)
            {
                targets.Add(combatant);
            }
        }

    }

    public List<Combatant> GetTargets()
    {
        foreach (Combatant combatant in targets)
        {
            print(combatant.gameObject.name);
        }

        return targets;
    }


}