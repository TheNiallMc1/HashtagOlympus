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

    public Waypoint waypointListHolder;

    protected Transform _destination;
    protected float rdist = 2;

    protected int wpNum = 0;
    protected int wpIndex = 0;
    public int test = 0;


    void Start()
    {
        waypoints = GameObject.FindGameObjectWithTag("Waypoint").GetComponent<Waypoint>().wayPoints;
        nav = GetComponent<NavMeshAgent>();
        aiBrain = GetComponent<AI_Brain>();
        wpNum = 0;
        FindClosestWaypoint(transform.position);

    }

    void MoveToWaypoint()
    {

        nav.SetDestination(waypoints[wpIndex].pos);
        aiBrain.initMove = false;
     //   Debug.Log(aiBrain.initMove);

    }

    private void FixedUpdate()
    {
        if (waypoints[wpIndex].touristsNearby != null)
        {
            if (waypoints[wpIndex].touristsNearby.Count > 5 && !IsMe())
            {
                Moving();
    }
}
    }

    public virtual void Moving()
    {
        if (!nav.pathPending && nav.remainingDistance < rdist && wpIndex != 7)
        {
            
            //FindNextWaypoint(waypoints[wpIndex]);
            if (waypoints[wpIndex].touristsNearby.Count > 5)
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
    }

    public Waypoint GetPath()
    {

       // Debug.Log(waypoints.Count);
        if (waypoints.Count > 0)
        {
            if (waypoints[wpIndex].transform.parent.gameObject.tag == "Monument")
            {
                return waypoints[wpIndex];
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
        
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
        Waypoint closestWaypoint = null;
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


    ///*
    //Search the area to find the first waypoint to the AI.
    //*/
    //public Waypoint FindInitialWaypoint(Vector3 target)
    //{
    //    aiBrain.priority = AI_Brain.ePriority.Moving;
    //    aiBrain.state = AI_Brain.eState.Moving;
    //    Waypoint closestWaypoint = null;
    //    float closestDist = Mathf.Infinity;
    //    foreach (var waypoint in GameObject.FindGameObjectsWithTag("Waypoint"))
    //    {

    //        var dist = (waypoint.transform.position - target).magnitude;
    //        if (dist < closestDist)
    //        {
    //            closestWaypoint = waypoint.GetComponent<Waypoint>();
    //            closestDist = dist;
    //        }
    //    }
        
    //    if (closestWaypoint != null)
    //    {
    //        waypoints = closestWaypoint.wayPoints;
    //    //    Debug.Log("Waypoint Found");
    //        FindNextWaypoint(closestWaypoint);
    //    }
    //    return null;
    //}

    public void MoveToTarget(GameObject target)
    {
        nav.SetDestination(target.transform.position);
    }
}
