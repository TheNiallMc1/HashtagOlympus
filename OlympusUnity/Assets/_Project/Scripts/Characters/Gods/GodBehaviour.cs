using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class GodBehaviour : MonoBehaviour
{
    public string godName;
    protected Combatant thisCombatant;

    [Header("Combat Stats")]
    public int awarenessRadius;
    public int attackRadius;

    public int costToRespawn;
   
    [HideInInspector] public bool isKOed;
    public bool attackingLocked;
    [HideInInspector] public bool movementLocked;
    
    bool closeToTargetPosition;

    [Header("Attacking")]
    public List<Combatant> enemiesSeen;
    public List<Combatant> enemiesInAttackRange;
    
    public Combatant currentAttackTarget;

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

    protected int ultimateCharge;
    protected bool usingUltimate;

    protected NavMeshAgent navMeshAgent;

    [Header("Detection Colliders")]
    public GameObject mouseDetectorCollider;
    public SphereCollider awarenessRadiusCollider;
    public SphereCollider attackRadiusCollider;

    [Header("Animations")] 
    public Animator animator;
    private int lastNumber = 1;
    public bool attackAnimationIsPlaying;
    
    private static readonly int AutoAttack01 = Animator.StringToHash("AutoAttack01");
    private static readonly int AutoAttack02 = Animator.StringToHash("AutoAttack02");
    private static readonly int AutoAttack03 = Animator.StringToHash("AutoAttack03");
    private static readonly int AutoAttack04 = Animator.StringToHash("AutoAttack04");

    private readonly List<int> autoAttackAnimations = new List<int>
    {
        AutoAttack01,
        AutoAttack02,
        AutoAttack03,
        AutoAttack04
    };
    

    public virtual void Start()
    {
        thisCombatant = GetComponent<Combatant>();
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

        // // If there are enemies in awareness range but not attack range, head to the enemy that can be seen
        // if (!isKnockedOut && !movingToArea && !movingToEnemy && attackRangeEmpty && !awarenessRangeEmpty && !movementLocked)
        // {
        //     SwitchState(GodState.moveToEnemy);
        // }



        // If the god reaches their target destination, and is not attacking, switch to idle state
        if (!isKnockedOut && currentState != GodState.idle && !attacking && closeToTargetPosition)
        {
            SwitchState(GodState.idle);
        }

        float animSpeed = navMeshAgent.velocity.magnitude / navMeshAgent.speed;

        animator.SetFloat("Vertical_f", animSpeed);
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
            SwitchState(GodState.attacking);
        }

        // Remove tourist if the method is to remove from the list, and the tourist is already in the list
        if (!addToList && alreadyInList)
        {
            enemiesSeen.Remove(tourist);
            
            if (!enemiesSeen.Any())
            {
                SwitchState(GodState.idle);
            }
            
        }
    }
    
    public void UpdateAttackList(bool addToList, Combatant tourist)
    {
        bool alreadyInList = enemiesInAttackRange.Contains(tourist);

        // Add tourist if the method is to add from the list, and the tourist is not already in the list
        if (addToList && !alreadyInList)
        {
            enemiesInAttackRange.Add(tourist);
            SwitchState(GodState.attacking);
        }

        // Remove tourist if the method is to remove from the list, and the tourist is already in the list
        if (!addToList && alreadyInList)
        {
            enemiesInAttackRange.Remove(tourist);
            
            if (!enemiesInAttackRange.Any() && !enemiesSeen.Any())
            {
                SwitchState(GodState.idle);
            }

        }
    }

    protected void Attack()
    {        
        // Determine and store current target
        currentAttackTarget = enemiesSeen[0];
        
        // If the current target is null (usually because it died) remove it from the lists
        if (currentAttackTarget == null)
        {
            UpdateAttackList(false, currentAttackTarget);
            UpdateAwarenessList(false, currentAttackTarget);
            // Determine and store a new target if the last one was null 
            currentAttackTarget = enemiesSeen[0];
        }

        if (!enemiesSeen.Any() && !enemiesInAttackRange.Any())
        {
            SwitchState(GodState.idle);
        }

        var targetPosition = currentAttackTarget.transform.position;
        
        MoveToTarget(targetPosition);
        transform.LookAt(targetPosition);

        if (attackAnimationIsPlaying)
        {
            return;
        }
        
        int animNumber = GetRandomNumber();
        Debug.Log("Anim: " + animNumber);

        attackAnimationIsPlaying = true;
        
        animator.ResetTrigger(autoAttackAnimations[animNumber]);

        animator.SetTrigger(autoAttackAnimations[animNumber]);

        lastNumber = animNumber;

        // If the current target is now null because it died remove it from the lists
        if (currentAttackTarget == null)
        {
            animator.ResetTrigger(autoAttackAnimations[animNumber]);

            UpdateAttackList(false, currentAttackTarget);
            UpdateAwarenessList(false, currentAttackTarget);
            // Determine and store a new target if the last one was null 
            currentAttackTarget = enemiesInAttackRange[0];
        }

        if (!enemiesInAttackRange.Any())
        {
            SwitchState(GodState.idle);
        }
    }

    #region State Behaviours

    public void SwitchState(GodState newState) // Call this and pass in a state to switch states
    {
        switch (newState)
        {
            case GodState.idle:
                IdleState();
                break;

            case GodState.attacking:
                AttackingState();
                break;

            case GodState.moveToArea:
                MoveToAreaState();
                break;

            case GodState.moveToEnemy:
                MoveToEnemyState();
                break;

            case GodState.knockedOut:
                KnockedOutState();
                break;
            
            case GodState.usingAbility:
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
       // currentAttackCoroutine = StartCoroutine(AutoAttackCoroutine());
    }

    #endregion

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

    private int GetRandomNumber()
    {
        int randomNumber = Random.Range(0, autoAttackAnimations.Count - 1);
        if (randomNumber == lastNumber)
        {
            if (randomNumber < autoAttackAnimations.Count - 1)
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