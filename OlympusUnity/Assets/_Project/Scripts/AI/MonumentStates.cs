using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Combatant))]
public class MonumentStates : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _defenders;
    [SerializeField]
    private GameObject objectToPool;
    public int amountToPool;

    private Combatant _thisCombatant;
    private Combatant.eTargetType _currentTargetType;

    private void Start()
    {
        _thisCombatant = GetComponent<Combatant>();
        _currentTargetType = _thisCombatant.targetType;
        _defenders = new List<GameObject>();
        GameObject tmp;
        for (var i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            _defenders.Add(tmp);
        }
    }

    private void LateUpdate()
    {
        if(_thisCombatant.currentHealth == _thisCombatant.maxHealth) return;
        if (_thisCombatant.currentHealth < _thisCombatant.maxHealth)
            _thisCombatant.targetType = Combatant.eTargetType.DMonument;

        if (_thisCombatant.targetType == Combatant.eTargetType.DMonument && _thisCombatant.currentHealth <= 0)
        {
            switch (_currentTargetType)
            {
                case Combatant.eTargetType.PMonument:
                    _thisCombatant.targetType = Combatant.eTargetType.EMonument;
                    _thisCombatant.currentHealth = 100;
                    break;
                case Combatant.eTargetType.EMonument:
                    _thisCombatant.targetType = Combatant.eTargetType.PMonument;
                    _thisCombatant.currentHealth = 100;
                    break;
            }
        }
        switch (_thisCombatant.targetType)
        {
            case Combatant.eTargetType.PMonument:
                if(_currentTargetType != Combatant.eTargetType.EMonument) return;
                PlayerMonument();
                _currentTargetType = Combatant.eTargetType.PMonument;
                break;
            case Combatant.eTargetType.DMonument:
                DamagedMonument();
                break;
            case Combatant.eTargetType.EMonument:
                if(_currentTargetType != Combatant.eTargetType.PMonument) return;
                EnemyMonument();
                _currentTargetType = Combatant.eTargetType.EMonument;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void SpawnDefence()
    {
        return;
    }

    private void SpawnStructures()
    {
        return;
    }

    private void PlayerMonument()
    {
        return;
    }

    private void DamagedMonument()
    {
        return;
    }

    private void EnemyMonument()
    {
        return;
    }
}
