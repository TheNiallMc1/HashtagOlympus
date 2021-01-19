﻿using System.Collections;
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


    // Animation Events
    public void TakeDamageAnimation()
    {
        godBehaviour.currentAttackTarget.TakeDamage(godBehaviour.attackDamage);
    }
}