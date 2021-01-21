using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AI_Movement : MonoBehaviour
{

    protected NavMeshAgent nav;
    private AI_Brain aiBrain;

    public List<Waypoint> waypoints;
    public List<Waypoint> Path;

    public Waypoint spawn;

    public Waypoint closestWaypoint = null;

    protected Transform _destination;
    protected float rdist = 2f;

    protected int wpNum = 0;
    [SerializeField]
    protected int wpIndex = 0;
    public int test = 0;


    void Start()
    {
        waypoints = GameObject.FindGameObjectWithTag("Waypoint").GetComponent<Waypoint>().wayPoints;
        spawn = waypoints[0];
        nav = GetComponent<NavMeshAgent>();
        aiBrain = GetComponent<AI_Brain>();
        wpNum = 0;
        FindNextWaypoint(spawn);

    }

    void MoveToWaypoint()
    {

        nav.SetDestination(waypoints[wpIndex].pos);
        aiBrain.initMove = false;
     //   Debug.Log(aiBrain.initMove);

    }



    public virtual void Moving()
    {
        if (!nav.pathPending && nav.remainingDistance < rdist && wpIndex != 12)
        {
            

                int num = UnityEngine.Random.Range(0, 10);
                if (num > 7)
                {

                    Debug.Log("nearby");
                    aiBrain.wieghtCheck = true;
                    FindNextWaypoint(waypoints[wpIndex]);
                }
                else
                {
                    aiBrain.wieghtCheck = false;
                    MoveToWaypoint();
                }
            }
            else
            { 
                aiBrain.wieghtCheck = false;
                MoveToWaypoint();
               
            }

    }

    public Waypoint GetPath()
    {
                return waypoints[wpIndex];
        
    }

    public bool IsMe()
    {
        foreach (AI_Brain tourist in waypoints[wpIndex].touristsNearby)
        {
            if(tourist == aiBrain)
            {
                return true;
            }
        }
        return false;
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
        
        int length = obj.neighbors.Count;     // list of a waypoints neighbooring waypoints.

        key = UnityEngine.Random.Range(0, length);

        //  add a randomly selected waypoint to the path list from the waypoints neighboor list.
        if (key < obj.neighbors.Count)
        {
            next = obj.neighbors[key];
            wpIndex = next.index;

            if (test == 0)
            {
                test++;
                MoveToWaypoint();
            }
        }
        return null;

    }

    public void FindClosestWaypoint(Vector3 target)
    {  
        float closestDist = Mathf.Infinity;

        for (int i = 0; i < waypoints.Count; i++)
        {
          

            var dist = (waypoints[i].transform.position - target).magnitude;
            if (dist < closestDist)
            {
                closestWaypoint = waypoints[i];
                closestDist = dist;
            }
        }

        wpIndex = closestWaypoint.index;
        FindNextWaypoint(closestWaypoint);
    }


    public void MoveToTarget(GameObject target)
    {
        nav.SetDestination(target.transform.position);
    }
}
