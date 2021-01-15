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
    public float lifeTime = 10f;

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
        StartCoroutine(GetTargetsRoutine());
        StartCoroutine(DestroyCone());
    }

    // Update is called once per frame 
    void Update()
    {
        if (key1)
        {
            GetTargets();

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
        Combatant combatant = other.gameObject.GetComponent<Combatant>();

        if (combatant != null)
        {
            combatantsInCone.Add(combatant);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Combatant combatant = other.gameObject.GetComponent<Combatant>();

        if (combatant != null && targets.Contains(combatant))
        {
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

    public void PrintTargets()
    {
        foreach (Combatant combatant in targets)
        {
            print(combatant.gameObject.name);
        }
    }


    public HashSet<Combatant> GetTargets()
    {
        foreach(Combatant.TargetType type in targetTypes)
        {
            IncludeType(type);
        }

        PrintTargets();
        return targets;
    }





    IEnumerator GetTargetsRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        GetTargets();
    }

    IEnumerator DestroyCone()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}