using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodPlacementInfo : MonoBehaviour
{
    private static GodPlacementInfo _instance = null; 
    public static GodPlacementInfo Instance { get { return _instance; } } 

    public int currentGodIndex;

    public GameObject god1;
    public Vector3 god1Location;
    
    public GameObject god2;
    public Vector3 god2Location;
    
    public GameObject god3;
    public Vector3 god3Location;

    public List<GameObject> allGodsList;
    
    void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null; 
        }
    }
    
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject); 
            }
        }
    }

    void Start()
    {
        allGodsList = new List<GameObject>();
    }
}
