using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combatant : MonoBehaviour
{

    public enum eTargetType
    {
        Player,
        Enemy,
        PMonument,
        EMonument
    };

    [SerializeField] private eTargetType _targetType;
    public eTargetType targetType { get { return _targetType; } set { _targetType = value; } }

    public int maxHealth = 100;
    public int currentHealth = 100;
    public int attackStat = 10;

    // Start is called before the first frame update 
    void Start()
    {

    }

    // Update is called once per frame 
    void Update()
    {

    }

    public void RestoreHealth(int healthRecovered)
    {
        currentHealth += healthRecovered;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }

    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void Die()
    {
        print(gameObject.name + " has been defeated");
        Destroy(gameObject);
    }
}