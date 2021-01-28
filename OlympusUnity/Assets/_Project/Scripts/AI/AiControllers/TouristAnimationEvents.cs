using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.AI.AiControllers
{
    public class TouristAnimationEvents : MonoBehaviour
    {
        private Combatant _touristCombatant;
        private AIBrain _aIBrain;
        private NavMeshAgent _navMeshAgent;

        [SerializeField] private GameObject handheldObject;
        [SerializeField] private GameObject particleEffect;
    
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



        // Objects and particle effect events

        public void EquipObject()
        {
            handheldObject.SetActive(true);
        }

        public void RemoveObject()
        {
            handheldObject.SetActive(false);
        }

        public void ParticleEffectOn()
        {
            particleEffect.SetActive(true);
        }

        public void ParticleEffectOff()
        {
            particleEffect.SetActive(false);
        }


    }
}
