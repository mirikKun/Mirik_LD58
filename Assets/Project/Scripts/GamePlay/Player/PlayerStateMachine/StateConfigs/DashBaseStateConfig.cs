using System.Collections.Generic;
using Assets.Code.GamePlay.Abilities.General;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.GameplayStateMachine;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.PlayerStateMachine.StateConfigs
{
    public abstract class DashBaseStateConfig: BaseMoveStateConfig
    {
        [field: SerializeField] public float DashSpeed { get; private set; } = 50f;
        [field: SerializeField] public float DashExitSpeed { get; private set; } = 8f;
        [field: SerializeField] public float DashDuration { get; private set; } = 0.24f;

        [field: SerializeField] public float UpdatedFov { get; private set; } = 77;
        public override List<StateConfiguration> GetStateConfiguration(ActorEntity playerEntity,
            AbilityInstance abilitiesInstance)
        {
            List<StateConfiguration> jumpStateConfigurations = new List<StateConfiguration>()
            {
                GetDashConfiguration(playerEntity,abilitiesInstance)
            };
            return jumpStateConfigurations;
        }

        protected abstract StateConfiguration GetDashConfiguration(ActorEntity playerEntity, AbilityInstance abilitiesInstance);
    }
}