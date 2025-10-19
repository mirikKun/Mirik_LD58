using UnityEngine;

namespace Project.Scripts.GamePlay.Common.Physic.ColliderLogic
{
    public interface ITriggerHittable
    {
        public Vector3 GetPosition();
        public void OnHit(IAttackTrigger attackTrigger);
    }
}