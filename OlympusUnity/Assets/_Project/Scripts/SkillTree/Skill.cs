using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New Skill", menuName = "Skill Tree/New Skill", order = 1)]
public class Skill : ScriptableObject
{
    public string skillName;
    public string skillDescription;

    public Sprite skillIcon;

    public bool isLocked;
    public bool isPurchased;

    public int skillPointsRequired;
}