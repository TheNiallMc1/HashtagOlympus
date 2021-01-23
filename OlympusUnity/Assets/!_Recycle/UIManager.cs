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

    [Header("Portraits")]
    public List<Button> godPortraits;

    public void Start()
    {
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
        GodBehaviour thisGod = GameManager.Instance.allPlayerGods[buttonIndex];
    }

    private void UpdateAllPortraits()
    {
        for (int index = 0; index < 3; index++)
        {
            Button currentButton = godPortraits[index];

            UpdatePortraitValues(index);
        }
    }
    
    public void UpdateAbilityInfo(SpecialAbility ability)
    {
        abilityInfo.text = String.Format("{0} - {1}", ability.abilityName, ability.abilityDescription);
    }
}
