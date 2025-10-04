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
    [CreateAssetMenu(menuName = "State Configs/Dash State Config", fileName = "DashStateConfig")]
    public class DashMoveStateConfig : DashBaseStateConfig
    {

        [field: Space] 
        [field: SerializeField] public float AfterDashHoveringDuration { get; private set; } = 0.67f;
        [field: SerializeField] public float AfterDashHoveringGravity { get; private set; } = 9;
        [field: SerializeField] public float AfterDashHoveringSpeed { get; private set; } = 19;

        public override List<StateConfiguration> GetStateConfiguration(ActorEntity playerEntity,
            AbilityInstance abilitiesInstance)
        {
            List<StateConfiguration> jumpStateConfigurations = new List<StateConfiguration>()
            {
                GetDashConfiguration(playerEntity,abilitiesInstance),
                GetAfterDashHoveringConfiguration(playerEntity)
            };
            return jumpStateConfigurations;
        }

        protected override StateConfiguration GetDashConfiguration(ActorEntity playerEntity,AbilityInstance abilitiesInstance)
        {
            var dash = new DashLongState(playerEntity, this,abilitiesInstance);
            StateConfiguration configuration = new StateConfiguration
            {
                State = dash,
                Transitions = new List<TransitionConfiguration>()
                {
                    TransitionConfiguration.GetConfiguration<RisingState, DashLongState>(dash.AirToToDash),
                    TransitionConfiguration.GetConfiguration<FallingState, DashLongState>(dash.AirToToDash),
                    TransitionConfiguration.GetConfiguration<DashLongState, AfterDashHoveringState>(dash.EndOfDash),
                    TransitionConfiguration.GetConfiguration<GroundedState, DashLongState>(dash.GroundToDash),
                    TransitionConfiguration.GetConfiguration<WallClingingState, DashLongState>(dash.WallClingingToDash)
                }
            };
            return configuration;
        }

        private StateConfiguration GetAfterDashHoveringConfiguration(ActorEntity playerEntity)
        {
            var hovering = new AfterDashHoveringState(playerEntity, this);
            StateConfiguration configuration = new StateConfiguration
            {
                State = hovering,
                Transitions = new List<TransitionConfiguration>()
                {
                    TransitionConfiguration.GetConfiguration<AfterDashHoveringState, RisingState>(hovering.HoveringToRising),
                    TransitionConfiguration.GetConfiguration<AfterDashHoveringState, FallingState>(hovering.HoveringToFalling),
                    TransitionConfiguration.GetConfiguration<AfterDashHoveringState, GroundedState>(hovering.HoveringToGrounded),
                }
            };
            return configuration;
        }
    }
}