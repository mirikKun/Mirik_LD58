using System;
using Assets.Code.GamePlay.Common.Entity;
using UnityEngine;

namespace Assets.Code.GamePlay.Health
{
    public abstract class BaseHealth:EntityComponent,IHealth
    {
        [SerializeField]
        private float _health;
        private bool _isInvincible;
        public event Action<float> HealthChanged;
        public event Action<BaseEntity> Died;
        public float Current { get; set; }
        public float Max { get; set; }
        private void Start()
        {
            Current = _health;
            Max = _health;
        }
        public virtual void TakeDamage(float damage)
        {
            if(_isInvincible)
                return;
            
            damage= Mathf.Clamp(damage, 0, Current);
            Current -= damage;
            Current = Mathf.Clamp(Current, 0, Current);
            if (Current <= 0)
            {
                Died?.Invoke(Entity);
            }
            HealthChanged?.Invoke(Current/Max);
            
        }

        public void SetInvincibility(bool isInvincible)
        {
            _isInvincible = isInvincible;
        }
    }
}