﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class CharacterDock : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GodBehaviour currentGod;
   // public RespectBuff respectBuff;
    public TMP_Text godNameDisplay;
    public TMP_Text godHealthDisplay;

    public Button reviveButton;
    public Button strButton;

    private void Awake()
    {
        reviveButton.gameObject.SetActive(false);
    }

    public void UpdateCharacterDock()
    {
        godNameDisplay.text = currentGod.godName;
        godHealthDisplay.text = currentGod.currentHealth + "/" + currentGod.maxHealth;

        if (currentGod.isKOed)
        {
            ShowReviveButton();
        }
        
    }

    public void DockSetUp(GodBehaviour assignedGod)
    {
        currentGod = assignedGod;
        godNameDisplay.text = currentGod.godName;
        godHealthDisplay.text = currentGod.currentHealth + "/" + currentGod.maxHealth;
        //adding correct ability buttons
    }
    
    void ShowReviveButton()
    {
        reviveButton.gameObject.SetActive(true);
    }
    
    public void ReviveButton()
    {
        currentGod.Revive();
        reviveButton.gameObject.SetActive(false);
        GameManager.Instance.RemoveRespect(currentGod.costToRespawn);
        Debug.Log("revive pressed");
    }
}
