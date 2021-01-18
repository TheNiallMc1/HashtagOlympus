using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combatant : MonoBehaviour
{

    public enum TargetType { Player, Enemy, PMonument, EMonument };

    public TargetType targetType;

    public int health = 100;
    public int attackStat = 10;

    // Start is called before the first frame update 
    void Start()
    {

    }

    // Update is called once per frame 
    void Update()
    {

    }

    public void TakeDamage(int damageTaken)
    {
        health -= damageTaken;

        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    public void Die()
    {
        print(gameObject.name + " has been defeated");
        Destroy(gameObject);
    }
}