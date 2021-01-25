using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonumentCollider : MonoBehaviour
{
    // This script serves as a way for the character detection colliders to communicate with the parent objects
    public Waypoint parentBehaviour;
    public ColliderType colliderType;

    // When a tourist enters the trigger, call the method in the parent behaviour
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("God"))
        {
            GodBehaviour god = other.GetComponent<GodBehaviour>();

            switch (colliderType)
            {
                case ColliderType.attackRadius:
                    parentBehaviour.UpdateGodsNearby(true, god);
                    break;
            }
        }
        if (other.gameObject.CompareTag("Tourist"))
        {
            AI_Brain tourist = other.GetComponent<AI_Brain>();

            switch (colliderType)
            {
                case ColliderType.attackRadius:
                    parentBehaviour.UpdateTouristsNearby(true, tourist);
                    break;
            }
        }
    }

    // When a tourist exits the trigger, call the method in the parent behaviour
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("God"))
        {
            GodBehaviour god = other.GetComponent<GodBehaviour>();

            switch (colliderType)
            {
                case ColliderType.attackRadius:
                    parentBehaviour.UpdateGodsNearby(false, god);
                    break;
            }
        }
        if (other.gameObject.CompareTag("Tourist"))
        {
            AI_Brain tourist = other.GetComponent<AI_Brain>();

            switch (colliderType)
            {
                case ColliderType.attackRadius:
                    parentBehaviour.UpdateTouristsNearby(false, tourist);
                    break;
            }
        }
    }
}
