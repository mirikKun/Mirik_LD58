using System;

namespace Assets.Code.GamePlay.Health
{
    public interface IHealth
    {
        event Action<float> HealthChanged;
        float Current { get; set; }
        float Max { get; set; }
        void TakeDamage(float damage);
        
        void SetInvincibility(bool isInvincible);
    }
} 