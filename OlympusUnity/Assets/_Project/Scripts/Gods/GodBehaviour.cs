using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GodBehaviour : MonoBehaviour
{
    // I AM ALIVE

    public string godName;
    protected int indexInGodList;
    
    [Header("Combat Stats")]
    public int maxHealth;
    public int currentHealth;
    
    public int awarenessRadius;
    public int attackRadius;
    public int attackDamage;
    public int armour;
    public int speed;

    public bool usesSpecialResource;
    
    public int costToRespawn;
    public bool isKOed;
    
    [Header("Attacking")]
    [SerializeField] protected internal List<Combatant> enemiesSeen;
    [SerializeField] protected internal List<Combatant> enemiesInAttackRange;
    
    protected Combatant currentAttackTarget;
    protected Coroutine currentAttackCoroutine;

    [Header("States")] 
    [SerializeField] protected internal GodState currentState;

    [HideInInspector] public Vector3 lastClickedPosition;
    
    [Header("Levelling and EXP")]
    protected int currentLevel;
    protected int currentExp;
    protected int expToNextLevel;
    protected int currentSkillPoints;

    [Header("Abilities")]

    //public List<SpecialAbility> passiveAbilities;

    protected Animator anim;
    protected NavMeshAgent navMeshAgent;
    protected MeshRenderer meshRenderer;

    [Header("Detection Colliders")]
    public GameObject mouseDetectorCollider;
    public SphereCollider awarenessRadiusCollider;
    public SphereCollider attackRadiusCollider;

    [Header("UI Elements")] 
    public GodHealthBar healthBar;
    protected UIManager uiManager;
    
    [Header("Testing Variables")]
    public Material standardMaterial;
    public Material selectedMaterial;
    public Material attackMaterial;
    
    public Sprite portraitSprite;
    public Sprite portraitSpriteSelected;

    public PlayerAbilities playerAbilites;

    public virtual void Start()
    {
        playerAbilites = GetComponent<PlayerAbilities>();

        // Give this god a reference to itself in the playerGods list
        for (int i = 0; i < GameManager.Instance.allPlayerGods.Count; i++)
        {
            if (GameManager.Instance.allPlayerGods[i] == this)
            {
                print("god found in player god list");
                indexInGodList = i;
            }
        }
        
        uiManager = FindObjectOfType<UIManager>();
        
        //healthBar = uiManager.healthBars[indexInGodList];
        //healthBar.Initialise();
        
        //healthBar.SetValue(50);
        
        currentHealth = maxHealth;
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        // meshRenderer = GetComponentInChildren<MeshRenderer>();
        
        currentState = GodState.idle;
        
        // Initialise collider radius
        // awarenessRadiusCollider.radius = awarenessRadius;
        // attackRadiusCollider.radius = attackRadius;

        // Get animation parameters
        animator = GetComponentInChildren<Animator>();
    }

    public void FixedUpdate()
    {
        // Booleans used for determining different states
        bool attackRangeEmpty = !enemiesInAttackRange.Any();
        bool awarenessRangeEmpty = !enemiesSeen.Any();
        
        bool movingToEnemy = currentState == GodState.moveToEnemy;
        bool movingToArea = currentState == GodState.moveToArea;
        bool attacking = currentState == GodState.attacking;
        bool isKnockedOut = currentState == GodState.knockedOut;

        bool closeToTargetPosition = navMeshAgent.remainingDistance < 0.1f;
    
        // If there are enemies in awareness range but not attack range, head to the enemy that can be seen
        if (!isKnockedOut && !movingToArea && !movingToEnemy && attackRangeEmpty && !awarenessRangeEmpty)
        {
            SwitchState(GodState.moveToEnemy);
        }
        
        // If there are enemies in attack range, and the god isn't currently moving to an area, attack the enemy
        if (!isKnockedOut && !attacking && !attackRangeEmpty)
        {
            SwitchState(GodState.attacking);
        }
    
        // If the god reaches their target destination, and is not attacking, switch to idle state
        if (!isKnockedOut && currentState != GodState.idle && !attacking && closeToTargetPosition)
        {
            SwitchState(GodState.idle);
        }
        
        // If health reduced to 0, switch to knocked out state
        else if (currentHealth<=0)
        {
            SwitchState((GodState.knockedOut));
        }

        animSpeed = navMeshAgent.velocity.magnitude / navMeshAgent.speed;
        // animSpeed = navMeshAgent.speed;

        animator.SetFloat("Vertical_f", animSpeed);
        if(navMeshAgent.destination != null)
        {
            // animator.SetLookAtPosition(navMeshAgent.destination);
        }
    }
    
    public void ToggleSelection(bool isSelected)
    {
        if (isSelected)
        {
            // meshRenderer.material = selectedMaterial;
            mouseDetectorCollider.SetActive(false);
        }
        
        if (!isSelected)
        {
            // meshRenderer.material = standardMaterial;
            mouseDetectorCollider.SetActive(true);
        }
    }
    
    public void MoveToTarget(Vector3 navDestination)
    {
        navMeshAgent.destination = navDestination;
    }

    public void UpdateAwarenessList(bool addToList, Combatant tourist)
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
    
    public void UpdateAttackList(bool addToList, Combatant tourist)
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
            
            case GodState.knockedOut:
                CancelAutoAttack(); // Cancel any currently running auto attack
                IdleState();
                isKOed = true;
                Debug.Log(godName+" is knocked out!!");
                break;
        }
    }

    private void IdleState()
    {
        // Material for testing
        // meshRenderer.material = standardMaterial;
        
        currentState = GodState.idle;
        print(godName + ": idling");
    }
    
    private void MoveToAreaState()
    {
        // Material for testing
        // meshRenderer.material = standardMaterial;
        
        currentState = GodState.moveToArea;
        MoveToTarget(lastClickedPosition); // Move to the area the player last clicked
        print(godName + ": moving to area");
    }
    
    private void MoveToEnemyState()
    {
        // Material for testing
        // meshRenderer.material = standardMaterial;
        
        currentState = GodState.moveToEnemy;
        MoveToTarget(enemiesSeen[0].transform.position); // Move to the first enemy in the awareness range list
        print(godName + ": moving to enemy");
    }
    
    private void AttackingState()
    {
        // Material for testing
        // meshRenderer.material = attackMaterial;
        
        currentState = GodState.attacking;
        print(godName + ": attacking");
        currentAttackCoroutine = StartCoroutine(AutoAttackCoroutine());
    }
    
    #endregion

    protected IEnumerator AutoAttackCoroutine()
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


        transform.LookAt(currentAttackTarget.transform.position);
        
        int animNumber = randomNumber();

        animator.ResetTrigger("AutoAttack0" + lastNumber);

        animator.SetTrigger("AutoAttack0" + animNumber);

        lastNumber = animNumber;

        yield return new WaitForSecondsRealtime(0.2f);

        // If the current target is now null because it died remove it from the lists
        if (currentAttackTarget == null)
        {
            animator.ResetTrigger("AutoAttack0" + animNumber);

            UpdateAttackList(false, currentAttackTarget);
            UpdateAwarenessList(false, currentAttackTarget);
            // Determine and store a new target if the last one was null 
            currentAttackTarget = enemiesInAttackRange[0];


        }
        else
        {
            yield return new WaitForSecondsRealtime(2.5f);
        }
        
        
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

    protected void CancelAutoAttack()
    {
        // Stop the auto attack coroutine if it exists
        if (currentAttackCoroutine != null)
        {
            StopCoroutine(currentAttackCoroutine);
        }
    }

    public void Revive()
    {
        Debug.Log("Reviving "+godName);
        SwitchState(GodState.idle);
        currentHealth = maxHealth;
        isKOed = false;
    }

    public virtual void TakeDamage(int damageAmount)
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

    protected virtual void Die()
    {
        print("dead");
    }
    
    // may need to be public for ui implementation
    //public void UseAbility(int abilityIndex)
    //{
    //    // .InitiateAbility();
    //}
    
}


public enum GodState
{
    idle,
    moveToArea,
    moveToEnemy,
    attacking,
    knockedOut
}