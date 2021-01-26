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
        public enum EState { Moving, Attacking, Ability, Drunk, Follow }

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

        [Header("Animation")]
        private bool _initialCoLoop = true;
        private int _lastNumber = 1;
        private static readonly int GodSeen = Animator.StringToHash("GodSeen");
        private static readonly int MonumentAttack = Animator.StringToHash("MonumentAttack");
        private static readonly int MonumentDestroyed = Animator.StringToHash("MonumentDestroyed");
        private static readonly int GodInRange = Animator.StringToHash("GodInRange");
        private static readonly int TouristStandardMovement = Animator.StringToHash("Tourist_standard_movement");
        private static readonly int AutoAttack01 = Animator.StringToHash("AutoAttack01");
        private static readonly int AutoAttack02 = Animator.StringToHash("AutoAttack02");

        private readonly List<int> _autoAttackAnimations = new List<int>
        {
            AutoAttack01,
            AutoAttack02,
        };


        public EPriority Priority
        {
            get => priority;
            set => priority = value;
        }

        public EState State
        {
            get => state;
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
                    _isCombatantNotNull = enemiesInAttackRange.Count != 0;
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
                        attackAnimationIsPlaying = false;
                        _movementMotor.animator.SetBool(GodSeen, false);
                        isTargetNotNull = false;
                        inRange = false;
                        isAttacking = false;
                        _movementMotor.animator.Play(TouristStandardMovement);
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
                case EState.Drunk:
                    _movementMotor.currentPosition = transform.position;
                    _movementMotor.Drunk();
                    break;
                case EState.Follow:
                    _movementMotor.MoveToTarget(currentAttackTarget);
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
            if (target.currentHealth <= 0 || target.targetType == Combatant.eTargetType.EMonument)
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
                    
                    _movementMotor.animator.SetBool(GodSeen, true);
                }

                if (Priority == EPriority.Monument && inRange)
                {
                    _movementMotor.animator.SetTrigger(MonumentAttack);
                }
            }
            transform.LookAt(currentAttackTarget.transform.position);

            if (Priority == EPriority.God)
            {

                if (_initialCoLoop)
                {
                    _initialCoLoop = false;
                    _movementMotor.animator.Play(TouristStandardMovement);
                }
                _movementMotor.animator.ResetTrigger(_autoAttackAnimations[animNumber]);
                animNumber = RandomNumber();
                Debug.Log(animNumber);
                attackAnimationIsPlaying = true;
                _movementMotor.animator.ResetTrigger(_autoAttackAnimations[_lastNumber]);
                _movementMotor.animator.SetTrigger(_autoAttackAnimations[animNumber]);

                _lastNumber = animNumber;


            }

          

            if (currentAttackTarget != null) return;
             _movementMotor.animator.SetBool(MonumentDestroyed, true);

            _movementMotor.animator.ResetTrigger(_autoAttackAnimations[animNumber]);
             _movementMotor.animator.SetBool(GodInRange, false);



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
            Transform targetPosition = currentAttackTarget.transform;
            if (Priority != EPriority.God)
            {
                if (Priority == EPriority.Monument) targetPosition = wayPoint.transform;
            }
            else
                targetPosition = currentAttackTarget.transform;

            if (!isTargetNotNull) return;
            var position = transform.position;
            if (!((position - targetPosition.position).magnitude < 5)) return;
            inRange = true;
            _movementMotor.nav.isStopped = true;



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

                            Priority = EPriority.God;
                            State = EState.Attacking;
                        }

                        break;
                    }
                // Remove tourist if the method is to remove from the list, and the tourist is already in the list
                case false when alreadyInList:
                    enemiesInAttackRange.Remove(god);
                    if (enemiesInAttackRange.Count > 0) return;
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
                    if (monumentsInAttackRange.Count > 0) return;
                    State = EState.Moving;
                    Priority = EPriority.Moving;
                    break;
            }


        }

        #endregion

        private int RandomNumber()
        {
            var randomNumber = UnityEngine.Random.Range(0, _autoAttackAnimations.Count - 1);
            if (randomNumber != _lastNumber) return randomNumber;
            if (randomNumber < _autoAttackAnimations.Count - 1)
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
