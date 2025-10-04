using System.Collections.Generic;
using Assets.Code.GamePlay.Abilities.General;
using Assets.Code.GamePlay.Armaments;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.GameplayStateMachine;
using Assets.Code.GamePlay.Player.PlayerStateMachine.States;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.PlayerStateMachine.StateConfigs
{
    [CreateAssetMenu(menuName = "State Configs/DiveStrike Config", fileName = "DiveStrikeStateConfig")]
    public class DiveStrikeMoveStateConfig : BaseMoveStateConfig
    {
        [field: SerializeField] public float DiveStrikeSpeed { get; private set; } = 35f;

        [field: SerializeField] public ArmamentConfig ArmamentConfig { get; private set; }
        public override List<StateConfiguration> GetStateConfiguration(ActorEntity playerEntity, AbilityInstance abilitiesInstance)
        {
            List<StateConfiguration> diveStrikeStateConfigurations = new List<StateConfiguration>()
            {
                GetDiveStrikeConfiguration(playerEntity,abilitiesInstance)
            };
            return diveStrikeStateConfigurations;
        }

        private StateConfiguration GetDiveStrikeConfiguration(ActorEntity playerEntity,AbilityInstance abilitiesInstance)
        {
            var divestrike = new DiveStrikeState(playerEntity, this, abilitiesInstance);
            StateConfiguration configuration = new StateConfiguration
            {
                State = divestrike,
                Transitions = new List<TransitionConfiguration>()
                {
                    // Add transitions here
                    TransitionConfiguration.GetConfiguration<FallingState,DiveStrikeState>(divestrike.FallingToDiveStrike),
                    TransitionConfiguration.GetConfiguration<RisingState,DiveStrikeState>(divestrike.RisingToDiveStrike),
                    TransitionConfiguration.GetConfiguration<DiveStrikeState,GroundedState>(divestrike.DiveStrikeToGrounded)
                }
            };
            return configuration;
        }
    }
}
