using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodColliders : MonoBehaviour
{
    // This script serves as a way for the character detection colliders to communicate with the parent objects
    public GodBehaviour parentBehaviour;
    public ColliderType colliderType;
    
    // When a tourist enters the trigger, call the method in the parent behaviour
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tourist"))
        {
            TouristStats tourist = other.GetComponent<TouristStats>();
            
            switch (colliderType)
            {
                case ColliderType.attackRadius:
                    parentBehaviour.UpdateAttackList(true, tourist);
                    break;
                
                case ColliderType.awarenessRadius:
                    parentBehaviour.UpdateAwarenessList(true, tourist);
                    break;
            }
        }
    }

    // When a tourist exits the trigger, call the method in the parent behaviour
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Tourist"))
        {
            TouristStats tourist = other.GetComponent<TouristStats>();
            
            switch (colliderType)
            {
                case ColliderType.attackRadius:
                    parentBehaviour.UpdateAttackList(false, tourist);
                    break;
                
                case ColliderType.awarenessRadius:
                    parentBehaviour.UpdateAwarenessList(false, tourist);
                    break;
            }
        }
    }
}

public enum ColliderType
{
    attackRadius,
    awarenessRadius
}