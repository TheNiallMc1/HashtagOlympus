 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouristColliders : MonoBehaviour
{
    // This script serves as a way for the character detection colliders to communicate with the parent objects
    public AI_Brain parentBehaviour;
    public ColliderType colliderType;

    // When a tourist enters the trigger, call the method in the parent behaviour
    private void OnTriggerEnter(Collider other)
    {
        Combatant target = other.gameObject.GetComponentInParent<Combatant>();

        if (target != null && target.targetType == Combatant.eTargetType.Player)
        {
            parentBehaviour.UpdateAttackList(true, target);
        }

        if (target != null && target.targetType == Combatant.eTargetType.PMonument)
        {
            parentBehaviour.UpdateMonumentList(true, target);
        }
    }

    // When a tourist exits the trigger, call the method in the parent behaviour
    private void OnTriggerExit(Collider other)
    {
        Combatant target = other.gameObject.GetComponentInParent<Combatant>();

        if (target != null && target.targetType == Combatant.eTargetType.Player)
        {
            parentBehaviour.UpdateAttackList(false, target);
        }

        if (target != null && target.targetType == Combatant.eTargetType.PMonument)
        {
            parentBehaviour.UpdateMonumentList(false, target);
        }
    }
}
