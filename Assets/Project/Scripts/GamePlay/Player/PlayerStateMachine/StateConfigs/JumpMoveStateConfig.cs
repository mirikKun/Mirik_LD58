using System.Collections.Generic;
using Assets.Code.GamePlay.Abilities.General;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.GameplayStateMachine;
using Assets.Code.GamePlay.Player.PlayerStateMachine.States;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.PlayerStateMachine.StateConfigs
{
    
    [CreateAssetMenu(menuName = "State Configs/Jump State Config", fileName = "JumpStateConfig")]
    public class JumpMoveStateConfig:BaseMoveStateConfig
    {
        [field: SerializeField] public float JumpSpeed { get; private set; } = 10f;
        [field: SerializeField] public float JumpDuration { get; private set; } = 0.2f;

        public override List<StateConfiguration> GetStateConfiguration(ActorEntity playerEntity,
            AbilityInstance abilitiesInstance)
        {
            List<StateConfiguration> jumpStateConfigurations = new List<StateConfiguration>()
            {
                GetJumpingConfiguration(playerEntity,abilitiesInstance)
            };
            return jumpStateConfigurations;
        }
        
        private StateConfiguration GetJumpingConfiguration(ActorEntity playerEntity,AbilityInstance abilitiesInstance)
        {
            var jumping = new JumpingState(playerEntity,this,abilitiesInstance);
            StateConfiguration configuration = new StateConfiguration
            {
                State = jumping,
                Transitions = new List<TransitionConfiguration>()
                {
                    TransitionConfiguration.GetConfiguration<GroundedState,JumpingState>(jumping.GroundedToJumping),
                    TransitionConfiguration.GetConfiguration<JumpingState,RisingState>(jumping.JumpingToRising),
                    TransitionConfiguration.GetConfiguration<JumpingState,FallingState>(jumping.JumpingToFalling)
                }
            };
            return configuration;
        }
    }
}