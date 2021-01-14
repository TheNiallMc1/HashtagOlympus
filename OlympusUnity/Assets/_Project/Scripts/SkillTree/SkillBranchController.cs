using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillBranchController : MonoBehaviour
{
    // The scriptable object containing all this branch info
    [HideInInspector] public SkillBranch branchInfo;

    [Header("Individual Skills")]
    public GameObject skillNodePrefab;
    
    [Header("UI Elements")] 
    public TextMeshProUGUI branchNameText;
    public GameObject skillsContainer;
    
    // Local versions of the SO variables, to avoid fetching them everytime we need them
    private string branchName;
    private List<Skill> skillList;

    public void Start()
    {
        skillList = branchInfo.skillList;
        branchName = branchInfo.branchName;

        branchNameText.text = branchName;
        
        GenerateSkillNodes();
    }

    public void GenerateSkillNodes()
    {
        foreach (Skill skillInfo in skillList)
        {
            // Instantiate a new skill node and access its script
            GameObject skillObject = Instantiate(skillNodePrefab, skillsContainer.transform);
            SkillNode skillScript = skillObject.GetComponent<SkillNode>();

            // Set the SO reference in this new skill to the Skill asset at this index
            skillScript.skillInfo = skillInfo;
        }
    }
}