using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodPlacementInfo : MonoBehaviour
{
    private static GodPlacementInfo _instance = null; 
    public static GodPlacementInfo Instance { get { return _instance; } } 

    public int currentGodIndex;

    public GodBehaviour god1;
    public Vector3 god1Location;
    
    public GodBehaviour god2;
    public Vector3 god2Location;
    
    public GodBehaviour god3;
    public Vector3 god3Location;
    
    
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
