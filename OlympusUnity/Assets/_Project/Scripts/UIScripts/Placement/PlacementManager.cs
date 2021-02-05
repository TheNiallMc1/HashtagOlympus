﻿using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    private static PlacementManager _instance;
    public static PlacementManager Instance => _instance;

    public int currentGodIndex;

    public GameObject continueUI;

    public int placementCount;

    [SerializeField]
    public List<GameObject> blueprints;
    public GameObject currentBluePrint;

    [SerializeField] public List<GameObject> godModels;
    
    
    
    // Start is called before the first frame update
    private void Start()
    {
        // Creating singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        
        continueUI.gameObject.SetActive(false);
        placementCount = 0;
    }

    public void ChangeCurrentGodIndex(int newIndex)
    {
        currentGodIndex = newIndex;
        currentBluePrint = blueprints[newIndex];
    }
    public GameObject ReturnCurrentGod()
    {
       GameObject godToReturn = godModels[currentGodIndex].gameObject;
        return godToReturn;
    }

    public void IncreaseCount()
    {
        placementCount++;

        if (placementCount > 2)
        {
            CheckIfPlacementComplete();
        }
    }

    private void CheckIfPlacementComplete()
    {
        placementCount = 0;
        continueUI.gameObject.SetActive(true);
    }

    public void TimeToGo()
    {
        //GodPlacementInfo.Instance.god1 = UberManager.Instance.selectedGods[0].gameObject;
        GodPlacementInfo.Instance.god1Location = godModels[0].gameObject.transform.position;
            
        //GodPlacementInfo.Instance.god2 = UberManager.Instance.selectedGods[1].gameObject;
        GodPlacementInfo.Instance.god2Location = godModels[1].gameObject.transform.position;
            
       // GodPlacementInfo.Instance.god3 = UberManager.Instance.selectedGods[2].gameObject;
        GodPlacementInfo.Instance.god3Location = godModels[2].gameObject.transform.position;
        
        UberManager.Instance.SwitchGameState(UberManager.GameState.GamePlay);
    }

    public void TryAgain()
    {
        foreach (GameObject t in godModels)
        {
            t.gameObject.transform.position = (new Vector3(1000, 0, 0));
            continueUI.gameObject.SetActive(false);
        }
    }
    
}
