using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BaseEnemyAI : MonoBehaviour
{

    protected enum ePriority { Moving, Monument, God }
    protected enum eState { idle, Moving, Attacking, Ability }

    [Header("Inscribed")]
    [SerializeField]
    protected List<Waypoint> waypoints;
    [SerializeField]
    protected List<Waypoint> Path;

    protected float _health;
    protected float _damage = 50;
    protected float _speed;
    protected Transform _destination;
    protected float rdist = 2;

    [Header("Dynamic")]
    [SerializeField]
    protected ePriority _priority = ePriority.Moving;
    [SerializeField]
    protected eState _state = eState.idle;

    protected int wpNum = 0;
    protected int test = 0;

    protected GameObject attackTarget;
    MonumentHealth monument;
    protected NavMeshAgent nav;

    protected ePriority priority { get { return _priority; } set { _priority = value; } }
    protected eState state { get { return _state; } set { _state = value; } }
    public float health { get { return _health; } set { _health = value; } }


    void Start()
    {
        nav = GetComponent<NavMeshAgent>();


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

        nav.SetDestination(Path[0].pos);


    }


    void FixedUpdate()
    {

        StopAllCoroutines();
        switch (_state)
        {
            case eState.Moving:
                StopAllCoroutines();
                Moving();
                break;
            case eState.Attacking:
                Attack();
                break;
        }
    }
    protected virtual void Moving()
    {
        if (!nav.pathPending && nav.remainingDistance < rdist && Path[0].name != "Waypoint (9)")
        {
            FindClosestMonument(transform.position);
            var near = 5;

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
    }

    protected virtual void Attack()
    {
        if (_priority == ePriority.God)
        {
            nav.stoppingDistance = 0.1f;
            if (attackTarget != null)
            {
                nav.SetDestination(attackTarget.transform.position);
                if (!nav.pathPending && nav.remainingDistance < 0.1)
                {
                    StartCoroutine(AttackingMonumentCoroutine());
                }

            }
            else
            {
                _priority = ePriority.Moving;
                _state = eState.Moving;

            }
        }
        if (_priority == ePriority.Monument)
        {
            nav.stoppingDistance = 0.1f;
            if (monument != null)
            {
                nav.SetDestination(monument.transform.position);
                if (!nav.pathPending && nav.remainingDistance < 0.1)
                {
                    StartCoroutine(AttackingMonumentCoroutine());
                }

            }
            else
            {
                _priority = ePriority.Moving;
                _state = eState.Moving;

            }
        }
    }

    /*
     Handles AI while it is attacking a monument. 
    */
    protected IEnumerator AttackingMonumentCoroutine()
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
            }
                
           yield return new WaitForSeconds(5);

        }
        yield break;
    }

    /*
    Handles AI while it is targetting a God. 
    */
    protected IEnumerator AttackingGodCoroutine()
    {
        yield break;
    }


    /*
    *****************************************************
                        May be deleted
    *****************************************************
    */
    IEnumerator MonumentStateCoroutine()
    {
        yield return new WaitForSeconds(10);
        
    }

    /*
    Once a waypoint has been found, Add that waypoint to a list,
    that list is a track of visited waypoints.
    Then add it to another list which is the way point path to follow.
    */
    protected Waypoint FindNextWaypoint(Waypoint obj)
    {
       
        Waypoint next = null;
        int key;
        waypoints.Add(obj);
        int length = obj.neighbors.Count;     // list of a waypoints neighbooring waypoints.
     
        key = UnityEngine.Random.Range(0, length);
       
        //  add a randomly selected waypoint to the path list from the waypoints neighboor list.
        if (key < obj.neighbors.Count)
        {
            next = obj.neighbors[key];
            Path.Add(next);

            if(test == 0)
            {
                test++;
                MoveToNextWaypoint();
            }
        }
        return null;
        
    }
    /*
    Search the area to find the closest waypoint to the AI.
    */
    protected Waypoint FindClosestWaypoint(Vector3 target)
    {
        Waypoint closestWaypoint = null;
        float closestDist = Mathf.Infinity;
        foreach (var waypoint in GameObject.FindGameObjectsWithTag("Waypoint"))
        {

            var dist = (waypoint.transform.position - target).magnitude;
            if (dist < closestDist)
            {
                closestWaypoint = waypoint.GetComponent<Waypoint>();
                closestDist = dist;
            }
        }
        if (closestWaypoint != null)
        {         
            FindNextWaypoint(closestWaypoint);
        }
        return null;
    }
    /*
    Search the area to find the around a waypoint to find the
    closest monument to that waypoint AI, set the monument as the next target.
    */
    protected MonumentHealth FindClosestMonument(Vector3 target)
    {
        MonumentHealth closestMonument = null;
        float closestDist = Mathf.Infinity;
        foreach (var monument in GameObject.FindGameObjectsWithTag("Monument"))
        {
            var dist = (monument.transform.position - target).magnitude;
            if (dist < closestDist)
            {
                closestMonument = monument.GetComponent<MonumentHealth>();
                closestDist = dist;
            }
        }
        if (closestMonument != null)
        {
            attackTarget = closestMonument.gameObject;
            monument = closestMonument;
            return closestMonument;
        }
        return null;
    }
}
