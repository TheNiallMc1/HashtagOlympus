using System.Collections.Generic;
using UnityEngine;

public class GodSelectManager : MonoBehaviour
{
    private static GodSelectManager _instance;
    public static GodSelectManager Instance => _instance;

    public GameObject FinalSelectionStuff;
    public GameObject NormalSelectionStuff;

    public int addedGodCounter;
    
    private PlayerControls playerControls;

    private List<ModelBehaviour> addedModels;

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

        addedModels = new List<ModelBehaviour>();

        FinalSelectionStuff.gameObject.SetActive(false);
        addedGodCounter = 0;
        
        playerControls = new PlayerControls();
        playerControls.Enable();
    }

    public void AddGodToList(ModelBehaviour modelToAdd)
    {
        addedGodCounter++;
        addedModels.Add(modelToAdd);

        if (addedModels.Count ==3)
        {
            ConfirmFinalSelection();
        }
    }

    public void RemoveGodFromList(ModelBehaviour modelToRemove)
    {
        addedModels.Remove(modelToRemove);
        addedGodCounter--;
    }

    private void ConfirmFinalSelection()
    {
       NormalSelectionStuff.gameObject.SetActive(false);
       FinalSelectionStuff.gameObject.SetActive(true);
    }

    public void SendFinalSelection()
    {
        UberManager.Instance.SwitchGameState(UberManager.GameState.GodPlacement);
    }

    public void Reselect()
    {
        NormalSelectionStuff.gameObject.SetActive(true);
        FinalSelectionStuff.gameObject.SetActive(false);
    }
}
