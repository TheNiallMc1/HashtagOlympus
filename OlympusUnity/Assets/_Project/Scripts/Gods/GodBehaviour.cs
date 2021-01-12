using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GodBehaviour : MonoBehaviour
{
    public string godName;
   
    [Header("Combat Stats")]
    public int maxHealth;
    protected int currentHealth;
    
    public int awarenessRadius;
    public int attackRadius;
    public int attackDamage;
    public int armour;
    public int speed;

    public int costToRespawn;
    
    [Header("Attacking")]
    [SerializeField] protected List<TouristBehaviour> enemiesSeen;
    [SerializeField] protected List<TouristBehaviour> enemiesInAttackRange;
    
    protected TouristBehaviour currentAttackTarget;
    protected Coroutine currentAttackCoroutine;
    
    [SerializeField] protected GodState currentState;

    [HideInInspector] public Vector3 lastClickedPosition;
    
    [Header("Levelling and EXP")]
    protected int currentLevel;
    protected int currentExp;
    protected int expToNextLevel;
    protected int currentSkillPoints;

    [Header("Abilities")]
    public SpecialAbility[] specialAbilities;
    public SpecialAbility[] passiveAbilities;
    
    protected NavMeshAgent navMeshAgent;
    protected MeshRenderer meshRenderer;

    [Header("Detection Colliders")]
    public GameObject mouseDetectorCollider;
    public SphereCollider awarenessRadiusCollider;
    public SphereCollider attackRadiusCollider;
    
    [Header("Testing Variables")]
    public Material standardMaterial;
    public Material selectedMaterial;
    public Material attackMaterial;
    
    public Sprite portraitSprite;
    public Sprite portraitSpriteSelected;

    public void Awake()
    {
        currentHealth = maxHealth;
        navMeshAgent = GetComponent<NavMeshAgent>();
        meshRenderer = GetComponent<MeshRenderer>();

        currentState = GodState.idle;
        
        // Initialise collider radius
        awarenessRadiusCollider.radius = awarenessRadius;
        attackRadiusCollider.radius = attackRadius;
    }

    public void FixedUpdate()
    {
        // Booleans used for determining different states
        bool attackRangeEmpty = !enemiesInAttackRange.Any();
        bool awarenessRangeEmpty = !enemiesSeen.Any();
        
        bool movingToEnemy = currentState == GodState.moveToEnemy;
        bool movingToArea = currentState == GodState.moveToArea;
        bool attacking = currentState == GodState.attacking;

        bool closeToTargetPosition = navMeshAgent.remainingDistance < 0.1f;

        // If there are enemies in awareness range but not attack range, head to the enemy that can be seen
        if (!movingToArea && !movingToEnemy && attackRangeEmpty && !awarenessRangeEmpty)
        {
            SwitchState(GodState.moveToEnemy);
        }
        
        // If there are enemies in attack range, and the god isn't currently moving to an area, attack the enemy
        if (!attacking && !attackRangeEmpty)
        {
            SwitchState(GodState.attacking);
        }

        // If the god reaches their target destination, and is not attacking, switch to idle state
        else if (currentState != GodState.idle && !attacking && closeToTargetPosition)
        {
            SwitchState(GodState.idle);
        }
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

    public void UpdateAwarenessList(bool addToList, TouristBehaviour tourist)
    {
        bool alreadyInList = enemiesSeen.Contains(tourist);

        // Add tourist if the method is to add from the list, and the tourist is not already in the list
        if (addToList && !alreadyInList)
        {
            enemiesSeen.Add(tourist);
        }

        // Remove tourist if the method is to remove from the list, and the tourist is already in the list
        if (!addToList && alreadyInList)
        {
            enemiesSeen.Remove(tourist);
        }
    }
    
    public void UpdateAttackList(bool addToList, TouristBehaviour tourist)
    {
        bool alreadyInList = enemiesInAttackRange.Contains(tourist);

        // Add tourist if the method is to add from the list, and the tourist is not already in the list
        if (addToList && !alreadyInList)
        {
            enemiesInAttackRange.Add(tourist);
        }

        // Remove tourist if the method is to remove from the list, and the tourist is already in the list
        if (!addToList && alreadyInList)
        {
            enemiesInAttackRange.Remove(tourist);
        }
    }


    #region State Behaviours
    
    public void SwitchState(GodState newState) // Call this and pass in a state to switch states
    {
        switch (newState)
        {
            case GodState.idle:
                CancelAutoAttack(); // Cancel any currently running auto attack
                IdleState();
                break;
            
            case GodState.attacking:
                AttackingState();
                break;
            
            case GodState.moveToArea:
                CancelAutoAttack();
                MoveToAreaState();
                break;
            
            case GodState.moveToEnemy:
                CancelAutoAttack();
                MoveToEnemyState();
                break;
        }
    }

    private void IdleState()
    {
        // Material for testing
        meshRenderer.material = standardMaterial;
        
        currentState = GodState.idle;
        print(godName + ": idling");
    }

    private void MoveToAreaState()
    {
        // Material for testing
        meshRenderer.material = standardMaterial;
        
        currentState = GodState.moveToArea;
        MoveToTarget(lastClickedPosition); // Move to the area the player last clicked
        print(godName + ": moving to area");
    }

    private void MoveToEnemyState()
    {
        // Material for testing
        meshRenderer.material = standardMaterial;
        
        currentState = GodState.moveToEnemy;
        MoveToTarget(enemiesSeen[0].transform.position); // Move to the first enemy in the awareness range list
        print(godName + ": moving to enemy");
    }

    private void AttackingState()
    {
        // Material for testing
        meshRenderer.material = attackMaterial;
        
        currentState = GodState.attacking;
        print(godName + ": attacking");
        currentAttackCoroutine = StartCoroutine(AutoAttackCoroutine());
    }
    
    #endregion

    private IEnumerator AutoAttackCoroutine()
    {        
        // Determine and store current target
        currentAttackTarget = enemiesInAttackRange[0];
        
        // If the current target is null (usually because it died) remove it from the lists
        if (currentAttackTarget == null)
        {
            UpdateAttackList(false, currentAttackTarget);
            UpdateAwarenessList(false, currentAttackTarget);
            // Determine and store a new target if the last one was null 
            currentAttackTarget = enemiesInAttackRange[0];
        }
        
        currentAttackTarget.TakeDamage(attackDamage);
        
        // If the current target is now null because it died remove it from the lists
        if (currentAttackTarget == null)
        {
            UpdateAttackList(false, currentAttackTarget);
            UpdateAwarenessList(false, currentAttackTarget);
            // Determine and store a new target if the last one was null 
            currentAttackTarget = enemiesInAttackRange[0];
        }
        
        yield return new WaitForSecondsRealtime(2f);
        
        // If any more enemies remain in range, loop the coroutine
        if (enemiesInAttackRange.Any())
        {
            currentAttackCoroutine = StartCoroutine(AutoAttackCoroutine());
        }
        else
        {
            currentAttackCoroutine = null;
            SwitchState(GodState.idle);
            yield break; // If there are no enemies left, end the coroutine
        }
        
    }

    private void CancelAutoAttack()
    {
        // Stop the auto attack coroutine if it exists
        if (currentAttackCoroutine != null)
        {
            StopCoroutine(currentAttackCoroutine);
        }
    }
}

public enum GodState
{
    idle,
    moveToArea,
    moveToEnemy,
    attacking
}