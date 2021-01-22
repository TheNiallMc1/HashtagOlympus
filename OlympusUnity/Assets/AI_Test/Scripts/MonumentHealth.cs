using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Combatant))]
public class MonumentHealth : MonoBehaviour
{
    public enum eState { God, Tourist}

    [SerializeField]
    private float _health = 2000;
    private Combatant thisCombatant;

    public float Health { get { return _health; } set { _health = value; } }

    public eState state { get { return _state; } set { _state = value; } }

    public eState _state = eState.God;

    private void Start()
    {
        thisCombatant = GetComponent<Combatant>();
    }

    private void Update()
    {
        if(thisCombatant.currentHealth <= 0)
        {
            thisCombatant.targetType = Combatant.eTargetType.EMonument;
        }
    }
}
