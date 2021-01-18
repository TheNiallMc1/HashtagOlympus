using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillsManager : MonoBehaviour
{
    public int skillPoints;
    public TextMeshProUGUI skillPointsText;
    // Information on which character's skill set this is
    public CharacterSkillSet skillSetInfo;
    
    // The branch list from the SO, stored locally
    private List<SkillBranch> branchList;
    
    public List<GameObject> branchObjList;

    public GameObject skillBranchPrefab;
    public GameObject tooltipObj;

    [Header("UI Elements")] 
    public GameObject skillBranchContainer;

    public void Start()
    {
        branchList = skillSetInfo.skillBranches;
        
        GenerateBranches();
    }

    public void GenerateBranches()
    {
        foreach (SkillBranch branchInfo in branchList)
        {
            // Instantiate a new skill branch and access its script
            GameObject branchObject = Instantiate(skillBranchPrefab, skillBranchContainer.transform);
            SkillBranchController branchScript = branchObject.GetComponent<SkillBranchController>();
            

            // Set the SO reference in this new branch to the SkillBranch asset at this index
            branchScript.branchInfo = branchInfo;
            
            branchObjList.Add(branchObject);
        }
    }

    public void UpdateSkills()
    {
        foreach (GameObject branch in branchObjList)
        {
            SkillBranchController branchScript = branch.GetComponent<SkillBranchController>();
            branchScript.UpdateSkillNodes();
        }
    }
    
    
    public void UpdateSkillPointCounter()
    {
        skillPointsText.text = String.Format("{0} Skill Points", skillPoints);
    }
}