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
    [CreateAssetMenu(menuName = "State Configs/Gravity Change State Config", fileName = "GravityChangeStateConfig")]

    public class GravityChangeMoveStateConfig : BaseMoveStateConfig
    {
        [field: SerializeField] public float RaycastNearDistance { get; private set; } = 4.5f;
        [field: SerializeField] public float ChangingDuration { get; private set; } = 0.5f;
        
        [field: SerializeField] public float GravityChangeSpeed { get; private set; } = 10f;
        
        [field: Space]
        [field: SerializeField] public bool GravityChangeJumpAvailable   { get; private set; }

        [field: SerializeField] public float GravityChangeJumpMaxHorizontalDistance { get; private set; } = 13f;
        [field: SerializeField] public float GravityChangeJumpMaxVerticalDistance { get; private set; } = 30f;

        public override List<StateConfiguration> GetStateConfiguration(ActorEntity playerEntity,
            AbilityInstance abilitiesInstance)
        {
            List<StateConfiguration> jumpStateConfigurations = new List<StateConfiguration>()
            {
                GetPounceConfiguration(playerEntity,abilitiesInstance),
                GetGravityChangePreparingConfiguration(playerEntity)
            };
            return jumpStateConfigurations;
        }

        private StateConfiguration GetPounceConfiguration(ActorEntity playerEntity,AbilityInstance abilitiesInstance)
        {
            var gravityChange = new GravityChangeState(playerEntity, this,abilitiesInstance);
            StateConfiguration configuration = new StateConfiguration
            {
                State = gravityChange,
                Transitions = new List<TransitionConfiguration>()
                {
                    TransitionConfiguration.GetConfiguration<GroundedState, GravityChangeState>(gravityChange.GroundedToGravityChange),
                    TransitionConfiguration.GetConfiguration<GravityChangeState, GroundedState>(gravityChange.GravityChangeToGrounded),
                    TransitionConfiguration.GetConfiguration<GravityChangeState, FallingState>(gravityChange.GravityChangeToFalling)
                }
            };
            return configuration;
        }
        private StateConfiguration GetGravityChangePreparingConfiguration(ActorEntity playerEntity)
        {
            var state = new GravityChangeJumpState(playerEntity, this);
            StateConfiguration configuration = new StateConfiguration
            {
                State = state,
                Transitions = new List<TransitionConfiguration>()
                {
                    TransitionConfiguration.GetConfiguration<GroundedState, GravityChangeJumpState>(state.GroundedToGravityJumpChangePreparing),
                    TransitionConfiguration.GetConfiguration<GravityChangeJumpState, GroundedState>(state.GravityJumpChangePreparingToGrounded),
                }
            };
            return configuration;
        }
    }
}