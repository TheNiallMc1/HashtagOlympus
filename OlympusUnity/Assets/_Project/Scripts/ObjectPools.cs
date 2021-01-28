using System.Collections.Generic;
using UnityEngine;

public class ObjectPools : MonoBehaviour
{
    public static ObjectPools SharedInstance;
    public List<GameObject> pooledTouristDrones;
    public GameObject objectToPool;
    public int amountToPool;

    // Start is called before the first frame update
    protected void Awake()
    {
        SharedInstance = this;
    }

    // Update is called once per frame
    protected void Start()
    {
        pooledTouristDrones = new List<GameObject>();
        GameObject tmp;
        for (var i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledTouristDrones.Add(tmp);
        }
    }

    public GameObject GetPoolObGameObject()
    {
        for (var i = 0; i < amountToPool; i++)
        {
            if(!pooledTouristDrones[i].activeInHierarchy)
            {
                return pooledTouristDrones[i];
            }
        }
        return null;
    }
}
