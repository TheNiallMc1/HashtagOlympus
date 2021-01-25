using System;
using UnityEngine;

public class MonumentCollider : MonoBehaviour
{
    // This script serves as a way for the character detection colliders to communicate with the parent objects
    public Waypoint parentBehaviour;

    // When a tourist enters the trigger, call the method in the parent behaviour
    private void OnTriggerEnter(Collider other)
    {
        var target = other.GetComponentInParent<Combatant>();

        if(target is null) return;
        if (target.targetType == Combatant.eTargetType.Player)
        {
            parentBehaviour.UpdateGodsNearby(true, target);
        }

        if (target.isActiveAndEnabled && target.targetType == Combatant.eTargetType.Enemy)
        {
            parentBehaviour.UpdateTouristsNearby(true, target);
        }
    }

    // When a tourist exits the trigger, call the method in the parent behaviour
    private void OnTriggerExit(Collider other)
    {
        var target = other.GetComponentInParent<Combatant>();

        if(target is null) return;
        switch (target.targetType)
        {
            case Combatant.eTargetType.Player:
                parentBehaviour.UpdateGodsNearby(false, target);
                break;
            case Combatant.eTargetType.PMonument:
                parentBehaviour.UpdateTouristsNearby(false, target);
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
