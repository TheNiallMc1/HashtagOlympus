using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonumentHealth : MonoBehaviour
{
    public enum eState { God, Tourist}

    [SerializeField]
    private float _health = 2000;

    public float Health { get { return _health; } set { _health = value; } }

    public eState state { get { return _state; } set { _state = value; } }

    public eState _state = eState.God;

    public void RemoveObject() 
    {
        Destroy(this.gameObject);
    }

   
}
