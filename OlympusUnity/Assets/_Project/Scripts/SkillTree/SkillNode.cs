using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Instrumentation;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillNode : MonoBehaviour
{
    // The SO for this skill
    [HideInInspector] public Skill skillInfo;

    public TooltipTrigger tooltipTrigger;
    
    [Header("UI Elements")] 
    public Image skillIconImage;
    public TextMeshProUGUI skillCostText;
    
    // Local versions of the SO variables, to avoid fetching them everytime we need them
    private string skillName;
    private string skillDescription;
    private Sprite skillIcon;
    private int skillCost;
    private bool isUnlocked;
    private bool isPurchased;
    
    public void Start()
    {
        skillName = skillInfo.skillName;
        skillDescription = skillInfo.skillDescription;
        skillIcon = skillInfo.skillIcon;
        skillCost = skillInfo.skillPointsRequired;

        skillIconImage.sprite = skillIcon;
        skillCostText.text = String.Format("{0}", skillCost);

        tooltipTrigger.header = skillName;
        tooltipTrigger.content = skillDescription;
    }
}