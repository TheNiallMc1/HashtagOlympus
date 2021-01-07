using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BaseEnemyAI : MonoBehaviour
{

    public enum ePriority      { Moving, Monument, God}
    public enum eState         {  idle, Moving, Attacking, Ability}

    [Header("Inscribed")]
    [SerializeField]
    List<Waypoint>       waypoints;
    [SerializeField]
    List<Waypoint>       Path;
    private float       _health;
    private float       _damage = 50;
    private float       _speed;
    public Transform    _destination;
    public float rdist = 2;

    [Header("Dynamic")]
    [SerializeField]
    private ePriority _priority = ePriority.Moving;
    [SerializeField]
    private eState  _state = eState.idle;
    public int         wpNum    = 0;
    public int test = 0;
    MonumentHealth monument;

    protected NavMeshAgent nav;

    public ePriority priority { get { return _priority; } set { _priority = value; } }
    public eState state { get { return _state; } set { _state = value; } }
    public float health { get { return _health; } set { _health = value; } }

    // Use this for initialization
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
       // nav.stoppingDistance = 0.01f;

        wpNum = 0;
        FindClosestWaypoint(transform.position);
        
    }

    void MoveToNextWaypoint()
    {


        int wpNum1 = 0;
        if (wpNum1 > Path.Count)
        {
            return;
        }
        _state = eState.Moving;
        MoveToWaypoint(wpNum1);
    }

    void MoveToWaypoint(int num)
    {
        //Debug.Log(Path.Count);
        nav.SetDestination(Path[0].pos);
        //nav.isStopped = true;
        //nav.updatePosition = false;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Debug.Log(Path.Count);
        //StopAllCoroutines();
        switch (_state)
        {
            case eState.Moving:
                StopAllCoroutines();
                if (!nav.pathPending && nav.remainingDistance < rdist && Path[0].name != "Waypoint (9)")
                {
                    FindClosestMonument(transform.position);
                    var near = 5;
                   // Debug.Log(monument.name);
                    if ((monument.transform.position - transform.position).magnitude < near)
                    {
                        _state = eState.Attacking;
                        _priority = ePriority.Monument;
                    }
                    else
                    {
                        waypoints.Add(Path[0]);
                        Path.RemoveAt(0);
                        FindNextWaypoint(waypoints[waypoints.Count - 1]);
                        MoveToNextWaypoint();
                    }
                    
                }
                break;
            case eState.Attacking:
                if (_priority == ePriority.Monument)
                {
                    nav.stoppingDistance = 0.1f;
                    if (monument != null)
                    {                   
                        nav.SetDestination(monument.transform.position);
                        if (!nav.pathPending && nav.remainingDistance < 0.02)
                        {
                            Debug.Log("I wasnt supposed to be here today");
                            StartCoroutine(AttackingCoroutine());
                        }
                        
                    }
                    else
                        {
                            //StopCoroutine(AttackingCoroutine());
                            _priority = ePriority.Moving;
                            _state = eState.Moving;
                            
                        }
 

                    
                }
                break;

        }
        
    }

    IEnumerator AttackingCoroutine()
    {
        while (monument != null || _state == eState.Attacking)
        {

            if (monument == null || monument.Health <= 0)
            {
               // yield return new WaitForSeconds(10);
                if (monument != null)
                    monument.RemoveObject();
                
            }
            else
            {
                monument.Health = monument.Health - _damage;
                //Debug.Log(monument.Health);
                
            }
                
           yield return new WaitForSeconds(5);

        }
        yield break;
    }

    IEnumerator MonumentStateCoroutine()
    {
        yield return new WaitForSeconds(10);
        
    }


    private Waypoint FindNextWaypoint(Waypoint obj)
    {
        //Debug.Log(waypoints.Count);
        Waypoint next = null;
        int key;
        waypoints.Add(obj);
        int length = obj.neighbors.Count;
       // Debug.Log(length);
        key = UnityEngine.Random.Range(0, length);
       // Debug.Log(key);

        if (key < obj.neighbors.Count)
        {
            next = obj.neighbors[key];
           // Debug.Log(next.name);
            Path.Add(next);
           // Debug.Log(Path.Count);

            if(test == 0)
            {
                test++;
                MoveToNextWaypoint();
            }
           // return next;
        }
        return null;
        
    }

    private Waypoint FindClosestWaypoint(Vector3 target)
    {
        Waypoint closestWaypoint = null;
        float closestDist = Mathf.Infinity;
        foreach (var waypoint in GameObject.FindGameObjectsWithTag("Waypoint"))
        {
            var dist = (waypoint.transform.position - target).magnitude;
            if (dist < closestDist)
            {
                closestWaypoint = waypoint.GetComponent<Waypoint>();
                //Debug.Log(closest.name);
                closestDist = dist;
            }
        }
        if (closestWaypoint != null)
        {
           // Debug.Log(closest);
            //return closest;
         
            FindNextWaypoint(closestWaypoint);
        }
        return null;
    }
    private MonumentHealth FindClosestMonument(Vector3 target)
    {
        MonumentHealth closestMonument = null;
        float closestDist = Mathf.Infinity;
        foreach (var monument in GameObject.FindGameObjectsWithTag("Monument"))
        {
            var dist = (monument.transform.position - target).magnitude;
            if (dist < closestDist)
            {
                closestMonument = monument.GetComponent<MonumentHealth>();
                //Debug.Log(closest.name);
                closestDist = dist;
            }
        }
        if (closestMonument != null)
        {
            // Debug.Log(closest);
            //return closest;
            monument = closestMonument;
           // Debug.Log(monument.name);
            return closestMonument;
        }
        return null;
    }
}
