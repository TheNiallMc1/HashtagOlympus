using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.AI.AiControllers;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Influencer : AIBrain
{

    [SerializeField]
    private List<Transform> touristDrones;

    public int waveIndex = 3;
    [SerializeField] private Transform spawnLocation;

    private static readonly int Spawn = Animator.StringToHash("SpawnSimps");
    private new readonly List<int> _autoAttackAnimations = new List<int>
        {
            AutoAttack01,
            AutoAttack02,
            AutoAttack03,
            AutoAttack04
        };

    protected void Start()
    {
        base._autoAttackAnimations = _autoAttackAnimations;

    }

    private void LateUpdate()
    {
        if(Priority != EPriority.Monument)  return; 

        if (!TouristsIsAlive())
        {
            if (RandomNumber() < 4)
            {
                SpawnWave();
            }
        }
    }

    private void SpawnWave()
    {
        _movementMotor.animator.Play(Spawn);
        for (var i = 0; i < waveIndex; i++)
        {
            SpawnTourist();
        }
    }

    private void SpawnTourist()
    {
        GameObject touristDrone = ObjectPools.SharedInstance.GetDronePoolObGameObject();
        spawnLocation = transform;
        touristDrone.transform.position = spawnLocation.position;
        touristDrone.transform.rotation = spawnLocation.rotation;

        touristDrone.SetActive(true);
        touristDrones.Add(transform);

    }

    private int RandomNumber()
    {
        var randomNumber = Random.Range(1, 2000);
        return randomNumber;
    }

    private bool TouristsIsAlive()
    {
        touristDrones = touristDrones.Where(e => e != null).ToList();

        return touristDrones.Count > 0;
    }
}
