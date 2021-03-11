using System.Collections.Generic;
using _Project.Scripts.AI.AiControllers;
using UnityEngine;

public class Defender : AIBrain
{
    private new readonly List<int> _autoAttackAnimations = new List<int>
        {
            AutoAttack01,
            AutoAttack02,
            AutoAttack03,
            AutoAttack04,
            AutoAttack05
        };

    public Vector3 _defencePosition;
    protected void Start()
    {
        _defencePosition = transform.position;
        drunkParticles.SetActive(false);
        partyParticles.SetActive(false);

        base._autoAttackAnimations = _autoAttackAnimations;
    }
    
    protected override void FixedUpdate()
    {
        //base.FixedUpdate();
        if(monumentsInAttackRange.Count > 0)
            monumentsInAttackRange.Clear();
       

        if (enemiesInAttackRange.Count == 0)
        {
            State = EState.Moving;
            Priority = EPriority.Moving;
        }
        
        PriorityUpdate();
        StateUpdate();
    }

    protected override void PriorityUpdate()
    {
        switch (priority)
        {
            case EPriority.God :
                _isCombatantNotNull = enemiesInAttackRange.Count != 0;
                if (_isCombatantNotNull)
                {
                    currentAttackTarget = null;
                    currentAttackTarget = enemiesInAttackRange[0];
                    isTargetNotNull = true;
                }

                break;
            case EPriority.Moving when State != EState.Drunk:
                currentAttackTarget = null;
                isTargetNotNull = false;
                break;
        }
    }

    protected override void StateUpdate()
    {
        switch (state)
        {
            case EState.Moving when State == EState.Frozen:
                return;
            case EState.Moving:
                Moving();
                break;
            case EState.Attacking when State == EState.Frozen:
                return;
            case EState.Attacking:
                Attacking();
                break;
            case EState.Drunk when State != EState.Frozen:
                Drunk();
                break;
            case EState.Party:
                Party();
                break;
            case EState.Hangover:
                Hungover();
                break;
        }
    }
    
    protected override void Moving()
    {
        partyParticles.SetActive(false);
        drunkParticles.SetActive(false);
        _isDrunk = false;
        attackAnimationIsPlaying = false;
        isTargetNotNull = false;
        inRange = false;
        isAttacking = false;
        _movementMotor.animator.SetBool(GodSeen, false);
        _movementMotor.nav.isStopped = false;
        _initialCoLoop = true;
        ReturnToDefencePosition();
    }

    protected override void Attacking()
    {
        partyParticles.SetActive(false);
        drunkParticles.SetActive(false);
        isAttacking = true;
        _isDrunk = false;

        Attack(currentAttackTarget);
    }

    protected override void Drunk()
    {
        _movementMotor.nav.isStopped = false;
        partyParticles.SetActive(true);
        drunkParticles.SetActive(false);
        _isDrunk = false;
        _movementMotor.MoveToTarget(currentFollowTarget);
    }

    protected override void Party()
    {
        _movementMotor.nav.isStopped = true;
        _movementMotor.animator.SetBool(GodSeen, false);
        _movementMotor.animator.speed = 0;
    }

    protected override void Hungover()
    {
        _movementMotor.nav.isStopped = true;
        _movementMotor.animator.SetBool(GodSeen, false);
    }
    private void ReturnToDefencePosition()
    {
        _movementMotor.nav.SetDestination(_defencePosition);
    }
}