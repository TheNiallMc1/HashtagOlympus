﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AITestManager : MonoBehaviour
{
    public enum SpawnState { Spawning, Waiting, Counting}

    public SpawnState state = SpawnState.Counting;

    public Transform prefab;
    public Transform left;

    public float timeBetweenWaves = 5f;
    public float countDown = 2f;

    [SerializeField]
    private List<Transform> touristDrones;
    
    public int enemiesPerWave = 0;
    public int numberOfWaves = 3;
    public int currentWave = 1;
    public float incrementAmount = 1.3f;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        yield return new WaitForSeconds(countDown);

        while (currentWave != numberOfWaves + 1)
        {
            state = SpawnState.Spawning;

            yield return SpawnWave();

            state = SpawnState.Waiting;

            yield return new WaitWhile(TouristsIsAlive);

            state = SpawnState.Counting;

            yield return new WaitForSeconds(timeBetweenWaves);

            currentWave++;
        }
    }

    private bool TouristsIsAlive()
    {
        touristDrones = touristDrones.Where(e => e != null).ToList();

        return touristDrones.Count > 0;
    }

    private IEnumerator SpawnWave()
    {
        for (var i = 0; i < enemiesPerWave; i++)
        {
            SpawnTourist();
            yield return new WaitForSeconds(0.5f);
        }
        enemiesPerWave = (int)(enemiesPerWave * incrementAmount);
    }

    private void SpawnTourist()
    {
        touristDrones.Add(Instantiate(prefab, left.position, left.rotation));
    }

}
