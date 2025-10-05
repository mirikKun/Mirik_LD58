using System;
using Assets.Code.GamePlay.Common.Entity;

namespace Assets.Code.GamePlay.Health
{
    public interface IHealth
    {
        event Action<float> HealthChanged;
        float Current { get; set; }
        float Max { get; set; }
        void TakeDamage(float damage);
        
        void SetInvincibility(bool isInvincible);
        event Action<BaseEntity> Died;
    }
} 