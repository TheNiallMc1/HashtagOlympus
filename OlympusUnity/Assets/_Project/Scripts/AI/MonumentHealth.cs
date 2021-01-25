using UnityEngine;

[RequireComponent(typeof(Combatant))]
public class MonumentHealth : MonoBehaviour
{
 
    private Combatant _thisCombatant;

    private void Start()
    {
        _thisCombatant = GetComponent<Combatant>();
    }

    private void Update()
    {
        if(_thisCombatant.currentHealth <= 0)
        {
            _thisCombatant.targetType = Combatant.eTargetType.EMonument;
        }
    }
}
