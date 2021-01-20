using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AI_Movement))]
public class AI_Brain : MonoBehaviour
{
    AI_Movement movementMotor;

    [SerializeField] protected internal List<GodBehaviour> enemiesInAttackRange;
    [SerializeField] protected internal List<MonumentHealth> monumentsInAttackRange;

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

    protected GodBehaviour god;
    protected MonumentHealth monument;
    //MonumentHealth monument;
    Waypoint waypoint;


    public ePriority priority { get { return _priority; } set { _priority = value; } }

   

    public eState state { get { return _state; } set { _state = value; } }
    public float health { get { return _health; } set { _health = value; } }

    private void Awake()
    {
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
            if (monumentsInAttackRange[0].state == MonumentHealth.eState.Tourist && enemiesInAttackRange.Count == 0)
            {
                _state = eState.Moving;
                _priority = ePriority.Moving;
                InRange = false;
            }
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
                    god = enemiesInAttackRange[0];
                }
                break;
            case ePriority.Monument:        
                if (waypoint != null)
                {
                    monument = waypoint.transform.parent.GetComponent<MonumentHealth>();
                }
                break;
        }

                
        switch (_state)
        {
            case eState.Moving:
                if (!initMove)
                {
                   // movementMotor.Moving();
                }
                break;
            case eState.Attacking:
                if(_priority == ePriority.God)
                {
                    Attack(god.gameObject);
                }
                if(_priority == ePriority.Monument)
                {
                    Attack(monument.gameObject);
                }
               break;
        }
        targetInRange();
        if (InRange)
        {
            _state = eState.Attacking;
        }

    }

    protected virtual void Attack(GameObject target)
    {
        
        if (god != null)
        {
            movementMotor.MoveToTarget(target);
            if((transform.position - target.transform.position).magnitude < 5)
            {
                //StartCoroutine(AttackingCoroutine);
            }
            else
            {
                enemiesInAttackRange.Remove(god);
            }
        
        }
        
    }

    /*
    Handles AI while it is attacking a monument. 
   */
    //protected IEnumerator AttackingCoroutine()
    //{
    //    while (god != null || _state == eState.Attacking)
    //    {

    //        if (god == null || monument.Health <= 0)
    //        {
    //            // yield return new WaitForSeconds(10);
    //            if (god != null) 
    //            { 
    //                //attackTarget.RemoveObject();
    //            }

    //        }
    //        else
    //        {
    //          //  monument.Health = monument.Health - _damage;
    //        }

    //        yield return new WaitForSeconds(5);

    //    }
    //    yield break;
    //}
    protected void targetInRange()
    {
        if(god != null)
        {
            if((transform.position - god.transform.position).magnitude < 10)
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

    public void UpdateAttackList(bool addToList, GodBehaviour god)
    {
        bool alreadyInList = enemiesInAttackRange.Contains(god);

        // Add tourist if the method is to add from the list, and the tourist is not already in the list
        if (addToList && !alreadyInList)
        {
            enemiesInAttackRange.Add(god);
            this.god = enemiesInAttackRange[0];
            _priority = ePriority.God;
        }

        // Remove tourist if the method is to remove from the list, and the tourist is already in the list
        if (!addToList && alreadyInList)
        {
            
            //enemiesInAttackRange.Remove(god);
            _priority = AI_Brain.ePriority.Moving;
            _state = AI_Brain.eState.Moving;
            //  movementMotor.test = 0;
            //   movementMotor.FindClosestWaypoint(transform.position);
        }
    }
    internal void UpdateMonumentList(bool addToList, MonumentHealth monument)
    {
        bool alreadyInList = monumentsInAttackRange.Contains(monument);

        // Add tourist if the method is to add from the list, and the tourist is not already in the list
        if (addToList && !alreadyInList)
        {
            monumentsInAttackRange.Add(monument);
            this.monument = monumentsInAttackRange[0];
            if (!wieghtCheck)
            {
                _priority = ePriority.Monument;
                _state = eState.Attacking;
            }
        }

        // Remove tourist if the method is to remove from the list, and the tourist is already in the list
        if (!addToList && alreadyInList)
        {
            monumentsInAttackRange.Remove(monument);
            _priority = ePriority.Moving;
            _state = eState.Moving;
            //movementMotor.test = 0;
            //movementMotor.FindClosestWaypoint(transform.position);
        }
    }
}
