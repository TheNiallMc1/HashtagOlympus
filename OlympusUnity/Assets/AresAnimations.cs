using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AresAnimations : MonoBehaviour
{
    GodBehaviour godBehaviour;
    
    // Start is called before the first frame update
    void Start()
    {
        godBehaviour = GetComponentInParent<GodBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Animation Events
    public void TakeDamageAnimation()
    {
        Debug.Log("target attacked");
        godBehaviour.currentAttackTarget.TakeDamage(godBehaviour.attackDamage);
    }
}
