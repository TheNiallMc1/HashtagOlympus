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
    public TMP_Text finalSelectionText;

    public GameObject chosenGods;
    public int addedGodCounter;
    
    private PlayerControls playerControls;

    
    public Dictionary<ModelBehaviour, GameObject> modelToGod;

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
        modelToGod = new Dictionary<ModelBehaviour, GameObject>();
        
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
        Debug.Log("adding : " + modelToAdd.godName+", now: "+addedGodCounter);
        //use model as key to get god from dict to add to list

        if (addedGodCounter == 3)
        {
            ConfirmFinalSelection();
        }
    }

    public void RemoveGodFromList(ModelBehaviour modelToRemove)
    {
        addedGodCounter--;
        Debug.Log("removing : " + modelToRemove.godName+", now: "+addedGodCounter);
    }
    
    public void ConfirmFinalSelection()
    {
        SelectionButtons.gameObject.SetActive(false);
        FinalSelectionStuff.gameObject.SetActive(true);
       // finalSelectionText.text = selectedGods[0].godName + "\n" + selectedGods[1].godName + "\n" +
                              //    selectedGods[2].godName;

    }

    public void SendFinalSelection()
    {
        //send to game mananger
        Debug.Log("sending final selection");
        //UberManager.Instance.AddSelectedGodList(selectedGods);
    }

    public void Reselect()
    {
        //selectedGods.Clear();
        FinalSelectionStuff.gameObject.SetActive(false);
       // SelectionButtons.gameObject.SetActive(true);
    }

   /* public void AddGodToList(GodBehaviour selectedGod)
    {
        selectedGods.Add(selectedGod);
        Debug.Log("added :" + selectedGod.godName+" : "+selectedGods.Count);
        //selectedGod.gameObject.GetComponentInParent<GodDontDestroy>().ChooseUnchooseThisGod(true);
        selectedGod.gameObject.transform.SetParent(chosenGods.transform, true);
        
        if (selectedGods.Count == 3)
        {
            ConfirmFinalSelection();
        }
    }

    public void RemoveGodFromList(GodBehaviour selectedGod)
    {
        selectedGods.Remove(selectedGod);
        //selectedGod.gameObject.GetComponentInParent<GodDontDestroy>().ChooseUnchooseThisGod(false);
        selectedGod.gameObject.transform.SetParent(null);
        Debug.Log("removed :" + selectedGod.godName+" : "+selectedGods.Count);
    }
    
    
    public void ConfirmFinalSelection()
    {
        SelectionButtons.gameObject.SetActive(false);
        FinalSelectionStuff.gameObject.SetActive(true);
        finalSelectionText.text = selectedGods[0].godName + "\n" + selectedGods[1].godName + "\n" +
                               selectedGods[2].godName;

    }

    public void SendFinalSelection()
    {
        //send to game mananger
        Debug.Log("sending final selection");
        UberManager.Instance.AddSelectedGodList(selectedGods);
    }

    public void Reselect()
    {
        selectedGods.Clear();
        FinalSelectionStuff.gameObject.SetActive(false);
        SelectionButtons.gameObject.SetActive(true);
    }*/
}
