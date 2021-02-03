using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class AITestManager : MonoBehaviour
{
    public enum SpawnState { Spawning, Waiting, Counting}

    public SpawnState state = SpawnState.Counting;

    public Transform prefab;
    public Transform left;

    public TMP_Text countDownTxt;
    public TMP_Text enemiesRemainingTxt;

    public float timeBetweenWaves = 2f;
    public float countDown = 5f;

    [SerializeField]
    private List<Transform> touristList;
    
    public int enemiesPerWave;
    public int numberOfWaves = 3;
    public int currentWave = 1;
    public float incrementAmount = 1.3f;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(Spawner());
    }

    private void Update()
    {
        if(TouristsIsAlive() && state == SpawnState.Waiting)
            enemiesRemainingTxt.text = "Enemies Left: " + touristList.Count;
    }

    private IEnumerator Spawner()
    {
        countDown = 3f;
        while (countDown > 0)
        {
            countDown -= 1.0f;
            countDownTxt.text = "Next Wave: " + countDown.ToString() + "s";
            yield return new WaitForSeconds(1f);
        }
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
        touristList = touristList.Where(e => e != null).ToList();

        return touristList.Count > 0;
    }

    private IEnumerator SpawnWave()
    {
        for (var i = 0; i < enemiesPerWave; i++)
        {
            if (i % 5 == 0)
            {
                SpawnJock();
                yield return new WaitForSeconds(0.5f);
                continue;
            }

            if (i % 6 == 0)
            {
                SpawnInfluencer();
                yield return new WaitForSeconds(0.5f);
                continue;
            }
            if (i % 3 == 0)
            {
                SpawnNerd();
                yield return new WaitForSeconds(0.5f);
                continue;
            }
            SpawnTourist();
            yield return new WaitForSeconds(0.5f);
        }
        enemiesPerWave = (int)(enemiesPerWave * incrementAmount);
    }

    private void SpawnTourist()
    {
        GameObject tourist = ObjectPools.SharedInstance.GetDronePoolObGameObject();
        if (tourist != null)
        {
            tourist.transform.position = left.position;
            tourist.transform.rotation = left.rotation;
            tourist.SetActive(true);
            touristList.Add(tourist.transform);
        }

    }

    private void SpawnInfluencer()
    {
        GameObject tourist = ObjectPools.SharedInstance.GetInfluencerPoolObGameObject();
        if (tourist != null)
        {
            tourist.transform.position = left.position;
            tourist.transform.rotation = left.rotation;
            tourist.SetActive(true);
            touristList.Add(tourist.transform);
        }

    }

    private void SpawnJock()
    {
        GameObject tourist = ObjectPools.SharedInstance.GetJockPoolObGameObject();
        if (tourist != null)
        {
            tourist.transform.position = left.position;
            tourist.transform.rotation = left.rotation;
            tourist.SetActive(true);
            touristList.Add(tourist.transform);
        }

    }

    private void SpawnNerd()
    {
        GameObject tourist = ObjectPools.SharedInstance.GetNerdPoolObGameObject();
        if (tourist != null)
        {
            tourist.transform.position = left.position;
            tourist.transform.rotation = left.rotation;
            tourist.SetActive(true);
            touristList.Add(tourist.transform);
        }

    }

}
