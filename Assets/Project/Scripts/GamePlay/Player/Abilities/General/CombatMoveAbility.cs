using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.GameplayStateMachine;
using Assets.Code.GamePlay.Player.Abilities.Configs;
using Assets.Code.GamePlay.Player.PlayerStateMachine;
using UnityEngine;

namespace Assets.Code.GamePlay.Abilities.General
{
    public class CombatMoveAbility : IAbility
    {
        private CombatMoveAbilityConfig _config;
        private ActorEntity _caster;

        public void SetConfig(CombatMoveAbilityConfig config)
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
        
           
        }

        public void Stop()
        {
            
        }
    }
}