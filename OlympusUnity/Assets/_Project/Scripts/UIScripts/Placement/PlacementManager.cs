using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    private static PlacementManager _instance;
    public static PlacementManager Instance => _instance;

    public int currentGodIndex;

    public GameObject continueUI;
    public GameObject chosenGods;

    public bool allGodsPlaced;
    public int placementCount;

    [SerializeField]
    public List<GameObject> blueprints;
    public GameObject currentBluePrint;
    
    
    
    // Start is called before the first frame update
    void Start()
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

        //chosenGods = GameObject.Find("ChosenGods");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeCurrentGodIndex(int newIndex)
    {
        currentGodIndex = newIndex;
        currentBluePrint = blueprints[newIndex];
    }
    public GameObject ReturnCurrentGod()
    {
        GameObject godToReturn = UberManager.Instance.selectedGods[currentGodIndex].gameObject;
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
    public void CheckIfPlacementComplete()
    {
        placementCount = 0;
        continueUI.gameObject.SetActive(true);
        
    }

    public void TimeToGo()
    {
        GodPlacementInfo.Instance.god1 = UberManager.Instance.selectedGods[0].gameObject;
        GodPlacementInfo.Instance.god1Location = UberManager.Instance.selectedGods[0].gameObject.transform.position;
            
        GodPlacementInfo.Instance.god2 = UberManager.Instance.selectedGods[1].gameObject;
        GodPlacementInfo.Instance.god2Location = UberManager.Instance.selectedGods[1].gameObject.transform.position;
            
        GodPlacementInfo.Instance.god3 = UberManager.Instance.selectedGods[2].gameObject;
        GodPlacementInfo.Instance.god3Location = UberManager.Instance.selectedGods[2].gameObject.transform.position;
        
        UberManager.Instance.SwitchGameState(UberManager.GameState.GamePlay);
    }

    public void TryAgain()
    {
        Debug.Log("trying again");
        for (int i = 0; i < UberManager.Instance.selectedGods.Count;i++)
        {
            UberManager.Instance.selectedGods[i].gameObject.transform.position = (new Vector3(1000, 0, 0));
            continueUI.gameObject.SetActive(false);
        }
    }
    
}
