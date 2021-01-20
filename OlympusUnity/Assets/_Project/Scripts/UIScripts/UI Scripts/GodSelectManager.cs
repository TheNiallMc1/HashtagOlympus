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
    
    public List<GodBehaviour> selectedGods;
    public bool selectionComplete;

    public GameObject SelectionButtons;
    public GameObject FinalSelectionStuff;
    public TMP_Text finalSelectionText;

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
        
        selectedGods = new List<GodBehaviour>();
        selectionComplete = false;
        FinalSelectionStuff.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void AddGodToList(GodBehaviour selectedGod)
    {
        selectedGods.Add(selectedGod);
        Debug.Log("added :" + selectedGod.godName+" : "+selectedGods.Count);
        
        if (selectedGods.Count == 3)
        {
            ConfirmFinalSelection();
        }
    }

    public void RemoveGodFromList(GodBehaviour selectedGod)
    {
        selectedGods.Remove(selectedGod);
        Debug.Log("removed :" + selectedGod.godName+" : "+selectedGods.Count);
    }
    
    
    public void ConfirmFinalSelection()
    {
        //show conf button
        Debug.Log("Confirm final selection");
        SelectionButtons.gameObject.SetActive(false);
        FinalSelectionStuff.gameObject.SetActive(true);
        finalSelectionText.text = selectedGods[0].godName + "\n" + selectedGods[1].godName + "\n" +
                               selectedGods[2].godName;

    }

    public void SendFinalSelection()
    {
        //send to game mananger
        Debug.Log("sending final selection");
    }

    public void Reselect()
    {
        selectedGods.Clear();
        FinalSelectionStuff.gameObject.SetActive(false);
        SelectionButtons.gameObject.SetActive(true);
    }
}
