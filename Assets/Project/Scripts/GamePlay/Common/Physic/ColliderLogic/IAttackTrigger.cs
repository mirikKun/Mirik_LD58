using System;
using System.Collections.Generic;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.DataDriven.Effects;
using UnityEngine;

namespace Assets.Code.GamePlay.Physic.ColliderLogic
{
    public interface IAttackTrigger
    {
        public void Init(BaseEntity casterEntity);
        public List<Effect> Effects { get; }
        BaseEntity CasterEntity { get; }
        List<ITriggerHittable> HitObjects { get; }
        public void Reset();
        public Vector3 GetPosition();
        public void AddHitProtected(ITriggerHittable hittable);
        event Action Hitted;
    }
}