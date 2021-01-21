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
        if (other.gameObject.CompareTag("God"))
        {
            Combatant god = other.GetComponentInParent<Combatant>();

            switch (colliderType)
            {
                case ColliderType.attackRadius:
                    parentBehaviour.UpdateAttackList(true, god);
                    break;
            }
        }
        if (other.gameObject.CompareTag("Monument"))
        {
            Combatant monument = other.GetComponentInParent<Combatant>();

            switch (colliderType)
            {
                case ColliderType.attackRadius:
                    parentBehaviour.UpdateMonumentList(true, monument);
                    break;
            }
        }
    }

    // When a tourist exits the trigger, call the method in the parent behaviour
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("God"))
        {
            Combatant god = other.GetComponentInParent<Combatant>();

            switch (colliderType)
            {
                case ColliderType.attackRadius:
                    parentBehaviour.UpdateAttackList(false, god);
                    break;
            }
        }
        if (other.gameObject.CompareTag("Monument"))
        {
            Combatant monument = other.GetComponentInParent<Combatant>();

            switch (colliderType)
            {
                case ColliderType.attackRadius:
                    parentBehaviour.UpdateMonumentList(false, monument);
                    break;
            }
        }
    }
}
