using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityIcon : MonoBehaviour
{
    public UIManager uiManager;
    public TempAbilityClass ability;
    public GodBehaviour correspondingGod;
    public string abilityName;
    public string abilityDescription;

    public TextMeshProUGUI abilityInfoText;

    public void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    public void InitialiseIcon()
    {
        abilityName = ability.abilityName;
        abilityDescription = ability.abilityDescription;
    }
    
    public void ShowAbilityInfo()
    {
        abilityInfoText.text = String.Format("{0} - {1}", abilityName, abilityDescription);
    }
    
    public void HideAbilityInfo()
    {
        abilityInfoText.text = "";
    }
    
    
    // Set icon sprite
}
