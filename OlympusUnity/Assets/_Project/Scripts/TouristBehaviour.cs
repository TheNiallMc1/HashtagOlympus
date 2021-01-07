using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouristBehaviour : MonoBehaviour
{
    [Header("Character Stats")]
    public int maxHealth;
    protected int currentHealth;
    protected int attackDamage;
    
    [Header("AI Weights")]
    public int attackGodChance;
    public int attackStructureChance;

    [Header("Waypoint Scoring")]
    //public Waypoint currentWaypoint;
    
    public int closestToFinalWaypoint;

    public int godMonument;
    public int touristMonument;
    
    public int godsNearby;
    public int touristsNearby;
}
