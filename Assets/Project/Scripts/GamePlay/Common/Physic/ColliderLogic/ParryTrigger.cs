using System;
using Assets.Code.GamePlay.Common.Entity;
using UnityEngine;

namespace Assets.Code.GamePlay.Physic.ColliderLogic
{
    public class ParryTrigger : MonoBehaviour, ITriggerHittable
    {
        private ActorEntity _casterEntity;

        public event Action<IAttackTrigger> OnHitEvent;

        public void Init(ActorEntity casterEntity)
        {
            _casterEntity = casterEntity;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void OnHit(IAttackTrigger attackTrigger)
        {
            if(attackTrigger.HitObjects.Contains(_casterEntity))
            {
                // If the caster entity is already hit, ignore this hit
                return;
            }
            
            
            Debug.Log($"Counter {gameObject.name} was hit by {attackTrigger.GetType()}");
            OnHitEvent?.Invoke(attackTrigger);
            attackTrigger.AddHitProtected(_casterEntity);
        }
    }
}