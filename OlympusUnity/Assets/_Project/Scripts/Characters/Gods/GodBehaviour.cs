using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using cakeslice;

public class GodBehaviour : MonoBehaviour
{
    public string godName;
    protected Combatant thisCombatant;

    [Header("Combat Stats")]
    public int awarenessRadius;
    public int attackRadius;

    public int costToRespawn;
   
    [HideInInspector] public bool isKOed;

    [Header("Attacking")]
    public List<Combatant> enemiesSeen;
    public List<Combatant> enemiesInAttackRange;
    
    public Combatant currentAttackTarget;

    [Header("States")]
    [SerializeField] protected internal GodState currentState;

    [HideInInspector] public Vector3 lastClickedPosition;

    [Header("Abilities")]
    public List<AbilityManager> specialAbilities;

    [Header("Ultimate")] 
    public string ultimateName;

    public float ultimateGainTickInterval; // How often Ultimate Charge is gained
    public int ultimateGainPerTick; // How much Ultimate Charge is gained per tick
    protected int ultimateCharge;
    public TextMeshProUGUI ultimateChargeText;
    
    protected string ultimateStartAnimTrigger;
    protected string ultimateFinishAnimTrigger;

    public float ultimateDurationTickInterval; // How often Ultimate Charge is lost while Ultimate active
    public int ultimateDecreasePerTick; // How much Ultimate Charge is decreased per tick

    private NavMeshAgent navMeshAgent;

    [Header("Detection Colliders")]
    public GameObject mouseDetectorCollider;
    public SphereCollider awarenessRadiusCollider;
    public SphereCollider attackRadiusCollider;
    public GameObject selectionCircle;

    [Header("Animations")] 
    [HideInInspector] public Animator animator;
    private int lastNumber = 1;
    [HideInInspector] public bool attackAnimationIsPlaying;
    
    private static readonly int AutoAttack01 = Animator.StringToHash("AutoAttack01");
    private static readonly int AutoAttack02 = Animator.StringToHash("AutoAttack02");
    private static readonly int AutoAttack03 = Animator.StringToHash("AutoAttack03");
    private static readonly int AutoAttack04 = Animator.StringToHash("AutoAttack04");
    
    private static readonly int VerticalF = Animator.StringToHash("Vertical_f");

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

    StartCoroutine(GainUltimateChargeCoroutine());

    }

    public void Update()
    {
        float animSpeed = navMeshAgent.velocity.magnitude / navMeshAgent.speed;
        
        animator.SetFloat(VerticalF, animSpeed);
    }
    
    public void ToggleOutlineOnOff(bool shouldTurnOn)
    {
        var allObjects = gameObject.GetComponentsInChildren<Transform>();
        foreach (var child in allObjects)
        {
            if (child.GetComponent<Outline>() !=null)
            {
                child.GetComponent<Outline>().enabled = shouldTurnOn;
            }
        }
    }

    public virtual void OnDamageEvent(int damageTaken)
    {
        // Override if needed
    }

    public void OnDeathEvent()
    {
        // Call base and override if needed
        SwitchState(GodState.knockedOut);
        animator.Play("Die");
    }
    
    public virtual void FixedUpdate()
    {
        if ( CanAttack() )
        {
            Attack();
        }

        if ( CanIdle() )
        {
            SwitchState(GodState.idle);
        }
    }

    public void ToggleSelection(bool isSelected)
    {
        if (!isSelected)
        {
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

    private void Attack()
    {        
        SwitchState(GodState.attacking);
        
        // Determine and store current target
        currentAttackTarget = enemiesSeen[0];
        
        // If the current target is null (usually because it died) remove it from the lists
        if (!currentAttackTarget.gameObject.activeInHierarchy)
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
        

        if (attackAnimationIsPlaying)
        {
            return;
        }

        transform.LookAt(targetPosition);

        int animNumber = GetRandomNumber();
        
        attackAnimationIsPlaying = true;
        
        animator.ResetTrigger(autoAttackAnimations[animNumber]);

        animator.SetTrigger(autoAttackAnimations[animNumber]);

        lastNumber = animNumber;

        // If the current target is now null because it died remove it from the lists
        if (!currentAttackTarget.gameObject.activeInHierarchy)
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
                UsingAbilityState();
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
    }

    private void UsingAbilityState()
    {
        // Override in subclass
    }
    
    private void MoveToAreaState()
    {
        if (CanMoveToArea())
        {
            currentState = GodState.moveToArea;
            MoveToTarget(lastClickedPosition); // Move to the area the player last clicked
        }
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
    
    // CHECKS FOR ENTERING STATES \\

    private bool CanMoveToArea()
    {
        bool usingAbility = currentState == GodState.usingAbility;
        bool knockedOut = currentState == GodState.knockedOut;

        return !usingAbility && !knockedOut;
    }
    
    private bool CanAttack()
    {
        bool usingUltimate = currentState == GodState.usingUltimate;
        bool usingAbility = currentState == GodState.usingAbility;
        bool knockedOut = currentState == GodState.knockedOut;

        bool movingToArea = currentState == GodState.moveToArea;
        
        bool attackListEmpty = !enemiesInAttackRange.Any();
        bool awarenessListEmpty = !enemiesSeen.Any();

        bool eitherListValid = !attackListEmpty || !awarenessListEmpty;
        
        return !knockedOut && !usingUltimate && !usingAbility && eitherListValid && !movingToArea; 
    }

    private bool CanIdle()
    {
        bool usingUltimate = currentState == GodState.usingUltimate;
        bool usingAbility = currentState == GodState.usingAbility;
        bool knockedOut = currentState == GodState.knockedOut;
        bool attacking = currentState == GodState.attacking;
        bool moving = currentState == GodState.moveToArea || currentState == GodState.moveToEnemy;

        return !moving && !attacking && !knockedOut && !usingAbility && !usingUltimate;
    }
    
    protected bool CanUseAbility()
    {
        bool usingUltimate = currentState == GodState.usingUltimate;
        bool usingAbility = currentState == GodState.usingAbility;
        bool knockedOut = currentState == GodState.knockedOut;
        
        return !knockedOut && !usingUltimate && !usingAbility;
    }

    #endregion

    public void Revive()
    {
        SwitchState(GodState.idle);
        thisCombatant.currentHealth = thisCombatant.maxHealth;
        isKOed = false;
    }

    // may need to be public for ui implementation
    public void UseAbility(int abilityIndex)
    {
        if ( CanUseAbility() )
        {
            specialAbilities[abilityIndex].EnterTargetSelectMode();
            currentState = GodState.usingAbility;
        }
    }

    public virtual void ActivateUltimate()
    {
        // Override in sub class
    }

    protected virtual void EndUltimate()
    {
        // Override in sub class if needed
        ultimateCharge = 0; // Just adjusting in case it falls below zero somehow
        ultimateChargeText.text = ultimateCharge.ToString();

        currentState = GodState.idle;

        StartCoroutine(GainUltimateChargeCoroutine());
    }

    private IEnumerator GainUltimateChargeCoroutine()
    {
        // Gain charge every tick
        yield return new WaitForSecondsRealtime(ultimateGainTickInterval);
        ultimateCharge += ultimateGainPerTick;
        ultimateChargeText.text = ultimateCharge.ToString();

        // If less than 100, keep gaining. If 100 or over, stop.
        if (ultimateCharge < 100)
        {
            StartCoroutine(GainUltimateChargeCoroutine());
        }
        else
        {
            ultimateChargeText.text = ultimateName;
        }
    }

    protected IEnumerator UltimateDurationCoroutine()
    {
        yield return new WaitForSecondsRealtime(ultimateDurationTickInterval);
        ultimateCharge -= ultimateDecreasePerTick;
        ultimateChargeText.text = ultimateCharge.ToString();
        
        // When ultimate hits zero, end the Ultimate
        if (ultimateCharge <= 0)
        {
            EndUltimate();
        }
        else
        {
            StartCoroutine(UltimateDurationCoroutine());
        }
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
    usingAbility,
    usingUltimate
}