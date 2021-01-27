using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FreezeStatus", menuName = "Status Effect/Freeze", order = 1)]
public class Status_Freeze : StatusEffect
{
    [Header("Freeze Variables")] 
    private float baseSpeed;
    
    public override void TickEffect()
        {
            throw new System.NotImplementedException();
        }
    
        public override void EntryEffect()
        {
            //baseSpeed = affectedCombatant.navMeshAgent.speed;
            //affectedCombatant.navMeshAgent.speed = 0;
        }
    
        public override void ExitEffect()
        {
            //affectedCombatant.navMeshAgent.speed = baseSpeed;
        }
}
