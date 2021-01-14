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
    
    // Local versions of the SO variables, to avoid fetching them everytime we need them
    private string branchName;
    private List<Skill> skillList;

    public void Start()
    {
        skillList = branchInfo.skillList;
        branchName = branchInfo.branchName;

        branchNameText.text = branchName;
    }
}