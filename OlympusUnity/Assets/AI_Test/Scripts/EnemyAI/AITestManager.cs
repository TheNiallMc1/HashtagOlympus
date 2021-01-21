﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AITestManager : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING}

    public SpawnState state = SpawnState.COUNTING;

    public Transform prefab;
    public Transform left;

    public float timeBetweenWaves = 5f;
    public float countDown = 2f;

    [SerializeField]
    private List<Transform> touristDrones;
    
    public int waveIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        yield return new WaitForSeconds(countDown);

        while (true)
        {
            state = SpawnState.SPAWNING;

            yield return SpawnWave();

            state = SpawnState.WAITING;

            yield return new WaitWhile(TouristsIsAlive);

            state = SpawnState.COUNTING;

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
        waveIndex++;
        for (int i = 0; i < waveIndex; i++)
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
