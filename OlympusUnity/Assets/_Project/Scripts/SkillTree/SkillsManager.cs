using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsManager : MonoBehaviour
{
    // Information on which character's skill set this is
    public CharacterSkillSet skillSetInfo;
    
    // The branch list from the SO, stored locally
    private List<SkillBranch> branchList;

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
        }
    }

    public void CreateTooltip()
    {
    }

    public void HideTooltip()
    {
        
    }
}