using System.Collections.Generic;
using Assets.Code.GamePlay.Common.Entity;
using FMODUnity;
using Project.Scripts.GamePlay.Common.GameplayStateMachine;
using Project.Scripts.GamePlay.Player.Abilities.General;
using UnityEngine;

namespace Project.Scripts.GamePlay.Player.PlayerStateMachine.StateConfigs
{
    public abstract class DashBaseStateConfig: BaseMoveStateConfig
    {
        [field: SerializeField] public float DashSpeed { get; private set; } = 50f;
        [field: SerializeField] public float DashExitSpeed { get; private set; } = 8f;
        [field: SerializeField] public float DashDuration { get; private set; } = 0.24f;

        [field: SerializeField] public float UpdatedFov { get; private set; } = 77;
        [field:Header("Sounds")]
        [field: SerializeField] public EventReference Sound { get; private set; }
        
        
        
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