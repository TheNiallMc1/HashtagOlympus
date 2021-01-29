using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    public List<GameObject> skillNodeList;

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
            
            skillNodeList.Add(skillObject);
        }
    }

    public void UpdateSkillNodes()
    {
        foreach (GameObject skillNode in skillNodeList)
        {
            SkillNode skillScript = skillNode.GetComponent<SkillNode>();
            skillScript.UpdateNodeInfo();
        }
    }
    
    public void UnlockSkills()
    {
        // Find position of skill in list
        // If the skill before this one has been purchased, unlock it
        // If this is the first skill in the list, unlock it

        for (int i = 0; i < skillNodeList.Count; i++)
        {
            Skill thisSkill = skillNodeList[i].GetComponent<SkillNode>().skillInfo;
            
            if (i == 0)
            {
                thisSkill.isLocked = false;
            }
            else
            {
                Skill previousSkill = skillNodeList[i - 1].GetComponent<SkillNode>().skillInfo;
                
                if (previousSkill.isPurchased)
                {
                    thisSkill.isLocked = false;
                }
            }
        }
        
        UpdateSkillNodes();
    }
}