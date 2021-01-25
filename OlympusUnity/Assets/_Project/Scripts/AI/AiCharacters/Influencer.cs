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

    protected void Start()
    {
        StartCoroutine(Spawner());
    }


    private IEnumerator Spawner()
    {
        yield return new WaitForSeconds(countDown);

        while (true)
        {
            state = SpawnState.Spawning;

            yield return SpawnWave();

            state = SpawnState.Waiting;

            yield return new WaitWhile(TouristsIsAlive);

            state = SpawnState.Counting;

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    private bool TouristsIsAlive()
    {
        touristDrones = touristDrones.Where(e => e != null).ToList();

        return touristDrones.Count > 0;
    }

    private IEnumerator SpawnWave()
    {
        for (var i = 0; i < waveIndex; i++)
        {
            SpawnTourist();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void SpawnTourist()
    {
        touristDrones.Add(Instantiate(prefab, left.position, left.rotation));
    }

}
