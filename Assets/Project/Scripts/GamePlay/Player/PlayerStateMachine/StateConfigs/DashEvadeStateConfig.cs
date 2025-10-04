using System.Collections.Generic;
using Assets.Code.GamePlay.Abilities.General;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.GameplayStateMachine;
using Assets.Code.GamePlay.Player.PlayerStateMachine.States;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.PlayerStateMachine.StateConfigs
{
    [CreateAssetMenu(menuName = "State Configs/Dash Evade State Config", fileName = "DashEvadeStateConfig")]
    public class DashEvadeStateConfig: DashBaseStateConfig
    {
        public override List<StateConfiguration> GetStateConfiguration(ActorEntity playerEntity,
            AbilityInstance abilitiesInstance)
        {
            List<StateConfiguration> jumpStateConfigurations = new List<StateConfiguration>()
            {
                GetDashConfiguration(playerEntity,abilitiesInstance)
            };
            return jumpStateConfigurations;
        }
        protected override StateConfiguration GetDashConfiguration(ActorEntity playerEntity,AbilityInstance abilitiesInstance)
        {
            var dash = new DashEvadeState(playerEntity, this,abilitiesInstance);
            StateConfiguration configuration = new StateConfiguration
            {
                State = dash,
                Transitions = new List<TransitionConfiguration>()
                {
                    //TransitionConfiguration.GetConfiguration<DashState,GroundedState>(dash.DashToGround),
                    TransitionConfiguration.GetConfiguration<RisingState, DashEvadeState>(dash.AirToToDash),
                    TransitionConfiguration.GetConfiguration<FallingState, DashEvadeState>(dash.AirToToDash),
                    // TransitionConfiguration.GetConfiguration<DashState,RisingState>(dash.DashToRising),
                    // TransitionConfiguration.GetConfiguration<DashState,FallingState>(dash.DashToFalling),
                    TransitionConfiguration.GetConfiguration<DashEvadeState, FallingState>(dash.EndOfDash),
                    TransitionConfiguration.GetConfiguration<GroundedState, DashEvadeState>(dash.GroundToDash),
                    TransitionConfiguration.GetConfiguration<WallClingingState, DashEvadeState>(dash.WallClingingToDash)
                }
            };
            return configuration;
        }

    }
}