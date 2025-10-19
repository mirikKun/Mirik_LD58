using System.Collections.Generic;
using Assets.Code.GamePlay.Common.Entity;
using Project.Scripts.GamePlay.Armaments.Configs;
using Project.Scripts.GamePlay.Common.GameplayStateMachine;
using Project.Scripts.GamePlay.Player.Abilities.General;
using Project.Scripts.GamePlay.Player.PlayerStateMachine.States;
using UnityEngine;

namespace Project.Scripts.GamePlay.Player.PlayerStateMachine.StateConfigs
{
    [CreateAssetMenu(menuName = "State Configs/Dash Attack State Config", fileName = "DashAttackStateConfig")]

    public class DashAttackStateConfig:DashBaseStateConfig
    {
        [field:Header("Armament")]
        [field: SerializeField] public ArmamentConfig ArmamentConfig { get; private set; }
        
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
            var dash = new DashAttackState(playerEntity, this,abilitiesInstance);
            StateConfiguration configuration = new StateConfiguration
            {
                State = dash,
                Transitions = new List<TransitionConfiguration>()
                {
                    //TransitionConfiguration.GetConfiguration<DashState,GroundedState>(dash.DashToGround),
                    TransitionConfiguration.GetConfiguration<RisingState, DashAttackState>(dash.AirToToDash),
                    TransitionConfiguration.GetConfiguration<FallingState, DashAttackState>(dash.AirToToDash),
                    // TransitionConfiguration.GetConfiguration<DashState,RisingState>(dash.DashToRising),
                    // TransitionConfiguration.GetConfiguration<DashState,FallingState>(dash.DashToFalling),
                    TransitionConfiguration.GetConfiguration<DashAttackState, FallingState>(dash.EndOfDash),
                    TransitionConfiguration.GetConfiguration<GroundedState, DashAttackState>(dash.GroundToDash),
                    TransitionConfiguration.GetConfiguration<WallClingingState, DashAttackState>(dash.WallClingingToDash)
                }
            };
            return configuration;
        }

    }
}