using System.Collections.Generic;
using Assets.Code.GamePlay.Abilities.General;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.GameplayStateMachine;
using Assets.Code.GamePlay.Player.PlayerStateMachine.States;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.PlayerStateMachine.StateConfigs
{
    [CreateAssetMenu(menuName = "State Configs/Climbing Over Config", fileName = "ClimbingOverStateConfig")]

    public class ClimbingOverMoveStateConfig:BaseMoveStateConfig
    {
        [field: SerializeField] public float ClimbingOverSpeed { get; private set; } = 7f;
        [field: SerializeField] public float HorizontalSpeedReduction { get; private set; } = 0.3f;

        public override List<StateConfiguration> GetStateConfiguration(ActorEntity playerEntity,
            AbilityInstance abilitiesInstance)
        {
            List<StateConfiguration> jumpStateConfigurations = new List<StateConfiguration>()
            {
                GetClimbingOverConfiguration(playerEntity)
            };
            return jumpStateConfigurations;
        }
        
        private StateConfiguration GetClimbingOverConfiguration(ActorEntity playerEntity)
        {
            var climbingOver = new ClimbingOverState(playerEntity,this);
            StateConfiguration configuration = new StateConfiguration
            {
                State = climbingOver,
                Transitions = new List<TransitionConfiguration>()
                {
                    TransitionConfiguration.GetConfiguration<FallingState,ClimbingOverState>(climbingOver.FallingToClimbingOver),
                    TransitionConfiguration.GetConfiguration<RisingState,ClimbingOverState>(climbingOver.RisingToClimbingOver),
                    TransitionConfiguration.GetConfiguration<GroundedState,ClimbingOverState>(climbingOver.GroundedToClimbingOver),
                    TransitionConfiguration.GetConfiguration<ClimbingOverState,RisingState>(climbingOver.ClimbingOverToRising),
                    TransitionConfiguration.GetConfiguration<ClimbingOverState,FallingState>(climbingOver.ClimbingOverToFalling),
                    
                }
            };
            return configuration;
        }
    }
}