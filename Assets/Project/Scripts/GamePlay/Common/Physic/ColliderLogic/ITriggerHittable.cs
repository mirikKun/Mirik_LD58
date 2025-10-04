using UnityEngine;

namespace Assets.Code.GamePlay.Physic.ColliderLogic
{
    public interface ITriggerHittable
    {
        public Vector3 GetPosition();
        public void OnHit(IAttackTrigger attackTrigger);
    }
}