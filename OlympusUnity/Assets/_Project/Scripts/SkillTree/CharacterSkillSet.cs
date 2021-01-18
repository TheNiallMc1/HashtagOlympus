using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Set", menuName = "Skill Tree/Character Skill Set", order = 1)]
public class CharacterSkillSet : ScriptableObject
{
    public List<SkillBranch> skillBranches;
}