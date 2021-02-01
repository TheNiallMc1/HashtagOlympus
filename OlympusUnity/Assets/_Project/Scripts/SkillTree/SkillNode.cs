using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // The SO for this skill
    [HideInInspector] public Skill skillInfo;

    public TooltipTrigger tooltipTrigger;
    public Sprite defaultNodeIcon;
    public Sprite rolloverNodeIcon;
    public Sprite purchasedNodeIcon;
    public Sprite lockedNodeIcon;

    private SkillBranchController skillBranch;
    private SkillsManager skillManager;
    
    [Header("UI Elements")] 
    public Image nodeBackground;
    public Image skillIconImage;
    public TextMeshProUGUI skillCostText;
    public GameObject lockedIcon;
    public GameObject purchasedIcon;
    
    // Local versions of the SO variables, to avoid fetching them everytime we need them
    private string skillName;
    private string skillDescription;
    private Sprite skillIcon;
    private int skillCost;
    private bool isLocked;
    private bool isPurchased;

    public void Start()
    {
        // Instantiate a copy of skill info at start, so we dont directly change the asset
        skillInfo = Instantiate(skillInfo);
        
        skillBranch = GetComponentInParent<SkillBranchController>();
        skillManager = FindObjectOfType<SkillsManager>();
        
        
        UpdateNodeInfo();
    }

    public void UpdateNodeInfo()
    {
        skillName = skillInfo.skillName;
        skillDescription = skillInfo.skillDescription;
        skillIcon = skillInfo.skillIcon;
        skillCost = skillInfo.skillPointsRequired;

        isLocked = skillInfo.isLocked;
        isPurchased = skillInfo.isPurchased;
        
        skillIconImage.sprite = skillIcon;
        skillCostText.text = String.Format("{0}", skillCost);

        if (isLocked)
        {
            lockedIcon.SetActive(true);
            
            GenerateLockedTooltip();

            nodeBackground.sprite = lockedNodeIcon;
        }
        
        else if (isPurchased)
        {
            skillCostText.gameObject.SetActive(false);
            purchasedIcon.SetActive(true);
            
            GeneratePurchasedTooltip();
            
            nodeBackground.sprite = purchasedNodeIcon;
        }
        
        else
        {
            lockedIcon.SetActive(false);
            purchasedIcon.SetActive(false);
            
            skillCostText.gameObject.SetActive(true);

            GenerateDefaultTooltip();
            
            nodeBackground.sprite = defaultNodeIcon;
        }
    }

    public void OnClick()
    {
        // If the ability is locked or purchased, the player should not be able to buy it
        if (!isLocked && !isPurchased)
        {
            PurchaseSkill();
        }
    }

    public void GenerateDefaultTooltip()
    {
        tooltipTrigger.header = skillName;
        tooltipTrigger.content = skillDescription;
    }
    
    public void GenerateLockedTooltip()
    {
        tooltipTrigger.header = $"<color=#c23a3a> LOCKED: {skillName}";
        tooltipTrigger.content = "<color=#c23a3a> Purchase the previous skill in this branch to unlock";
    }

    public void GeneratePurchasedTooltip()
    {
        tooltipTrigger.header = String.Format("<color=#6ba825>{0}", skillName);
        tooltipTrigger.content = String.Format("<color=#6ba825>{0}", skillDescription);
    }
    
    public void PurchaseSkill()
    {
        // If player can afford it, buy the skill
        if (skillCost <= skillManager.skillPoints)
        {
            skillManager.skillPoints -= skillCost;
            skillInfo.isPurchased = true;
            skillBranch.UnlockSkills();
            skillManager.UpdateSkillPointCounter();
            
            string header = String.Format("<color=#6ba825>{0}", skillName);
            string content = String.Format("<color=#6ba825>{0}", skillDescription);
            
            TooltipSystem.Hide();
            TooltipSystem.Show(content, header);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // If it isn't locked and purchased, show the rollover
        if (!isLocked && !isPurchased)
        {
            nodeBackground.sprite = rolloverNodeIcon;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // If it isn't locked and purchased, show the default
        if (!isLocked && !isPurchased)
        {
            nodeBackground.sprite = defaultNodeIcon;
        }
    }
}