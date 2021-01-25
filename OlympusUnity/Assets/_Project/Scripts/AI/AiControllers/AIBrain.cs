using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.AI.AiControllers
{
    [RequireComponent(typeof(AIMovement))]
    [RequireComponent(typeof(Combatant))]
    public class AIBrain : MonoBehaviour
    {
        private AIMovement _movementMotor;

        [Header("Target Lists")]
        [SerializeField] protected internal List<Combatant> enemiesInAttackRange;
        [SerializeField] protected internal List<Combatant> monumentsInAttackRange;

        [Header("Combatants")]
        protected Combatant thisCombatant;
        public Combatant currentAttackTarget;

        public enum EPriority { Moving, Monument, God }
        public enum EState { Moving, Attacking, Ability }
    
        [Header("Dynamic States")]
        [SerializeField]
        protected EPriority priority = EPriority.Moving;
        [SerializeField]
        protected EState state = EState.Moving;
        public Waypoint wayPoint;

        [Header("Dynamic Validation")]
        public bool inRange;
        public bool initMove = true;
        public bool weightCheck;
        public bool isAttacking;
        public bool attackAnimationIsPlaying;
        public bool isTargetNotNull;
        private bool _isCombatantNotNull;
        private bool _isMonumentsNotNull;

        private bool _initialCoLoop = true;
        private int _lastNumber = 1;
        private static readonly int GodSeen = Animator.StringToHash("GodSeen");
        private static readonly int MonumentAttack = Animator.StringToHash("MonumentAttack");
        private static readonly int MonumentDestroyed = Animator.StringToHash("MonumentDestroyed");
        private static readonly int GodInRange = Animator.StringToHash("GodInRange");
    

        public EPriority Priority { get => priority;
            set => priority = value;
        }

        public EState State { get => state;
            set => state = value;
        }


        protected void Awake()
        {
            thisCombatant = GetComponent<Combatant>();
            _movementMotor = GetComponent<AIMovement>();
            State = EState.Moving;
      
        }

        #region State Behaviours

        protected void FixedUpdate()
        {
            
            
            wayPoint = _movementMotor.GetPath();
            switch (priority)
            {
                case EPriority.God: 
                    _isCombatantNotNull =  enemiesInAttackRange.Count != 0;
                    if (_isCombatantNotNull)
                    {
                        currentAttackTarget = null;
                        currentAttackTarget = enemiesInAttackRange[0];
                        isTargetNotNull = true;
                    }

                    break;
                case EPriority.Monument: 
                    _isMonumentsNotNull = monumentsInAttackRange.Count != 0;       
                    if (_isMonumentsNotNull)
                    {
                        initMove = false;
                        currentAttackTarget = monumentsInAttackRange[0];
                        isTargetNotNull = true;
                    }
                    break;
                case EPriority.Moving:
                    currentAttackTarget = null;
                    isTargetNotNull = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (state)
            {
                case EState.Moving:
                    if (!initMove)
                    {
                        _movementMotor.animator.SetBool(GodSeen, false);
                        isTargetNotNull = false;
                        inRange = false;
                        isAttacking = false;
                        _movementMotor.animator.Play("Tourist_standard_movement");
                        _movementMotor.nav.isStopped = false;
                        _initialCoLoop = true;
                        _movementMotor.Moving();
                        
                    }
                    break;
                case EState.Attacking:
                    isAttacking = true;
                    
                    if (Priority == EPriority.God)
                    {
                        Attack(currentAttackTarget);
                    }

                    if (Priority == EPriority.Monument)
                    {
                        
                        Attack(currentAttackTarget);
                    }
                    break;
                case EState.Ability:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

        #region Combat Logic

        protected virtual void Attack(Combatant target)
        {
            if (State != EState.Attacking || attackAnimationIsPlaying) return;
           if (target.currentHealth <=0 || target.targetType == Combatant.eTargetType.EMonument)
           {

               if (Priority == EPriority.God)
               {
                   UpdateAttackList(false, currentAttackTarget);
               }
               if (Priority == EPriority.Monument)
               {
                   _movementMotor.animator.SetBool(MonumentDestroyed, true);
                   UpdateMonumentList(false, currentAttackTarget);
               }
           }

           transform.LookAt(currentAttackTarget.transform.position);

           var animNumber = 1;

           
 
           if (isTargetNotNull)
           {
               _movementMotor.MoveToTarget(target);
               TargetInRange();
               if (Priority == EPriority.God && inRange)
               {
                    _movementMotor.nav.isStopped = true;
                   _movementMotor.animator.SetBool(GodSeen, true);
               }

               if (Priority == EPriority.Monument && inRange)
               {
                   _movementMotor.nav.isStopped = true;
                    _movementMotor.animator.SetTrigger(MonumentAttack);
               }
           }
           transform.LookAt(currentAttackTarget.transform.position);

           if (Priority == EPriority.God)
           {
                if (_initialCoLoop)
                {
                    _initialCoLoop = false;
                    _movementMotor.animator.Play("Tourist_standard_movement");
                }
                _movementMotor.animator.ResetTrigger("AutoAttack0" + animNumber);
               animNumber = RandomNumber();

               _movementMotor.animator.ResetTrigger("AutoAttack0" + _lastNumber);
               _movementMotor.animator.SetTrigger("AutoAttack0" + animNumber);

               _lastNumber = animNumber;


           }

           _movementMotor.animator.SetBool(GodInRange, false);

           if (currentAttackTarget != null) return;
           // movementMotor.animator.SetBool("MonumentDestroyed", true);

           _movementMotor.animator.ResetTrigger("AutoAttack0" + animNumber);




           if (Priority == EPriority.God)
           {
               UpdateAttackList(false, currentAttackTarget);
           }
           if (Priority == EPriority.Monument)
           {
               UpdateMonumentList(false, currentAttackTarget);
           }
        }

        protected void TargetInRange()
        {
            if (!isTargetNotNull) return;
            if((transform.position - wayPoint.transform.position).magnitude < 5 || (transform.position - currentAttackTarget.transform.position).magnitude < 5)
            {
                inRange = true;
            }
        }

        #endregion

        #region List Management

        internal void UpdateAttackList(bool addToList, Combatant god)
        {
            var alreadyInList = enemiesInAttackRange.Contains(god);

            switch (addToList)
            {
                // Add tourist if the method is to add from the list, and the tourist is not already in the list
                case true when !alreadyInList:
                {
                    if (god.targetType == Combatant.eTargetType.Player)
                    {
                        enemiesInAttackRange.Add(god);

                        _movementMotor.animator.SetBool(GodInRange, true);
                

                        currentAttackTarget = enemiesInAttackRange[0];
                        Priority = EPriority.God;
                        State = EState.Attacking;
                    }

                    break;
                }
                // Remove tourist if the method is to remove from the list, and the tourist is already in the list
                case false when alreadyInList:
                    enemiesInAttackRange.Remove(god);
                    Priority = EPriority.Moving;
                    State = EState.Moving;
                    break;
            }
        }
        internal void UpdateMonumentList(bool addToList, Combatant monument)
        {
            var alreadyInList = monumentsInAttackRange.Contains(monument);

            switch (addToList)
            {
                // Add tourist if the method is to add from the list, and the tourist is not already in the list
                case true when !alreadyInList:
                {
                    if (monument.targetType == Combatant.eTargetType.PMonument)
                    {
                        monumentsInAttackRange.Add(monument);
                        currentAttackTarget = monumentsInAttackRange[0];

                        if (weightCheck == false)
                        {
                            Priority = EPriority.Monument;
                            State = EState.Attacking;
                        }
                    }

                    break;
                }
                // Remove tourist if the method is to remove from the list, and the tourist is already in the list
                case false when alreadyInList:
                    monumentsInAttackRange.Remove(monument);
                    State = EState.Moving;
                    Priority = EPriority.Moving;
                    break;
            }

        
        }

#endregion

        private int RandomNumber()
        {
            var randomNumber = UnityEngine.Random.Range(1, 2);
            if (randomNumber != _lastNumber) return randomNumber;
            if (randomNumber < 2)
            {
                randomNumber++;
            }
            else
            {
                randomNumber--;
            }
            return randomNumber;
        }
    }
}
