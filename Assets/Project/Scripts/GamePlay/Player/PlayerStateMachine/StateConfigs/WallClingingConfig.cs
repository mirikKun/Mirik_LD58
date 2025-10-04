using System.Collections.Generic;
using Assets.Code.GamePlay.Abilities.General;
using Assets.Code.GamePlay.Abilities.Systems;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.GameplayStateMachine;
using Assets.Code.GamePlay.Player.Controller;
using Assets.Code.GamePlay.Player.PlayerStateMachine.States;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.PlayerStateMachine.StateConfigs
{
    [CreateAssetMenu(menuName = "State Configs/Wall Clinging State Config", fileName = "WallClingingStateConfig")]

    public class WallClingingConfig:BaseMoveStateConfig

    {
        public override List<StateConfiguration> GetStateConfiguration(ActorEntity playerEntity,
            AbilityInstance abilitiesInstance)
        {
            List<StateConfiguration> jumpStateConfigurations = new List<StateConfiguration>()
            {
                GetClingingConfiguration(playerEntity)
            };
            return jumpStateConfigurations;
        }
        private StateConfiguration GetClingingConfiguration(ActorEntity playerEntity)
        {
            var clinging = new WallClingingState(playerEntity);
            StateConfiguration configuration = new StateConfiguration
            {
                State = clinging,
                Transitions = new List<TransitionConfiguration>()
                {
                    TransitionConfiguration.GetConfiguration<FallingState,WallClingingState>(clinging.FallingToClinging),
                    TransitionConfiguration.GetConfiguration<RisingState,WallClingingState>(clinging.RisingToClinging),
                    TransitionConfiguration.GetConfiguration<WallClingingState,PounceState>(clinging.ClingingToPounce),
                    TransitionConfiguration.GetConfiguration<WallClingingState,FallingState>(clinging.ClingingToFalling)
                }
            };
            return configuration;
        }
    }
}