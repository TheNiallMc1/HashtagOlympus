using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GodBehaviour : MonoBehaviour
{
    public string godName;
   
    public int maxHealth;
    protected int currentHealth;
    
    public int attackDamage;
    public int armour;
    public int speed;

    public int costToRespawn;

    public SpecialAbility[] specialAbilities;
    
    public Sprite portraitSprite;
    public Sprite portraitSpriteSelected;
    
    protected NavMeshAgent navMeshAgent;
    protected MeshRenderer meshRenderer;
    
    public GameObject mouseDetectorCollider;
    
    public Material standardMaterial;
    public Material selectedMaterial;

    public void Awake()
    {
        currentHealth = maxHealth;
        navMeshAgent = GetComponent<NavMeshAgent>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ToggleSelection(bool isSelected)
    {
        if (isSelected)
        {
            meshRenderer.material = selectedMaterial;
            mouseDetectorCollider.SetActive(false);
        }
        
        if (!isSelected)
        {
            meshRenderer.material = standardMaterial;
            mouseDetectorCollider.SetActive(true);
        }
    }
    
    public void MoveToTarget(Vector3 navDestination)
    {
        navMeshAgent.destination = navDestination;
    }
}