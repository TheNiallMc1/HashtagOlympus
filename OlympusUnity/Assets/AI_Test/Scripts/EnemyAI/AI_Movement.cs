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

    protected Transform _destination;
    protected float rdist = 2;

    protected int wpNum = 0;
    protected int test = 0;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        aiBrain = GetComponent<AI_Brain>();
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

        MoveToWaypoint(wpNum1);
    }

    void MoveToWaypoint(int num)
    {

        nav.SetDestination(Path[0].pos);
        aiBrain.initMove = false;
        Debug.Log(aiBrain.initMove);

    }


    void FixedUpdate()
    {

    }
    public virtual void Moving()
    {
        if (!nav.pathPending && nav.remainingDistance < rdist && Path[0].name != "Waypoint (9)")
        {
            
            waypoints.Add(Path[0]);
            Path.RemoveAt(0);
            FindNextWaypoint(waypoints[waypoints.Count - 1]);
            MoveToNextWaypoint();

        }
    }

    public Waypoint GetPath()
    {

        Debug.Log(waypoints.Count);
        if (waypoints.Count > 0)
        {
            if (waypoints[waypoints.Count - 1].transform.parent.gameObject.tag == "Monument")
            {
                return waypoints[waypoints.Count - 1];
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

    /*
    Once a waypoint has been found, Add that waypoint to a list,
    that list is a track of visited waypoints.
    Then add it to another list which is the way point path to follow.
    */
    protected Waypoint FindNextWaypoint(Waypoint obj)
    {

        Waypoint next = null;
        int key;
        
        if (obj != null)
        {
            Debug.Log("Add Waypoint: " + obj);
            waypoints.Add(obj);
           
        }
        int length = obj.neighbors.Count;     // list of a waypoints neighbooring waypoints.

        key = UnityEngine.Random.Range(0, length);

        //  add a randomly selected waypoint to the path list from the waypoints neighboor list.
        if (key < obj.neighbors.Count)
        {
            next = obj.neighbors[key];
            Path.Add(next);

            if (test == 0)
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
            Debug.Log("Waypoint Found");
            FindNextWaypoint(closestWaypoint);
        }
        return null;
    }

    public void MoveToTarget(GameObject target)
    {
        nav.SetDestination(target.transform.position);
    }
}
