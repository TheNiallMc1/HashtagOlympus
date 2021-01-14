using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AreaOverEffect : MonoBehaviour
{
    public float radius = 3f;
    // List<Combatant> targets = new List<Combatant>();
    HashSet<Combatant> targets = new HashSet<Combatant>();

    private DanielTestingKeys testKeys;
    private bool key1;
    private bool key2;
    private bool key3;

    public Combatant.TargetType[] onlyEnemies = new Combatant.TargetType[] { Combatant.TargetType.Enemy };
    public Combatant.TargetType[] enemiesAndEMonuments = new Combatant.TargetType[] { Combatant.TargetType.Enemy, Combatant.TargetType.EMonument };
    public Combatant.TargetType[] onlyGods = new Combatant.TargetType[] { Combatant.TargetType.Player };


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
            GetCircleAoETargets(transform.position, radius, Combatant.TargetType.Enemy);
        }

        if (key2)
        {
            key2 = false;
            GetCircleAoETargets(transform.position, radius, Combatant.TargetType.EMonument);
        }

        if (key3)
        {
            key3 = false;
            GetCircleAoETargets(transform.position, radius, Combatant.TargetType.Player);
        }

    }


    public void GetCircleAoETargets(Vector3 centre, float radius, Combatant.TargetType targetType)
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

    public HashSet<Combatant> GetTargets()
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