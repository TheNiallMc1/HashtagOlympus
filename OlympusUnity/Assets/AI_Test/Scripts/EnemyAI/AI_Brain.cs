using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AI_Movement))]
public class AI_Brain : MonoBehaviour
{
    AI_Movement movementMotor;

    protected enum ePriority { Moving, Monument, God }
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


    public bool initMove = true;
    protected GameObject attackTarget;
    MonumentHealth monument;


    protected ePriority priority { get { return _priority; } set { _priority = value; } }
    protected eState state { get { return _state; } set { _state = value; } }
    public float health { get { return _health; } set { _health = value; } }

    private void Awake()
    {
        movementMotor = this.GetComponent<AI_Movement>();
        _state = eState.Moving;
    }

    protected virtual void Start()
    {
        
    }

    void FixedUpdate()
    {
        switch (_state)
        {
            case eState.Moving:
                if (!initMove)
                {
                    movementMotor.Moving();
                }
                break;
            case eState.Attacking: 
                if (movementMotor.GetPath() != null)
                {
                    attackTarget = movementMotor.GetPath().gameObject;
                }
                break;
        }
      
    }

    protected virtual void Attack()
    {
        
        if (attackTarget != null)
        {
            movementMotor.MoveToTarget(attackTarget);
            if((transform.position - attackTarget.transform.position).magnitude < 2)
            {
                //StartCoroutine(AttackingCoroutine);
            }
        
        }
        
    }

    /*
    Handles AI while it is attacking a monument. 
   */
    protected IEnumerator AttackingCoroutine()
    {
        while (attackTarget != null || _state == eState.Attacking)
        {

            if (attackTarget == null || monument.Health <= 0)
            {
                // yield return new WaitForSeconds(10);
                if (attackTarget != null) 
                { 
                    //attackTarget.RemoveObject();
                }

            }
            else
            {
              //  monument.Health = monument.Health - _damage;
            }

            yield return new WaitForSeconds(5);

        }
        yield break;
    }

    /*
    Handles AI while it is targetting a God. 
    */
    protected IEnumerator AttackingGodCoroutine()
    {
        yield break;
    }

}
