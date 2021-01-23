﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class AIMovement : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent nav;
    private AIBrain _aiBrain;

    public List<Waypoint> wayPoints;
    public List<Waypoint> path;

    public Waypoint spawn;

    public Waypoint closestWayPoint;

    protected Transform destination;
    protected float rDist = 2f;

    protected int wpNum;
    [SerializeField]
    protected int wpIndex;
    public int test;
    private static readonly int VerticalF = Animator.StringToHash("Vertical_f");
    private bool _isCurrentAttackTargetNull;


    protected void Start()
    {
        
        _aiBrain = GetComponent<AIBrain>();
        animator = GetComponentInChildren<Animator>();
        nav = GetComponent<NavMeshAgent>();
        wayPoints = GameObject.FindGameObjectWithTag("Waypoint").GetComponent<Waypoint>().wayPoints;
        spawn = wayPoints[0];
        _isCurrentAttackTargetNull = _aiBrain.currentAttackTarget == null;
        wpNum = 0;
        FindNextWayPoint(spawn);
    }

    protected void Update()
    {
        _isCurrentAttackTargetNull = _aiBrain.currentAttackTarget;
    }

    protected void FixedUpdate()
    {
        var animSpeed = nav.velocity.magnitude / nav.speed;

        animator.SetFloat(VerticalF, animSpeed);

        if(_isCurrentAttackTargetNull)
        {
            transform.LookAt(destination.position);
        }

    }

    public void MoveToWayPoint()
    {
        destination = wayPoints[wpIndex].transform;
        nav.SetDestination(wayPoints[wpIndex].Pos);
        _aiBrain.initMove = false;

    }



    public virtual void Moving()
    {
        if (!nav.pathPending && nav.remainingDistance < rDist && wpIndex != 12)
        {


            var num = Random.Range(0, 10);
            if (num > 7)
            {
                _aiBrain.weightCheck = true;
                FindNextWayPoint(wayPoints[wpIndex]);
            }
            else
            {
                _aiBrain.weightCheck = false;
                MoveToWayPoint();
            }
        }
        else
        {
            _aiBrain.weightCheck = false;
            MoveToWayPoint();

        }

    }

    public Waypoint GetPath()
    {
                return wayPoints[wpIndex];
        
    }

    /*
    Once a wayPoint has been found, Add that wayPoint to a list,
    that list is a track of visited wayPoints.
    Then add it to another list which is the way point path to follow.
    */
    protected Waypoint FindNextWayPoint(Waypoint obj)
    {
        var length = obj.neighbors.Count;     // list of a wayPoints neighboring wayPoints.

        var key = Random.Range(0, length);

        //  add a randomly selected wayPoint to the path list from the wayPoints neighbor list.
        if (key >= obj.neighbors.Count) return null;
        var next = obj.neighbors[key];
        wpIndex = next.index;
        closestWayPoint = next;

        if (test != 0) return null;
        test++;
        MoveToWayPoint();
        return null;

    }

    public void MoveToTarget(Combatant target)
    {
        nav.SetDestination(target.transform.position);
    }
}
