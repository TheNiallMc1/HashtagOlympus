using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Nerd : BaseEnemyAI
{
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
