﻿using System;
using UnityEngine;

namespace _Project.Scripts.AI.AiControllers
{
    public class TouristColliders : MonoBehaviour
    {
        // This script serves as a way for the character detection colliders to communicate with the parent objects
        public AIBrain parentBehaviour;
        public ColliderType colliderType;

        // When a tourist enters the trigger, call the method in the parent behaviour
        private void OnTriggerEnter(Collider other)
        {
            if (!this.isActiveAndEnabled) return;
            var target = other.GetComponentInParent<Combatant>();

            if (target == null) return;
            var targetType = target.targetType;
            switch (targetType)
            {
                case Combatant.eTargetType.Player:
                    parentBehaviour.UpdateAttackList(true, target);
                    break;
                case Combatant.eTargetType.PMonument:
                    parentBehaviour.UpdateMonumentList(true, target);
                    break;
                case Combatant.eTargetType.Enemy:
                    break;
                case Combatant.eTargetType.EMonument:
                    break;
            }
        }

        // When a tourist exits the trigger, call the method in the parent behaviour
        private void OnTriggerExit(Collider other)
        {

            if (!this.isActiveAndEnabled) return;
            var target = other.GetComponentInParent<Combatant>();

            if (target == null) return;
            var targetType = target.targetType;
            switch (targetType)
            {
                case Combatant.eTargetType.Player:
                    parentBehaviour.UpdateAttackList(false, target);
                    break;
                case Combatant.eTargetType.PMonument:
                    parentBehaviour.UpdateMonumentList(false, target);
                    break;
                case Combatant.eTargetType.Enemy:
                    break;
                case Combatant.eTargetType.EMonument:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
