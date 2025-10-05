using System;
using Assets.Code.Common.Utils.ActionList;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Health;
using Assets.Code.GamePlay.Player.Controller;
using Assets.Code.GamePlay.Stats;
using UnityEngine;

namespace Assets.Code.GamePlay.DataDriven.Effects
{
    [Serializable]
    public abstract class Effect:IActionElement
    {
        public abstract void Execute(BaseEntity caster, BaseEntity target,Vector3 from);
    }
    
    [Serializable]
    public class DamageEffect:Effect
    {
        [SerializeField] private float _amount;

        public override void Execute(BaseEntity caster, BaseEntity target,Vector3 from) {
            Debug.Log($"{caster.name} dealt {_amount} damage to {target.name}");

            float amount = _amount;
            if(caster.TryGet(out StatsController stats) && stats[StatType.Attack]>0)
            {
                amount = amount*stats[StatType.Attack];
            }
            
            
            target.Get<IHealth>().TakeDamage(amount);
        }
    }
    [Serializable]
    public class KnockbackEffect : Effect {
        [SerializeField] private float _force;

        public override void Execute(BaseEntity caster, BaseEntity target,Vector3 from) {
            // var dir = (target.transform.position - caster.transform.position).normalized;
            // target.GetComponent<Rigidbody>().AddForce(dir * _force, ForceMode.Impulse);
            Debug.Log($"{caster.name} knocked back {target.name} with force {_force}");
            Vector3 dir = (target.transform.position - from).normalized;
            dir.y = Mathf.Abs(dir.y);
            if(target.TryGet<PlayerMover>( out var mover))
            {
                mover.SetMomentum(dir * _force);
            }
           
        }
    }
    [Serializable]
    public class ChangeStateEffect : Effect {
        [SerializeField] private StatModifierConfig[] _statsModifierConfigs;

        public override void Execute(BaseEntity caster, BaseEntity target,Vector3 from) {
            // Assuming the target has a method to change its state
            // target.ChangeState(_newState);
            foreach (var config in _statsModifierConfigs)
            {
     
                StatModifier modifier = config.OperatorType switch
                {
                    StatOperatorType.Add => new BasicStatModifier(config.Stat.Type, config.Duration, v => v + config.Stat.Value),
                    StatOperatorType.Multiply => new BasicStatModifier(config.Stat.Type, config.Duration, v => v * config.Stat.Value),
                    _ => throw new ArgumentOutOfRangeException()
                };
                Debug.Log($"{caster.name} applied modifier {config.OperatorType} to {target.name}");

                target.Components.Get<StatsController>().Mediator.AddModifier(modifier);
            }
        }
    }
    [Serializable]
    public class InvincibilityEffect : Effect {
        [SerializeField] private bool _invincible;

        public override void Execute(BaseEntity caster, BaseEntity target,Vector3 from) {
            // Assuming the target has a method to become invincible
            // target.BecomeInvincible(_duration);
            Debug.Log($"{caster.name} made {target.name} invincible for {_invincible}");
            target.Components.Get<IHealth>().SetInvincibility(_invincible);
        }
    }
}