using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GodSelectManager : MonoBehaviour
{
    private static GodSelectManager _instance;
    public static GodSelectManager Instance => _instance;
    
    public List<GameObject> godPrefabs;
    public bool selectionComplete;

    public GameObject SelectionButtons;
    public GameObject FinalSelectionStuff;
    public GameObject NormalSelectionStuff;
    public TMP_Text finalSelectionText;

    public GameObject chosenGods;
    public int addedGodCounter;
    
    private PlayerControls playerControls;

    private List<ModelBehaviour> addedModels;
    
   // public Dictionary<ModelBehaviour, GameObject> modelToGod;

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
        
        godPrefabs = new List<GameObject>();
       // modelToGod = new Dictionary<ModelBehaviour, GameObject>();
       addedModels = new List<ModelBehaviour>();
        
        selectionComplete = false;
        FinalSelectionStuff.gameObject.SetActive(false);
        addedGodCounter = 0;
        
        playerControls = new PlayerControls();
        playerControls.Enable();

        //playerControls.Movement.LeftMouseClick.performed += context => DragToRotate();
        //playerControls.Movement.

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DragToRotate()
    {
        
    }

    void CreateModelToGodDisctionary()
    {
        //1. populate prefab list
        //2. loop thru, compare string names
        //3. if match then add
        
    }

    public void AddGodToList(ModelBehaviour modelToAdd)
    {
        addedGodCounter++;
        addedModels.Add(modelToAdd);
        Debug.Log("adding : " + modelToAdd.godName+", now: "+addedGodCounter);
        //use model as key to get god from dict to add to list

        if (addedModels.Count ==3)
        {
            ConfirmFinalSelection();
        }
    }

    public void RemoveGodFromList(ModelBehaviour modelToRemove)
    {
        addedModels.Remove(modelToRemove);
        addedGodCounter--;
        Debug.Log("removing : " + modelToRemove.godName+", now: "+addedGodCounter);
    }
    
    public void ConfirmFinalSelection()
    {
       NormalSelectionStuff.gameObject.SetActive(false);
       FinalSelectionStuff.gameObject.SetActive(true);
       // finalSelectionText.text = selectedGods[0].godName + "\n" + selectedGods[1].godName + "\n" +
                              //    selectedGods[2].godName;
    }

    public void SendFinalSelection()
    {
        //send to game mananger
        Debug.Log("sending final selection");
        //UberManager.Instance.AddSelectedGodList(selectedGods);
        UberManager.Instance.SwitchGameState(UberManager.GameState.GodPlacement);
    }

    public void Reselect()
    {
        //selectedGods.Clear();
        NormalSelectionStuff.gameObject.SetActive(true);
        FinalSelectionStuff.gameObject.SetActive(false);
       
    }
}
