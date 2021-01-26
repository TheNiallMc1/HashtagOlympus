using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GodSelectorButton : MonoBehaviour
{
    // Start is called before the first frame update
   // public GodBehaviour selectedGod;
    
    private Button btn;
    private Text buttonText;

    private bool isSelected;


    void Awake ( )
    {
        btn = GetComponent<Button> ( );
        buttonText = GetComponentInChildren<Text>();
        isSelected = false;
        buttonText.text = "SELECT";
    }

    public void UpdateButton()
    {
        if (!ShowModelController.Instance.currentModel.isSelected)
        {
            isSelected = false;
            buttonText.text = "SELECT";
        }

        if (ShowModelController.Instance.currentModel.isSelected)
        {
            isSelected = true;
            buttonText.text = "DESELECT";
        }
    }

    public void SendSelection()
    {
        //selectDeselect = !selectDeselect;

        if (!isSelected)
        {
            Debug.Log("selecting");
            //buttonText.text = "DESELECT";
            ShowModelController.Instance.currentModel.isSelected = true;
            GodSelectManager.Instance.AddGodToList(ShowModelController.Instance.currentModel);
            UpdateButton();
            
        } else if (isSelected)
        {
            Debug.Log("deselecting");
            //buttonText.text = "SELECT";
            ShowModelController.Instance.currentModel.isSelected = false;
           GodSelectManager.Instance.RemoveGodFromList(ShowModelController.Instance.currentModel);
           UpdateButton();
        }
    }
}
