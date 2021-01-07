using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonumentHealth : MonoBehaviour
{
    [SerializeField]
    private float _health = 2000;

    public float Health { get { return _health; } set { _health = value; } }

    public void RemoveObject() 
    {
        Destroy(this.gameObject);
    }
}
