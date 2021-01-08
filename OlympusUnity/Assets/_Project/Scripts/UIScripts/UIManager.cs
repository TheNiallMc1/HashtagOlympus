using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI respectCounter;
    public TextMeshProUGUI currentGodName;

    public List<Button> godPortraits;

    public void Start()
    {
        UpdateRespectText();
        UpdateCurrentGodText();

        for (int index = 0; index < godPortraits.Count; index++)
        {
            Button currentButton = godPortraits[index];
            
            currentButton.onClick.AddListener(delegate
            {
                UpdateAllPortraits();
            }
            );
            
            UpdatePortraitValues(index);
        }
    }

    private void UpdatePortraitValues(int buttonIndex)
    {
        Button thisButton = godPortraits[buttonIndex];
        GodPortrait portraitScript = thisButton.GetComponent<GodPortrait>();
        GodBehaviour thisGod = GameManager.Instance.allPlayerGods[buttonIndex];
        
        // Set the portrait script's god value equal to the player god at this same index
        portraitScript.correspondingGod = thisGod;
        portraitScript.UpdateValues();
    }

    private void UpdateAllPortraits()
    {
        for (int index = 0; index < godPortraits.Count; index++)
        {
            Button currentButton = godPortraits[index];
            
            UpdatePortraitValues(index);
        }
    }

    public void UpdateCurrentGodText()
    {
        if (GameManager.Instance.currentlySelectedGod != null)
        {
            currentGodName.text = GameManager.Instance.currentlySelectedGod.godName;
        }
        else
        {
            currentGodName.text = " ";
        }
    }
    
    public void UpdateRespectText()
    {
        respectCounter.text = String.Format("Respect: {0}", GameManager.Instance.currentRespect);
    }
}
