using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Animations;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI respectCounter;
    public TextMeshProUGUI currentGodName;
    
    [Header("Abilities")]
    public TextMeshProUGUI abilityInfo;
    public GameObject abilityIconPrefab;
    public GameObject[] abilityPanels;

    [Header("Bars")] 
    public List<GodHealthBar> healthBars;
    public List<GodSpecialBar> specialBars; // Used for special abilities, such as Ares Rage meter
    
    [Header("Portraits")]
    public List<Button> godPortraits;

    public void Start()
    {
        UpdateRespectText();
        UpdateCurrentGodText();
        AssignAbilitiesToUI();

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

        for (int index = 0; index < godPortraits.Count; index++)
        {
            GodSpecialBar thisSpecialBar = specialBars[index];
            
            // If the god at this index doesnt have a special resource, disable the bar
            if (GameManager.Instance.allPlayerGods[index].usesSpecialResource == false)
            {
                thisSpecialBar.gameObject.SetActive(false);
            }
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
        for (int index = 0; index < 3; index++)
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

    public void AssignAbilitiesToUI() // Reads the abilities the currently selected god has and updates the UI accordingly
    {
        GameObject[] abilityIcons;
        
        int numberOfGods = GameManager.Instance.allPlayerGods.Count;
        
        for (int i = 0; i < numberOfGods; i++)
        {
            GameObject thisAbilityPanel = abilityPanels[0];
            GodBehaviour thisGod = GameManager.Instance.allPlayerGods[i];
            print(thisGod.name);

            for (int j = 0; j < thisAbilityPanel.transform.childCount; j++)
            {
                // Loop through each child, assigning the proper ability to them
                GameObject thisIcon = thisAbilityPanel.transform.GetChild(j).gameObject;
                AbilityIcon thisIconScript = thisIcon.GetComponent<AbilityIcon>();
                
                thisIconScript.correspondingGod = thisGod;
                thisIconScript.ability = thisGod.specialAbilities[j];
                
                thisIconScript.InitialiseIcon();
            }
        }
    }
    
    public void UpdateAbilityInfo(SpecialAbility ability)
    {
        abilityInfo.text = String.Format("{0} - {1}", ability.abilityName, ability.abilityDescription);
    }
}
