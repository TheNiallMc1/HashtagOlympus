using System.Collections.Generic;
using UnityEngine;

public class ObjectPools : MonoBehaviour
{
    public static ObjectPools SharedInstance;
    public List<GameObject> pooledTouristDrones;
    public List<GameObject> pooledTouristInfluencer;
    public List<GameObject> pooledTouristJock;
    public List<GameObject> pooledTouristNerd;
    public GameObject droneObjectToPool;
    public GameObject influencerObjectToPool;
    public GameObject jockObjectToPool;
    public GameObject nerdObjectToPool;
    public int amountToPool;

    protected void Awake()
    {
        SharedInstance = this;
    }

    protected void Start()
    {
        pooledTouristDrones = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(droneObjectToPool);
            tmp.SetActive(false);
            pooledTouristDrones.Add(tmp);
        }
        for (int i = 0; i < 10; i++)
        {
            tmp = Instantiate(influencerObjectToPool);
            tmp.SetActive(false);
            pooledTouristInfluencer.Add(tmp);
        }
        for (int i = 0; i < 10; i++)
        {
            tmp = Instantiate(jockObjectToPool);
            tmp.SetActive(false);
            pooledTouristJock.Add(tmp);
        }
        for (int i = 0; i < 10; i++)
        {
            tmp = Instantiate(nerdObjectToPool);
            tmp.SetActive(false);
            pooledTouristNerd.Add(tmp);
        }
    }

    public GameObject GetDronePoolObGameObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if(!pooledTouristDrones[i].activeInHierarchy)
            {
                return pooledTouristDrones[i];
            }
        }
        return null;
    }
    public GameObject GetInfluencerPoolObGameObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledTouristInfluencer[i].activeInHierarchy)
            {
                return pooledTouristInfluencer[i];
            }
        }
        return null;
    }
    public GameObject GetJockPoolObGameObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledTouristJock[i].activeInHierarchy)
            {
                return pooledTouristJock[i];
            }
        }
        return null;
    }
    public GameObject GetNerdPoolObGameObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledTouristNerd[i].activeInHierarchy)
            {
                return pooledTouristNerd[i];
            }
        }
        return null;
    }
}
