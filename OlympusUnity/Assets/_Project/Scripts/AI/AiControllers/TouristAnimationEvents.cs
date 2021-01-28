using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.AI.AiControllers
{
    public class TouristAnimationEvents : MonoBehaviour
    {
        private Combatant _touristCombatant;
        private AIBrain _aIBrain;
        private NavMeshAgent _navMeshAgent;

        [SerializeField] private GameObject sprayCan;
        [SerializeField] private GameObject sprayEffect;
    
        // Start is called before the first frame update
        protected void Start()
        {
            _touristCombatant = GetComponentInParent<Combatant>();
            _aIBrain = GetComponentInParent<AIBrain>();
            _navMeshAgent = GetComponentInParent<AIMovement>().nav;
        }

    
        // Animation Events
        public void TakeDamageAnimation()
        {
            var target = _aIBrain.currentAttackTarget;
            if (target.isActiveAndEnabled)
            {
                target.TakeDamage(_touristCombatant.attackDamage);
            }
        }

        public void LockMovement()
        {
            _aIBrain.attackAnimationIsPlaying = true;
        }

        public void UnlockMovement()
        {
            _aIBrain.attackAnimationIsPlaying = false;
        }




        // Jock
        public void EquipCan()
        {
            sprayCan.SetActive(true);
        }

        public void RemoveCan()
        {
            sprayCan.SetActive(false);
        }

        public void SprayOn()
        {
            sprayEffect.SetActive(true);
        }

        public void SprayOff()
        {
            sprayEffect.SetActive(false);
        }
    }
}
