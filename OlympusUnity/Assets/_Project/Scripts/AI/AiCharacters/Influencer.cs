using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.AI.AiControllers;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Influencer : AIBrain
{
    public enum SpawnState { Spawning, Waiting, Counting }

    public new SpawnState state = SpawnState.Counting;

    public Transform prefab;
    public Transform left;

    public float timeBetweenWaves = 5f;
    public float countDown = 2f;

    [SerializeField]
    private List<Transform> touristDrones;

    public int waveIndex = 20;

    private void LateUpdate()
    {
        if (RandomNumber() < 4)
        {
            SpawnWave();
        }
    }

    private void SpawnWave()
    {
        for (var i = 0; i < waveIndex; i++)
        {
            SpawnTourist();
        }
    }

    private void SpawnTourist()
    {
        GameObject touristDrone = ObjectPools.SharedInstance.GetPoolObGameObject();
        touristDrone.SetActive(true);
        touristDrones.Add(transform);

    }

    private int RandomNumber()
    {
        var randomNumber = UnityEngine.Random.Range(1, 10);
        return randomNumber;
    }
}
