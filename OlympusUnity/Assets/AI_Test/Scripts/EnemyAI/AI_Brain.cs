using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AI_Movement))]
[RequireComponent(typeof(Combatant))]
public class AI_Brain : MonoBehaviour
{
    AI_Movement movementMotor;

    [SerializeField] protected internal List<Combatant> enemiesInAttackRange;
    [SerializeField] protected internal List<Combatant> monumentsInAttackRange;

    protected Combatant thisCombatant;

    public Combatant currentAttackTarget;
    protected Coroutine currentAttackCoroutine;

    public enum ePriority { Moving, Monument, God }
    public enum eState { idle, Moving, Attacking, Ability }

    [Header("Inscribed")]
    [SerializeField]


    protected float _health;
    protected float _damage = 50;
    protected float _speed;

    [Header("Dynamic")]
    [SerializeField]
    protected ePriority _priority = ePriority.Moving;
    [SerializeField]
    protected eState _state = eState.idle;

    public bool InRange = false;
    public bool initMove = true;
    public bool wieghtCheck = false;

    [SerializeField]
    public Waypoint waypoint;


    public ePriority priority { get { return _priority; } set { _priority = value; } }

   

    public eState state { get { return _state; } set { _state = value; } }
    public float health { get { return _health; } set { _health = value; } }

    private void Awake()
    {
        thisCombatant = GetComponent<Combatant>();
        movementMotor = this.GetComponent<AI_Movement>();
        _state = eState.Moving;
    }

    protected virtual void Start()
    {
        
    }

    private void Update()
    {
        if (monumentsInAttackRange.Count > 0)
        {
            //if (monumentsInAttackRange[0].state == MonumentHealth.eState.Tourist && enemiesInAttackRange.Count == 0)
            //{
            //    _state = eState.Moving;
            //    _priority = ePriority.Moving;
            //    InRange = false;
            //}
        }
        if((enemiesInAttackRange.Count == 0 && monumentsInAttackRange.Count == 0))
        {
            _state = eState.Moving;
            _priority = ePriority.Moving;
            InRange = false;
        }

      
    }

    void FixedUpdate()
    { 
        waypoint = movementMotor.GetPath();
        switch (_priority)
        {
            case ePriority.God:
                if (enemiesInAttackRange[0] != null)
                {
                    currentAttackTarget = enemiesInAttackRange[0];
                }
                break;
            case ePriority.Monument:        
                if (waypoint != null)
                {
                    currentAttackTarget = monumentsInAttackRange[0];
                }
                break;
        }

                
        switch (_state)
        {
            case eState.Moving:
                if (!initMove)
                {
                    CancelAutoAttack();
                    movementMotor.Moving();
                }
                break;
            case eState.Attacking:
                if(_priority == ePriority.God)
                {
                    Attack(currentAttackTarget);
                }
                if(_priority == ePriority.Monument)
                {
                    Attack(currentAttackTarget);
                }
               break;
        }
        targetInRange();
        if (InRange)
        {
            _state = eState.Attacking;
        }

    }

    protected virtual void Attack(Combatant target)
    {
        
        if (currentAttackTarget != null)
        {
            movementMotor.MoveToTarget(target);
            if((transform.position - target.transform.position).magnitude < 5)
            {
                currentAttackCoroutine = StartCoroutine(AttackingCoroutine());
            }
            else
            {
                enemiesInAttackRange.Remove(currentAttackTarget);
            }
        
        }
        
    }

    /*
    Handles AI while it is attacking a monument. 
   */
    protected IEnumerator AttackingCoroutine()
    {
        if(currentAttackTarget.currentHealth <= 0)
        {
            if (_priority == ePriority.God)
            {
                UpdateAttackList(false, currentAttackTarget);
            }
            if (_priority == ePriority.Monument)
            {
                UpdateMonumentList(false, currentAttackTarget);
            }
        }

        transform.LookAt(currentAttackTarget.transform.position);

        yield return new WaitForSecondsRealtime(0.2f);

        // If the current target is now null because it died remove it from the lists
        if (currentAttackTarget == null)
        {
            if (_priority == ePriority.God)
            {
                UpdateAttackList(false, currentAttackTarget);
            }
            if (_priority == ePriority.Monument)
            {
                UpdateMonumentList(false, currentAttackTarget);
            }
        }
        else
        {
            yield return new WaitForSecondsRealtime(2.5f);
        }


        // If any more enemies remain in range, loop the coroutine
        if (enemiesInAttackRange.Any())
        {
            currentAttackCoroutine = StartCoroutine(AttackingCoroutine());
        }
        else
        {
            currentAttackCoroutine = null;
            _state = eState.Moving;
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

    protected void targetInRange()
    {
        if(currentAttackTarget != null)
        {
            if((transform.position - currentAttackTarget.transform.position).magnitude < 10)
            {
                InRange = true;
            }
        }
    }

    /*
    Handles AI while it is targetting a God. 
    */
    protected IEnumerator AttackingGodCoroutine()
    {
        yield break;
    }

    public void UpdateAttackList(bool addToList, Combatant god)
    {
        bool alreadyInList = enemiesInAttackRange.Contains(god);

        // Add tourist if the method is to add from the list, and the tourist is not already in the list
        if (addToList && !alreadyInList)
        {
            if (god.targetType == Combatant.eTargetType.Player)
            {
                enemiesInAttackRange.Add(god);
                this.currentAttackTarget = enemiesInAttackRange[0];
                _priority = ePriority.God;
            }
        }

        // Remove tourist if the method is to remove from the list, and the tourist is already in the list
        if (!addToList && alreadyInList)
        {
            
            //enemiesInAttackRange.Remove(god);
            _priority = ePriority.Moving;
            _state = eState.Moving;
            movementMotor.FindClosestWaypoint(transform.position);
        }
    }
    internal void UpdateMonumentList(bool addToList, Combatant monument)
    {
        bool alreadyInList = monumentsInAttackRange.Contains(monument);

        // Add tourist if the method is to add from the list, and the tourist is not already in the list
        if (addToList && !alreadyInList)
        {
            if (monument.targetType == Combatant.eTargetType.PMonument)
            {
                monumentsInAttackRange.Add(monument);
                this.currentAttackTarget = monumentsInAttackRange[0];
                if (!wieghtCheck)
                {
                    _priority = ePriority.Monument;
                    _state = eState.Attacking;
                }
            }
        }

        // Remove tourist if the method is to remove from the list, and the tourist is already in the list
        if (!addToList && alreadyInList)
        {
            monumentsInAttackRange.Remove(monument);
            _priority = ePriority.Moving;
            _state = eState.Moving;
        }
    }
}
