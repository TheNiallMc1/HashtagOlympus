using System.Collections.Generic;
using UnityEngine;

public class CircleAoE : MonoBehaviour
{
    public float radius = 3f;
    List<Combatant> targets = new List<Combatant>();
    // HashSet<Combatant> targets = new HashSet<Combatant>();

    private DanielTestingKeys testKeys;
    private bool key1;
    private bool key2;
    private bool key3;

    public Combatant.eTargetType[] onlyEnemies = new Combatant.eTargetType[] { Combatant.eTargetType.Enemy };
    public Combatant.eTargetType[] enemiesAndEMonuments = new Combatant.eTargetType[] { Combatant.eTargetType.Enemy, Combatant.eTargetType.EMonument };
    public Combatant.eTargetType[] onlyGods = new Combatant.eTargetType[] { Combatant.eTargetType.Player };


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
        //onlyEnemies[0] = Combatant.TargetType.Enemy; 

        //enemiesAndEMonuments[0] = Combatant.TargetType.Enemy; 
        //enemiesAndEMonuments[1] = Combatant.TargetType.EMonument; 

        //onlyGods[0] = Combatant.TargetType.Player; 
    }

    // Update is called once per frame 
    void Update()
    {
        if (key1)
        {
            key1 = false;
            targets.Clear();
            GenerateCircleAoE(transform.position, radius, Combatant.eTargetType.Enemy);
            GetTargets();
        }

        if (key2)
        {
            key2 = false;
            targets.Clear();
            GenerateCircleAoE(transform.position, radius, Combatant.eTargetType.EMonument);
            GetTargets();
        }

        if (key3)
        {
            key3 = false;
            targets.Clear();
            GenerateCircleAoE(transform.position, radius, Combatant.eTargetType.Player);
            GetTargets();
        }
    }


    public void GenerateCircleAoE(Vector3 centre, float radius, Combatant.eTargetType targetType)
    {
        Collider[] colliders = Physics.OverlapSphere(centre, radius);

        foreach (Collider nearbyObject in colliders)
        {
            Combatant combatant = nearbyObject.gameObject.GetComponent<Combatant>();

            if (combatant != null && combatant.targetType == targetType)
            {
                targets.Add(nearbyObject.gameObject.GetComponent<Combatant>());
                print(nearbyObject.gameObject.name); 
            }
        }

    }

    // This might be a bad idea and unneccesary so blame it on it being 6pm in that case 

    public List<Combatant> GetTargets()
    {
        return targets;
    }


    //public List<Combatant> GetAoETargets(Vector3 centre, float radius, Combatant.TargetType[] targetType) 
    //{ 
    //    List<Combatant> targets = new List<Combatant>(); 
    //    Collider[] colliders = Physics.OverlapSphere(centre, radius); 

    //    foreach(Combatant.TargetType tType in targetType) 
    //    { 
    //        foreach (Collider nearbyObject in colliders) 
    //        { 
    //            Combatant combatant = nearbyObject.gameObject.GetComponent<Combatant>(); 

    //            if (combatant != null && combatant.targetType == tType) 
    //            { 
    //                targets.Add(nearbyObject.gameObject.GetComponent<Combatant>()); 
    //                print(nearbyObject.gameObject.name); 
    //            } 
    //        } 
    //    } 
    //    return targets; 
    //} 

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}