using Assets.Code.GamePlay.Common.Entity;
using Project.Scripts.GamePlay.Player.Abilities.Configs;
using Project.Scripts.Sounds;
using UnityEngine;

namespace Project.Scripts.GamePlay.Player.Abilities.General
{
    public class SelfEffectApplierAbility : IAbility
    {
        private SelfEffectApplierAbilityConfig _config;
        private ActorEntity _caster;

        public void SetConfig(SelfEffectApplierAbilityConfig config)
        {
            _config = config;
        }

        public void Init(ActorEntity caster)
        {
            _caster = caster;
        }

        public void OnInput(bool pressed)
        {
            if (pressed)
            {
                Execute();
            }
        }

        public void Execute()
        {
            Debug.Log($"SelfEffectApplierAbility.Execute() called for {_caster.name}");
            _caster.Get<SoundSource>().PlaySound(_config.Sound);
            _config.Effects.ForEach(effect => effect.Execute(_caster, _caster,_caster.GetPosition()));
        }
    }
}