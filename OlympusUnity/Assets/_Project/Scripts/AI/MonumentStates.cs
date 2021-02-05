using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Combatant))]
public class MonumentStates : MonoBehaviour
{
    [SerializeField] private GameObject _defenders;
    [SerializeField] private GameObject prefabGodMonument;
    [SerializeField] private GameObject prefabEnemyMonument;
    public int amountOfStands;

    private Combatant _thisCombatant;

    [SerializeField] private List<Combatant> _touristStands;

    [SerializeField] GameObject[] healthGlowParticles;

    [SerializeField] private bool _isGod = true;

    public enum monumentHealthState
    {
        VeryLow,
        Low,
        High,
        VeryHigh,
        Dead
    }
    [SerializeField] private monumentHealthState _eMonumentState;
    public monumentHealthState eMonumentState { get { return _eMonumentState; } set { _eMonumentState = value; } }
    


    private void Start()
    {
        
        prefabEnemyMonument =  transform.GetChild(1).gameObject;
        prefabGodMonument = transform.GetChild(0).gameObject;
        _defenders = transform.GetChild(2).gameObject;
        _thisCombatant = GetComponent<Combatant>();

        eMonumentState = monumentHealthState.VeryHigh;
        InitialiseEnemyMonuments();
        PlayerMonument();

        // SetNormalHealthParticles();
        eMonumentState = monumentHealthState.VeryHigh;
        healthGlowParticles[0].SetActive(true);

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

        if (_thisCombatant.targetType != Combatant.eTargetType.DMonument) return;
        foreach (var t in _touristStands)
        {
                
            if (t.currentHealth <= 0)
            {
                _touristStands.Remove(t);
            }

            if (_touristStands.Count == 0)
            {
                _isGod = true;
                eMonumentState = monumentHealthState.VeryHigh;
                healthGlowParticles[0].SetActive(true);
            }
        }
    }



    public void UpdateStates()
    {
        monumentHealthState oldState = eMonumentState;

        if (_thisCombatant.currentHealth > _thisCombatant.maxHealth * 0.75)
        {
            eMonumentState = monumentHealthState.VeryHigh;
        }
        else if (_thisCombatant.currentHealth > _thisCombatant.maxHealth * 0.50)
        {
            eMonumentState = monumentHealthState.High;
        }
        else if (_thisCombatant.currentHealth > _thisCombatant.maxHealth * 0.25)
        {
            eMonumentState = monumentHealthState.Low;
        }
        else if (_thisCombatant.currentHealth > 0)
        {
            eMonumentState = monumentHealthState.VeryLow;
        }
        else
        {
            eMonumentState = monumentHealthState.Dead;
        }



        if(oldState != eMonumentState)
        {
            SwapParticles(eMonumentState);
        }
        
    }

    private void SwapParticles(monumentHealthState newState)
    {
        foreach (GameObject healthEffect in healthGlowParticles)
        {
            healthEffect.SetActive(false);
        }

        switch (newState)
        {
            case monumentHealthState.VeryHigh:
                healthGlowParticles[0].SetActive(true);
                break;

            case monumentHealthState.High:
                healthGlowParticles[1].SetActive(true);
                break;

            case monumentHealthState.Low:
                healthGlowParticles[2].SetActive(true);
                break;

            case monumentHealthState.VeryLow:
                healthGlowParticles[3].SetActive(true);
                break;

            case monumentHealthState.Dead:
                break;
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
        foreach (var stand in _touristStands)
        {
            stand.currentHealth = 100;
        }
    }

    private void InitialiseEnemyMonuments()
    {
        for (int i = 1; i < amountOfStands + 1; i++)
        {
            prefabEnemyMonument.transform.GetChild(i).GetComponent<Combatant>().currentHealth = 100;
            _touristStands.Add(prefabEnemyMonument.transform.GetChild(i).GetComponent<Combatant>());
            
        }
    }

}
