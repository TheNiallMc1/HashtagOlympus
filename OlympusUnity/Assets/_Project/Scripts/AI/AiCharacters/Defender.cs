using System;
using _Project.Scripts.AI.AiControllers;
using UnityEngine;

public class Defender : AIBrain
{
    private Vector3 _defencePosition; 
    //// Start is called before the first frame update
    protected override void Start()
    {
        _defencePosition = transform.position;
        drunkParticles.SetActive(false);
        partyParticles.SetActive(false);
    }

        //// Update is called once per frame
        protected override void FixedUpdate()
    {
        if (!currentAttackTarget.gameObject.CompareTag("God"))
        {
            State = EState.Moving;
        }
        switch (state)
        {
            case EState.Moving:
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
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ReturnToDefencePosition()
    {
        _movementMotor.nav.SetDestination(_defencePosition);
    }
}