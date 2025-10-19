using Assets.Code.GamePlay.Physic.ColliderLogic;
using UnityEngine;

namespace Assets.Code.GamePlay.Common.Entity
{
    public class ActorEntity:BaseEntity,ITriggerHittable
    {
        protected override void InitComponentsRegistry()
        {
            Components= new ComponentsRegistry(_componentsList,this);
        }
        public Vector3 GetPosition()
        {
            return transform.position;
        }
        

        public void OnHit(IAttackTrigger attackTrigger)
        {
            foreach (var effect in attackTrigger.Effects)
            {
                effect.Execute(attackTrigger.CasterEntity,this,attackTrigger.GetPosition());
            }
        }
    }
}