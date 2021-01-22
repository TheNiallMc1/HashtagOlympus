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
    protected Combatant thisCombatant;

    [Header("Combat Stats")]
    public int awarenessRadius;
    public int attackRadius;

    public int costToRespawn;
    public bool isKOed;

    public bool attackingLocked;
    public bool movementLocked;
    bool closeToTargetPosition;

    [Header("Attacking")]
    [SerializeField] protected internal List<Combatant> enemiesSeen;
    [SerializeField] protected internal List<Combatant> enemiesInAttackRange;
    
    public Combatant currentAttackTarget;
   // protected Coroutine currentAttackCoroutine;

    [Header("States")]
    [SerializeField] protected internal GodState currentState;

    [HideInInspector] public Vector3 lastClickedPosition;

    [Header("Levelling and EXP")]
    protected int currentLevel;
    protected int currentExp;
    protected int expToNextLevel;
    protected int currentSkillPoints;

    [Header("Abilities")]

    public List<AbilityManager> specialAbilities;
    //public List<SpecialAbility> passiveAbilities;

    protected int ultimateCharge; // current ultimate charge percentage
    protected bool usingUltimate;

    protected NavMeshAgent navMeshAgent;

    [Header("Detection Colliders")]
    public GameObject mouseDetectorCollider;
    public SphereCollider awarenessRadiusCollider;
    public SphereCollider attackRadiusCollider;

    [Header("UI Elements")]
    public GodHealthBar healthBar;
    protected UIManager uiManager;

    public Animator animator;
    private int lastNumber = 1;
    public bool attackAnimationIsPlaying = false;

    public virtual void Start()
    {
        thisCombatant = GetComponent<Combatant>();

        uiManager = FindObjectOfType<UIManager>();

        navMeshAgent = GetComponent<NavMeshAgent>();

        currentState = GodState.idle;

        // Initialise collider radius
        awarenessRadiusCollider.radius = awarenessRadius;
        attackRadiusCollider.radius = attackRadius;

        animator = GetComponentInChildren<Animator>();
    }

    public virtual void OnDamageEvent(int damageTaken)
    {
        // Override if needed
    }

    public virtual void OnDeathEvent()
    {
        // Call base and override if needed
        SwitchState(GodState.knockedOut);
        animator.Play("Die");
    }
    
    public virtual void FixedUpdate()
    {
        // Booleans used for determining different states
        bool attackRangeEmpty = !enemiesInAttackRange.Any();
        bool awarenessRangeEmpty = !enemiesSeen.Any();

        bool movingToEnemy = currentState == GodState.moveToEnemy;
        bool movingToArea = currentState == GodState.moveToArea;
        bool attacking = currentState == GodState.attacking;
        bool isKnockedOut = currentState == GodState.knockedOut;

        if (movingToArea || movingToEnemy)
        {
            closeToTargetPosition = navMeshAgent.remainingDistance < 0.1f;
        }
        if (attacking && !attackAnimationIsPlaying)
        {
            Attack();
        }

        // If there are enemies in awareness range but not attack range, head to the enemy that can be seen
        if (!isKnockedOut && !movingToArea && !movingToEnemy && attackRangeEmpty && !awarenessRangeEmpty && !movementLocked)
        {
            SwitchState(GodState.moveToEnemy);
        }

        // If there are enemies in attack range, and the god isn't currently moving to an area, attack the enemy
        if (!isKnockedOut && !attacking && !attackRangeEmpty && !attackingLocked && !movingToArea)
        {
            SwitchState(GodState.attacking);
        }

        // If the god reaches their target destination, and is not attacking, switch to idle state
        if (!isKnockedOut && currentState != GodState.idle && !attacking && closeToTargetPosition)
        {
            SwitchState(GodState.idle);
        }

        float animSpeed = navMeshAgent.velocity.magnitude / navMeshAgent.speed;
        // animSpeed = navMeshAgent.speed;

        animator.SetFloat("Vertical_f", animSpeed);
        
        //if (!closeToTargetPosition)
        //{
        //    animator.SetLookAtPosition(navMeshAgent.destination);
        //}
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

    private void MoveToTarget(Vector3 navDestination)
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

    protected void Attack()
    {
        Debug.Log("Ares - Attack Called");
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

        //yield return new WaitForSecondsRealtime(0.2f);

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
           // yield return new WaitForSecondsRealtime(2.5f);
        }


        // If any more enemies remain in range, loop the coroutine
        if (enemiesInAttackRange.Any())
        {
          //  currentAttackCoroutine = StartCoroutine(AutoAttackCoroutine());
        }
        else
        {
          //  currentAttackCoroutine = null;
            SwitchState(GodState.idle);
        }

    }

    #region State Behaviours

    public void SwitchState(GodState newState) // Call this and pass in a state to switch states
    {
        switch (newState)
        {
            case GodState.idle:
                CancelAutoAttack();
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
                CancelAutoAttack();
                KnockedOutState();
                break;
            
            case GodState.usingAbility:
                CancelAutoAttack();
                UsingAbility();
                break;
        }
    }

    private void IdleState()
    {
        currentState = GodState.idle;
    }

    private void KnockedOutState()
    {
        currentState = GodState.knockedOut;
        isKOed = true;
        attackingLocked = true;
        movementLocked = true;
    }

    protected virtual void UsingAbility()
    {
        // Override in subclass
    }
    
    private void MoveToAreaState()
    {
        currentState = GodState.moveToArea;
        MoveToTarget(lastClickedPosition); // Move to the area the player last clicked
    }

    private void MoveToEnemyState()
    {
        currentState = GodState.moveToEnemy;

        if(enemiesSeen[0] != null)
        {
            MoveToTarget(enemiesSeen[0].transform.position); // Move to the first enemy in the awareness range list
        }
        else
        {
            currentState = GodState.idle;
        }
    }

    private void AttackingState()
    {
        currentState = GodState.attacking;
        print(godName + ": attacking");
       // currentAttackCoroutine = StartCoroutine(AutoAttackCoroutine());
    }

    #endregion

    //protected IEnumerator AutoAttackCoroutine()
    //{
    //    // Determine and store current target
    //    currentAttackTarget = enemiesInAttackRange[0];

    //    // If the current target is null (usually because it died) remove it from the lists
    //    if (currentAttackTarget == null)
    //    {
    //        UpdateAttackList(false, currentAttackTarget);
    //        UpdateAwarenessList(false, currentAttackTarget);
    //        // Determine and store a new target if the last one was null 
    //        currentAttackTarget = enemiesInAttackRange[0];
    //    }

    //    transform.LookAt(currentAttackTarget.transform.position);

    //    int animNumber = randomNumber();

    //    animator.ResetTrigger("AutoAttack0" + lastNumber);

    //    animator.SetTrigger("AutoAttack0" + animNumber);

    //    lastNumber = animNumber;

    //    yield return new WaitForSecondsRealtime(0.2f);

    //    // If the current target is now null because it died remove it from the lists
    //    if (currentAttackTarget == null)
    //    {
    //        animator.ResetTrigger("AutoAttack0" + animNumber);

    //        UpdateAttackList(false, currentAttackTarget);
    //        UpdateAwarenessList(false, currentAttackTarget);
    //        // Determine and store a new target if the last one was null 
    //        currentAttackTarget = enemiesInAttackRange[0];
    //    }
    //    else
    //    {
    //        yield return new WaitForSecondsRealtime(2.5f);
    //    }


    //    // If any more enemies remain in range, loop the coroutine
    //    if (enemiesInAttackRange.Any())
    //    {
    //        currentAttackCoroutine = StartCoroutine(AutoAttackCoroutine());
    //    }
    //    else
    //    {
    //        currentAttackCoroutine = null;
    //        SwitchState(GodState.idle);
    //    }

    //}

    protected void CancelAutoAttack()
    {
        //// Stop the auto attack coroutine if it exists
        //if (currentAttackCoroutine != null)
        //{
        //    StopCoroutine(currentAttackCoroutine);
        //}
    }

    public void Revive()
    {
        Debug.Log("Reviving " + godName);
        SwitchState(GodState.idle);
        thisCombatant.currentHealth = thisCombatant.maxHealth;
        isKOed = false;
    }

    // may need to be public for ui implementation
    public void UseAbility(int abilityIndex)
    {
        specialAbilities[abilityIndex].EnterTargetSelectMode();
    }

    public virtual void ActivateUltimate()
    {
        // Override in sub class
    }

    public virtual IEnumerator UltimateDurationCoroutine()
    {
        yield return null;
        // Override in sub class
    }

    private int randomNumber()
    {
        int randomNumber = UnityEngine.Random.Range(1, 4);
        if (randomNumber == lastNumber)
        {
            if (randomNumber < 4)
            {
                randomNumber++;
            }
            else
            {
                randomNumber--;
            }
        }
        return randomNumber;
    }

}

public enum GodState
{
    idle,
    moveToArea,
    moveToEnemy,
    attacking,
    knockedOut,
    usingAbility
}