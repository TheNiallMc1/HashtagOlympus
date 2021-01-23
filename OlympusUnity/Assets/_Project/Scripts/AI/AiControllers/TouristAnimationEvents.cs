using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.AI.AiControllers
{
    public class TouristAnimationEvents : MonoBehaviour
    {
        Combatant _touristCombatant;
        AIBrain _aIBrain;
        NavMeshAgent _navMeshAgent;
    
        // Start is called before the first frame update
        void Start()
        {
            _touristCombatant = GetComponentInParent<Combatant>();
            _aIBrain = GetComponentInParent<AIBrain>();
            _navMeshAgent = GetComponentInParent<AIMovement>().nav;
        }

    
        // Animation Events
        public void TakeDamageAnimation()
        {
            Combatant target = _aIBrain.currentAttackTarget;
            if (target != null)
            {
                target.TakeDamage(_touristCombatant.attackDamage);
            }
        }

        public void LockMovement()
        {
            _navMeshAgent.isStopped = true;
            _aIBrain.attackAnimationIsPlaying = true;
        }

        public void UnlockMovement()
        {
            _navMeshAgent.isStopped = false;
            _aIBrain.attackAnimationIsPlaying = false;
        }
    }
}
