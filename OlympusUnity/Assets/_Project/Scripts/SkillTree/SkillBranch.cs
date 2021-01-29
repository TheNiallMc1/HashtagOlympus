using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tree", menuName = "Skill Tree/New Branch", order = 1)]
public class SkillBranch : ScriptableObject
{
    public string branchName;

    public List<Skill> skillList;
}