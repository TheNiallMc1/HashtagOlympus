using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillNode : MonoBehaviour
{
    // The SO for this skill
    [HideInInspector] public Skill skillInfo;

    [Header("UI Elements")] 
    public Image skillIconImage;
    public TextMeshProUGUI skillCostText;
    
    // Local versions of the SO variables, to avoid fetching them everytime we need them
    private string skillName;
    private string skillDescription;
    private Sprite skillIcon;
    private int skillCost;
    
    public void Start()
    {
        skillName = skillInfo.skillName;
        skillDescription = skillInfo.skillDescription;
        skillIcon = skillInfo.skillIcon;
        skillCost = skillInfo.skillPointsRequired;

        skillIconImage.sprite = skillIcon;
        skillCostText.text = String.Format("{0}", skillCost);
    }

}