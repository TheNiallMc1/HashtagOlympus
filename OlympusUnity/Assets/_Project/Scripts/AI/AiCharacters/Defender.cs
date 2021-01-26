using System;
using _Project.Scripts.AI.AiControllers;
using UnityEngine;

public class Defender : AIBrain
{
    private AIMovement navAIMovement;

    private Vector3 _defencePosition; 
    //// Start is called before the first frame update
    protected void Start()
    {
        navAIMovement = GetComponent<AIMovement>();
        _defencePosition = transform.position;
    }

    new

        //// Update is called once per frame
        protected void FixedUpdate()
    {
        switch (state)
        {
            case EState.Moving:
                ReturnToDefencePosition();
                break;
            case EState.Attacking:
                Attack(currentAttackTarget);
                break;
            case EState.Ability:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ReturnToDefencePosition()
    {
        navAIMovement.nav.SetDestination(_defencePosition);
    }
}