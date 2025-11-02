using System;
using Assets.Code.GamePlay.Common.Entity;
using Project.Scripts.GamePlay.Common.Enums;
using Project.Scripts.GamePlay.Common.Health;
using Project.Scripts.GamePlay.Common.Movement;
using Project.Scripts.GamePlay.Player.Controller;
using Project.Scripts.GamePlay.Stats;
using Project.Scripts.GamePlay.Stats.Configs;
using Project.Scripts.Utils.ActionList;
using UnityEngine;

namespace Assets.Code.GamePlay.DataDriven.Effects
{
    [Serializable]
    public abstract class Effect : IActionElement
    {
        public abstract void Execute(BaseEntity caster, BaseEntity target, Transform from);
    }

    [Serializable]
    public class DamageEffect : Effect
    {
        [SerializeField] private float _amount;

        public override void Execute(BaseEntity caster, BaseEntity target, Transform from)
        {
            Debug.Log($"{caster.name} dealt {_amount} damage to {target.name}");
            float amount = _amount;
            if (caster.TryGet(out StatsController stats) && stats[StatType.Attack] > 0)
            {
                amount = amount * stats[StatType.Attack];
            }

            target.Get<IHealth>().TakeDamage(amount);
        }
    }

    [Serializable]
    public class KnockbackEffect : Effect
    {
        [SerializeField] private float _force;

        public override void Execute(BaseEntity caster, BaseEntity target, Transform from)
        {
            Debug.Log($"{caster.name} knocked back {target.name} with force {_force}");
            Vector3 dir = (target.transform.position - from.position).normalized;
            
            if (target.TryGet<IMovementForceApplier>(out var mover))
            {
                if (!mover.IsFlying)
                {
                     dir.y = Mathf.Abs(dir.y);
                }
                mover.ApplyForce(dir * _force);
                
            }
        }
    }
    [Serializable]
    public class StraightKnockbackEffect : Effect
    {
        [SerializeField] private float _force;
        [SerializeField] private CastDirection _castDirection;

        public override void Execute(BaseEntity caster, BaseEntity target, Transform from)
        {
            Debug.Log($"{caster.name} knocked back {target.name} with force {_force}");
            Vector3 dir = GetCastDirection(from);
            
            if (target.TryGet<IMovementForceApplier>(out var mover))
            {
                if (!mover.IsFlying)
                {
                     dir.y = Mathf.Abs(dir.y);
                }
                mover.ApplyForce(dir * _force);
                
            }
        }
        private Vector3 GetCastDirection(Transform tr) {
            return _castDirection switch {
                CastDirection.Forward => tr.forward,
                CastDirection.Right => tr.right,
                CastDirection.Up => tr.up,
                CastDirection.Backward => -tr.forward,
                CastDirection.Left => -tr.right,
                CastDirection.Down => -tr.up,
                _ => Vector3.one
            };
        }
    }

    [Serializable]
    public class ChangeStateEffect : Effect
    {
        [SerializeField] private StatModifierConfig[] _statsModifierConfigs;

        public override void Execute(BaseEntity caster, BaseEntity target, Transform from)
        {
            // Assuming the target has a method to change its state
            // target.ChangeState(_newState);
            foreach (var config in _statsModifierConfigs)
            {
                StatModifier modifier = config.OperatorType switch
                {
                    StatOperatorType.Add => new BasicStatModifier(config.Stat.Type, config.Duration,
                        v => v + config.Stat.Value),
                    StatOperatorType.Multiply => new BasicStatModifier(config.Stat.Type, config.Duration,
                        v => v * config.Stat.Value),
                    _ => throw new ArgumentOutOfRangeException()
                };
                Debug.Log($"{caster.name} applied modifier {config.OperatorType} to {target.name}");

                target.Components.Get<StatsController>().Mediator.AddModifier(modifier);
            }
        }
    }

    [Serializable]
    public class InvincibilityEffect : Effect
    {
        [SerializeField] private bool _invincible;

        public override void Execute(BaseEntity caster, BaseEntity target, Transform from)
        {
            // Assuming the target has a method to become invincible
            // target.BecomeInvincible(_duration);
            Debug.Log($"{caster.name} made {target.name} invincible for {_invincible}");
            target.Components.Get<IHealth>().SetInvincibility(_invincible);
        }
    }
}