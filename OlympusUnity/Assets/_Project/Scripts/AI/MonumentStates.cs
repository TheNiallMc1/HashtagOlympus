using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Combatant))]
public class MonumentStates : MonoBehaviour
{
    [SerializeField] private GameObject _defenders;
    [SerializeField] private GameObject prefabGodMonument;
    [SerializeField] private GameObject prefabEnemyMonument;
    public int amountToPool;
    public int amountOfStands;

    private Combatant _thisCombatant;

    [SerializeField] private List<Combatant> _touristStands;
    

    [SerializeField] private bool _isGod = true;

    private void Start()
    {
        
        prefabEnemyMonument =  transform.GetChild(1).gameObject;
        prefabGodMonument = transform.GetChild(0).gameObject;
        _defenders = transform.GetChild(2).gameObject;
        _thisCombatant = GetComponent<Combatant>();

        
        PlayerMonument();
    }

    private void LateUpdate()
    {
        if(_thisCombatant.currentHealth == _thisCombatant.maxHealth) return;

        if (_thisCombatant.currentHealth <= 0 && _isGod)
        {
            switch (_thisCombatant.targetType)
            {
                case Combatant.eTargetType.PMonument:
                    _thisCombatant.targetType = Combatant.eTargetType.DMonument;
                        _isGod = false;
                    DamagedMonument();
                    break;
                case Combatant.eTargetType.DMonument:
                    _thisCombatant.targetType = Combatant.eTargetType.PMonument;
                        _thisCombatant.currentHealth = 100;
                    PlayerMonument();
                    break;
            }
        }
        if (_thisCombatant.targetType == Combatant.eTargetType.DMonument)
        {
            foreach (var t in _touristStands)
            {
                
                if (t.currentHealth <= 0)
                {
                    _touristStands.Remove(t);
                }

                if (_touristStands.Count == 0)
                {
                    _isGod = true;
                }
            }
        }
    }

    private void PlayerMonument()
    {
        prefabGodMonument.SetActive(true);
        prefabEnemyMonument.SetActive(false);
        _defenders.SetActive(false);
    }

    private void DamagedMonument()
    {

        prefabEnemyMonument.SetActive(true);
        prefabGodMonument.SetActive(false);
        _defenders.SetActive(true);
        for (int i = 1; i < amountOfStands + 1; i++)
        {
            prefabEnemyMonument.transform.GetChild(i).GetComponent<Combatant>().currentHealth = 100;
            _touristStands.Add(prefabEnemyMonument.transform.GetChild(i).GetComponent<Combatant>());
            
        }
    }

}
