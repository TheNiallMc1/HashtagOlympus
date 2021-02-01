using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterToolTipInfo : MonoBehaviour
{

    [Header("Auto Attack")]
    public string autoAttackHeader = "Auto Attack";
    public string autoAttackContent = "...";
    
    [Header("Abiity 1")]
    public string ability1Header = "Ability 1";
    public string ability1Content = "...";
    
    [Header("Ability2")]
    public string ability2Header = "Ability 2";
    public string ability2Content = "...";
    
    [Header("Passive")]
    public string passiveHeader = "Passive";
    public string passiveContent = "...";
    
    [Header("Ultimate")]
    public string ultimateHeader = "Ultimate";
    public string ultimateContent = "...";

    [SerializeField]
    public List<string> allTooltips;

    private void Awake()
    {
       allTooltips.Add(autoAttackHeader);
       allTooltips.Add(autoAttackContent);
       
       allTooltips.Add(ability1Header);
       allTooltips.Add(ability1Content);
       
       allTooltips.Add(ability2Header);
       allTooltips.Add(ability2Content);
       
       allTooltips.Add(passiveHeader);
       allTooltips.Add(passiveContent);
       
       allTooltips.Add(ultimateHeader);
       allTooltips.Add(ultimateContent);
    }
}
