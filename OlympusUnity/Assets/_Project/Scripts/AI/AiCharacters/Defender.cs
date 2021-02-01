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
    //// Start is called before the first frame update
    protected void Start()
    {
        _defencePosition = transform.position;
        drunkParticles.SetActive(false);
        partyParticles.SetActive(false);

        base._autoAttackAnimations = _autoAttackAnimations;
    }

    //// Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!currentAttackTarget.gameObject.CompareTag("God"))
        {
            State = EState.Moving;
        }

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
            case EPriority.Moving:
                if (State == EState.Drunk) break;
                currentAttackTarget = null;
                isTargetNotNull = false;
                break;
        }

        switch (state)
        {
            case EState.Moving:
                if (State == EState.Frozen) return;

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
                break;

            case EState.Attacking:
                if (State == EState.Frozen) return;

                partyParticles.SetActive(false);
                drunkParticles.SetActive(false);
                isAttacking = true;
                _isDrunk = false;

                Attack(currentAttackTarget);
                break;

            case EState.Drunk:
                if (State != EState.Frozen)
                {
                    if (!_isDrunk)
                    {
                        partyParticles.SetActive(false);
                        drunkParticles.SetActive(true);
                        _movementMotor.currentPosition = transform.position;
                        _isDrunk = true;
                        attackAnimationIsPlaying = false;
                        isAttacking = false;
                        _movementMotor.animator.SetBool(GodSeen, false);
                        _movementMotor.animator.Play(TouristStandardMovement);
                        _movementMotor.nav.isStopped = false;
                    }

                    _movementMotor.Drunk();
                }
                break;
            case EState.Party:
                if (State != EState.Frozen)
                {
                    partyParticles.SetActive(true);
                    drunkParticles.SetActive(false);
                    _isDrunk = false;
                    _movementMotor.MoveToTarget(currentFollowTarget);
                }
                break;
        }
    }

    private void ReturnToDefencePosition()
    {
        _movementMotor.nav.SetDestination(_defencePosition);
    }
}