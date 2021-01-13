using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Influencer : AI_Movement
{
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();


        wpNum = 0;
        FindClosestWaypoint(transform.position);

    }
        // Update is called once per frame
        void Update()
    {
        Attack();
    }

    void SpawnDrones()
    {

    }
}
