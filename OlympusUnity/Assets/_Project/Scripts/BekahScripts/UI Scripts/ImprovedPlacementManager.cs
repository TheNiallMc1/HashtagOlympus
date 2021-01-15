using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovedPlacementManager : MonoBehaviour
{
    private static ImprovedPlacementManager _instance = null; 
    public static ImprovedPlacementManager Instance { get { return _instance; } } 

    public int currentGodIndex;
    
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
