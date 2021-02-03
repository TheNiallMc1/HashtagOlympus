using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.AI.AiControllers;
using UnityEngine;

public class ObjectPools : MonoBehaviour
{
    [Header("Pooled Objects")]
    public static ObjectPools SharedInstance;
    public List<GameObject> pooledTouristDrones;
    public List<GameObject> pooledTouristInfluencer;
    public List<GameObject> pooledTouristJock;
    public List<GameObject> pooledTouristNerd;

    [Header("Objects to Pool")]
    public GameObject droneObjectToPool;
    public GameObject influencerObjectToPool;
    public GameObject jockObjectToPool;
    public GameObject nerdObjectToPool;
    public int amountToPool;

    public GameObject wayPoints;
    public List<Waypoint> wpList;

    protected void Awake()
    {
        SharedInstance = this;
    }

    protected void Start()
    {
        wpList = wayPoints.GetComponent<Waypoint>().wayPoints;
        pooledTouristDrones = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(droneObjectToPool);
            tmp.GetComponent<AIMovement>().wayPoints = wpList;
            tmp.SetActive(false);
            pooledTouristDrones.Add(tmp);
        }
        for (int i = 0; i < 10; i++)
        {
            tmp = Instantiate(influencerObjectToPool);
            tmp.GetComponent<AIMovement>().wayPoints = wpList;
            tmp.SetActive(false);
            pooledTouristInfluencer.Add(tmp);
        }
        for (int i = 0; i < 10; i++)
        {
            tmp = Instantiate(jockObjectToPool);
            tmp.GetComponent<AIMovement>().wayPoints = wpList;
            tmp.SetActive(false);
            pooledTouristJock.Add(tmp);
        }
        for (int i = 0; i < 10; i++)
        {
            tmp = Instantiate(nerdObjectToPool);
            tmp.GetComponent<AIMovement>().wayPoints = wpList;
            tmp.SetActive(false);
            pooledTouristNerd.Add(tmp);
        }
    }

    public GameObject GetDronePoolObGameObject()
    {
        return pooledTouristDrones.FirstOrDefault(t => !t.activeInHierarchy);
    }
    public GameObject GetInfluencerPoolObGameObject()
    {
        return pooledTouristInfluencer.FirstOrDefault(t => !t.activeInHierarchy);
    }
    public GameObject GetJockPoolObGameObject()
    {
        return pooledTouristJock.FirstOrDefault(t => !t.activeInHierarchy);
    }
    public GameObject GetNerdPoolObGameObject()
    {
        return pooledTouristNerd.FirstOrDefault(t => !t.activeInHierarchy);
    }
}
