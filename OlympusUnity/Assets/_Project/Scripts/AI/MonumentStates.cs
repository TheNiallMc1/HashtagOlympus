using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Combatant))]
public class MonumentStates : MonoBehaviour
{
    [FormerlySerializedAs("_defenders")] [SerializeField]
    private List<GameObject> defenders;
    [SerializeField]
    private GameObject objectToPool;

    [SerializeField] private GameObject prefabGodMonument;
    [SerializeField] private GameObject prefabEnemyMonument;
    public int amountToPool;
    public int amountOfStands;

    private Combatant _thisCombatant;
    private Combatant.eTargetType _currentTargetType;

    [SerializeField] List<Combatant> _touristStands;

    [SerializeField] bool _isGod = true;

    private void Start()
    {
        
        prefabEnemyMonument =  transform.GetChild(1).gameObject;
        prefabGodMonument = transform.GetChild(0).gameObject;
        _thisCombatant = GetComponent<Combatant>();
        _currentTargetType = _thisCombatant.targetType;
        defenders = new List<GameObject>();
        GameObject tmp;
        for (var i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            defenders.Add(tmp);
        }
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
            int j = _touristStands.Count;
            Debug.Log("initial j = " + j);
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
    }

    private void DamagedMonument()
    {

        prefabEnemyMonument.SetActive(true);
        prefabGodMonument.SetActive(false);
        for (int i = 1; i < amountOfStands + 1; i++)
        {
            prefabEnemyMonument.transform.GetChild(i).GetComponent<Combatant>().currentHealth = 100;
            _touristStands.Add(prefabEnemyMonument.transform.GetChild(i).GetComponent<Combatant>());
            
        }
    }

}
