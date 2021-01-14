using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeAoE : MonoBehaviour
{
    //List<Combatant> combatantsInCone = new List<Combatant>();
    //List<Combatant> targets = new List<Combatant>();

    HashSet<Combatant> combatantsInCone = new HashSet<Combatant>();
    HashSet<Combatant> targets = new HashSet<Combatant>();

    public Combatant.TargetType[] targetTypes;

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
        // transform.rotation = Quaternion.Euler(90f, 0, 0);
        StartCoroutine(GetTargetsRoutine());
    }

    // Update is called once per frame 
    void Update()
    {
        if (key1)
        {
            GetAllTargets();

            key1 = false;
        }

        if (key2)
        {
            foreach (Combatant combatant in combatantsInCone)
            {
                print(combatant.gameObject.name);
            }
            key2 = false;
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        // List<Combatant> targets = new List<Combatant>(); 

        Combatant combatant = other.gameObject.GetComponent<Combatant>();

        if (combatant != null)
        {
            //combatantsInCone.Add(combatant);
            combatantsInCone.Add(combatant);
        }

        // return targets; 
    }

    private void OnTriggerExit(Collider other)
    {
        Combatant combatant = other.gameObject.GetComponent<Combatant>();

        // print(targets.)

        if (combatant != null && targets.Contains(combatant))
        {
            // targets.Remove(combatant);
            targets.Remove(combatant);
            combatantsInCone.Remove(combatant);
        }
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

    public HashSet<Combatant> GetTargets()
    {
        foreach (Combatant combatant in targets)
        {
            print(combatant.gameObject.name);
        }

        return targets;
    }


    public void GetAllTargets()
    {
        foreach(Combatant.TargetType type in targetTypes)
        {
            IncludeType(type);
        }

        GetTargets();
    }

    IEnumerator GetTargetsRoutine()
    {
        yield return null;
        GetAllTargets();
    }
}