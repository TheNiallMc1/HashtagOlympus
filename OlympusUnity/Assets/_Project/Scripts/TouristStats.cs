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
    public CurrentState currentState;
    
    [Tooltip("Chance of tourist switching from attacking monument to attacking gods")]
    [Range(0, 100)] public int attackGodChance;
    
    [Tooltip("Chance of tourist attacking a god monument when they reach one")]
    [Range(0, 100)] public int attackStructureChance;

    [Header("Waypoint Scoring")]
    //public Waypoint currentWaypoint;
    
    public int closestToFinalWaypoint;

    public int godMonument;
    public int touristMonument;
    
    public int godsNearby;
    public int touristsNearby;

    public void Awake()
    {
        Initialise();
    }
    
    public void Initialise()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(int damageAmount)
    {
        int newHealth = currentHealth -= damageAmount;
        
        if (newHealth <= 0)
        {
            Die();
        }
        
        else
        {
            currentHealth = newHealth;
            print(name + " took " + damageAmount + " damage");
        }
    }

    public void Die()
    {
        print(name + " died");
        Destroy(gameObject);
    }
}

public enum CurrentState
{
    idle,
    walking,
    attacking
}
