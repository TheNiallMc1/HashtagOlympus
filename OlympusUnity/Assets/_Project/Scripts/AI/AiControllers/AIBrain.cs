﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts.AI.AiControllers
{
    [RequireComponent(typeof(AIMovement))]
    [RequireComponent(typeof(Combatant))]
    public class AIBrain : MonoBehaviour
    {
        protected AIMovement _movementMotor;
        public GameObject drunkParticles;
        public GameObject partyParticles;

        [Header("Target Lists")] [SerializeField]
        protected internal List<Combatant> enemiesInAttackRange;

        [SerializeField] protected internal List<Combatant> monumentsInAttackRange;

        [Header("Combatants")] protected Combatant thisCombatant;
        public Combatant currentAttackTarget;
        [HideInInspector] public Combatant currentFollowTarget;

        public enum EPriority
        {
            Moving,
            Monument,
            God
        }

        public enum EState
        {
            Moving,
            Attacking,
            Ability,
            Drunk,
            Party,
            Frozen,
            Hangover
        }

        [Header("Dynamic States")] [SerializeField]
        protected EPriority priority = EPriority.Moving;

        [SerializeField] protected EState state = EState.Moving;
        public Waypoint wayPoint;

        [Header("Dynamic Validation")] public bool isFrozen;
        public bool inRange;
        public bool initMove = true;
        public bool weightCheck;
        public bool isAttacking;
        public bool attackAnimationIsPlaying;
        public bool isTargetNotNull;
        public bool _isCombatantNotNull;
        private bool _isMonumentsNotNull;
        public bool _isDrunk;
        public bool _isDead;
        public bool _drunkCoroutineRunning;
        public bool _isHungover;

        [Header("Animation")] 
        protected bool _initialCoLoop = true;
        private int _lastNumber = 1;
        private int animNumber;
        protected static readonly int GodSeen = Animator.StringToHash("GodSeen");
        protected static readonly int MonumentAttack = Animator.StringToHash("MonumentAttack");
        protected static readonly int MonumentDestroyed = Animator.StringToHash("MonumentDestroyed");
        protected static readonly int GodInRange = Animator.StringToHash("GodInRange");
        protected static readonly int TouristStandardMovement = Animator.StringToHash("Tourist_standard_movement");

        protected static readonly int AutoAttack01 = Animator.StringToHash("AutoAttack01");
        protected static readonly int AutoAttack02 = Animator.StringToHash("AutoAttack02");
        protected static readonly int AutoAttack03 = Animator.StringToHash("AutoAttack03");
        protected static readonly int AutoAttack04 = Animator.StringToHash("AutoAttack04");
        protected static readonly int AutoAttack05 = Animator.StringToHash("AutoAttack05");

        public List<int> _autoAttackAnimations = new List<int>
        {
            AutoAttack01,
            AutoAttack02
        };

        private static readonly int Hangover = Animator.StringToHash("Hangover");


        public EPriority Priority
        {
            get => priority;

            set
            {
                if (!isFrozen && !_isDrunk && !_isHungover)
                {
                    priority = value;
                }
            }
        }

        public EState State
        {
            get => state;

            set
            {
                if (!isFrozen && !_isDrunk && !_isHungover)
                {
                    state = value;
                }
            }
        }


        protected void Awake()
        {
            thisCombatant = GetComponent<Combatant>();
            _movementMotor = GetComponent<AIMovement>();
            _isDead = false;
            State = EState.Moving;

            drunkParticles.SetActive(false);
            partyParticles.SetActive(false);

        }


        #region State Behaviours

        protected virtual void FixedUpdate()
        {
            if (_isDead)
            {
                gameObject.SetActive(false);
            }

            wayPoint = _movementMotor.GetPath();

            PriorityUpdate();
            StateUpdate();


        }

        protected virtual void PriorityUpdate()
        {
            _isCombatantNotNull = enemiesInAttackRange.Count != 0;
            _isMonumentsNotNull = monumentsInAttackRange.Count != 0;

            switch (priority)
            {
                case EPriority.God when _isCombatantNotNull:
                    currentAttackTarget = null;
                    currentAttackTarget = enemiesInAttackRange[0];
                    isTargetNotNull = true;
                    break;
                case EPriority.Monument when _isMonumentsNotNull:
                    initMove = false;
                    currentAttackTarget = monumentsInAttackRange[0];
                    isTargetNotNull = true;
                    break;
                case EPriority.Moving when State != EState.Drunk:
                    currentAttackTarget = null;
                    isTargetNotNull = false;
                    break;
            }
        }

        protected virtual void StateUpdate()
        {
            switch (state)
            {
                case EState.Moving:
                    Moving();
                    break;
                case EState.Attacking:
                    Attacking();
                    break;
                case EState.Drunk:
                    Drunk();
                    break;
                case EState.Party when State != EState.Frozen:
                    Party();
                    break;
                case EState.Frozen:
                    Frozen();
                    break;
                case EState.Hangover:
                    Hungover();
                    break;
            }
        }

        protected virtual void Moving()
        {
            if (State == EState.Frozen && !isActiveAndEnabled) return;
            
            _movementMotor.nav.isStopped = false;
            
            if (initMove) return;
            
            partyParticles.SetActive(false);
            drunkParticles.SetActive(false);
            _isDrunk = false;
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

        protected virtual void Attacking()
        {
            if (State == EState.Frozen && !isActiveAndEnabled) return;
            
            if (_movementMotor.nav != null)
            {
                _movementMotor.nav.isStopped = false;
            }

            partyParticles.SetActive(false);
            drunkParticles.SetActive(false);
            isAttacking = true;
            _isDrunk = false;

            if (Priority == EPriority.God)
            {
                Attack(currentAttackTarget);
            }

            if (Priority == EPriority.Monument)
            {

                Attack(currentAttackTarget);
            }
        }

        protected virtual void Party()
        {
            _movementMotor.nav.isStopped = false;
            partyParticles.SetActive(true);
            drunkParticles.SetActive(false);
            _isDrunk = false;
            _movementMotor.MoveToTarget(currentFollowTarget);
        }

        protected virtual void Drunk()
        {
            if (State != EState.Frozen || isActiveAndEnabled)
            {
                if (_drunkCoroutineRunning) return;
                StartCoroutine(_movementMotor.Drunk());
            }
        }

        private void Frozen()
        {
            _movementMotor.nav.isStopped = true;
            _movementMotor.animator.SetBool(GodSeen, false);
            _movementMotor.animator.speed = 0;
        }
        
        protected virtual void Hungover()
        {
            _movementMotor.nav.isStopped = true;
            _movementMotor.animator.SetBool(GodSeen, false);
        }

        public void ActivateDrunk()
        {
            _movementMotor.nav.isStopped = false;
            partyParticles.SetActive(false);
            drunkParticles.SetActive(true);
            _movementMotor.currentPosition = transform.position;

            attackAnimationIsPlaying = false;
            isAttacking = false;
            _movementMotor.animator.SetBool(GodSeen, false);
            _movementMotor.animator.Play("Drunk_movement");
        }

        public void ActivateParty()
        {
            _movementMotor.animator.Play("Party");
        }

        public void ActivateHangover()
        {
            _movementMotor.animator.SetTrigger(Hangover);
        }

        #endregion

        #region Combat Logic

        protected virtual void Attack(Combatant target)
        {
            if (State != EState.Attacking || attackAnimationIsPlaying) return;
            if (target.currentHealth <= 0 || target.targetType == Combatant.eTargetType.EMonument)
            {

                UpdateLists(true);
            }
            
            SelectTargets();

            if (currentAttackTarget != null) return;
             _movementMotor.animator.SetBool(MonumentDestroyed, true);

            _movementMotor.animator.ResetTrigger(_autoAttackAnimations[animNumber]);
             _movementMotor.animator.SetBool(GodInRange, false);
             
            UpdateLists(false);
        }

        private void UpdateLists(bool isDestroyed)
        {
            if (Priority == EPriority.God)
                UpdateAttackList(false, currentAttackTarget);
            
            if (Priority != EPriority.Monument) return;
            
            if(isDestroyed) 
                _movementMotor.animator.SetBool(MonumentDestroyed, true);
            
            UpdateMonumentList(false, currentAttackTarget);
        }

        private void SelectTargets()
        {
            
            transform.LookAt(currentAttackTarget.transform.position);

            animNumber = 1;

            foreach (var targetCombatant in from targetCombatant in enemiesInAttackRange
                let closest = transform.position - currentAttackTarget.transform.position
                where (transform.position - targetCombatant.transform.position).magnitude < closest.magnitude
                select targetCombatant)
            {
                currentAttackTarget = targetCombatant;
            }
            if (isTargetNotNull)
            {
                MoveToTarget();
            }
            
        }

        private void MoveToTarget()
        {
            _movementMotor.MoveToTarget(currentAttackTarget);
            TargetInRange();

            if (Priority == EPriority.God && inRange)
            {
                    
                _movementMotor.animator.SetBool(GodSeen, true);
            }

            if (Priority == EPriority.Monument && inRange)
            {
                _movementMotor.animator.SetTrigger(MonumentAttack);
            }
            
            transform.LookAt(currentAttackTarget.transform.position);

            if (Priority == EPriority.God && inRange)
            {

                if (_initialCoLoop)
                {
                    _initialCoLoop = false;
                    _movementMotor.animator.Play(TouristStandardMovement);
                }
                _movementMotor.animator.ResetTrigger(_autoAttackAnimations[animNumber]);
                animNumber = RandomNumber();
                attackAnimationIsPlaying = true;
                _movementMotor.animator.ResetTrigger(_autoAttackAnimations[_lastNumber]);
                _movementMotor.animator.SetTrigger(_autoAttackAnimations[animNumber]);

                _lastNumber = animNumber;


            }
        }

        protected void TargetInRange()
        {
           // var targetPosition = Priority == EPriority.Monument ? wayPoint.transform : currentAttackTarget.transform;
           var targetPosition = currentAttackTarget.transform;
           if (!isTargetNotNull) return;
            var position = transform.position;
            var i = 6;
            if (_movementMotor.wpIndex == 9) i = 14;
            if (!((position - targetPosition.position).magnitude <= i)) return;
            inRange = true;
            _movementMotor.nav.isStopped = true;



        }

        public virtual void OnDeathEvent()
        {
            // Call base and override if needed
            _movementMotor.animator.Play("Death");
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
                        if(State == EState.Drunk) return;
                        
                        if (god.targetType == Combatant.eTargetType.Player)
                        {
                            enemiesInAttackRange.Add(god);

                            _movementMotor.animator.SetBool(GodSeen, true);

                            
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
                                if (State == EState.Drunk) return;
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
