using System;
using System.Collections.Generic;
using Assets.Code.GamePlay.Abilities.Systems;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.GameplayStateMachine;
using Assets.Code.GamePlay.Player.Abilities.Configs;
using Assets.Code.GamePlay.Player.Controller;
using Assets.Code.GamePlay.Player.PlayerStateMachine.States;
using UnityEngine;
using Zenject;

namespace Assets.Code.GamePlay.Player.PlayerStateMachine
{
    public class PlayerStateMachineContainer:EntityComponent
    {
        private StateMachine _stateMachine;
        private IAbilitiesSystem _abilitySystem;

        [Inject]
        private void Construct(IAbilitiesSystem abilitySystem)
        {
            _abilitySystem = abilitySystem;
        }

        private void OnDestroy()
        {
            _stateMachine?.Dispose();
        }

        public void Tick(float deltaTime)
        {
            _stateMachine.Update(deltaTime);
        }
        public void FixedTick(float fixedDeltaTime)
        {
            _stateMachine.FixedUpdate(fixedDeltaTime);
        }

     

        public void SetupStateMachine()
        {
            _stateMachine?.Dispose();
            _stateMachine = new StateMachine();
            List<StateConfiguration> configurations = GetStateConfigurations(Entity, _abilitySystem);
            StateMachineFactory factory = new StateMachineFactory(_stateMachine);
            factory.SetupStateMachine(configurations, typeof(GroundedState));
        }

        public List<StateConfiguration> GetStateConfigurations(ActorEntity playerEntity, IAbilitiesSystem abilitiesSystem)
        {
            List<StateConfiguration> stateConfigurations=new List<StateConfiguration>();
         
            foreach (var ability in abilitiesSystem.Abilities)
            {
                if(ability.AbilityConfig is MovingAbilityConfig movingAbility)
                    stateConfigurations.AddRange(movingAbility.MovementMoveStateConfig.GetStateConfiguration(playerEntity,ability));
            }
            
            return stateConfigurations;
        }
        public bool HaveStateInHistory<T>(int statesBack = 6)
        {
            int statesCount = _stateMachine.PreviousStates.Count;
            for (int i = 0; i < statesBack; i++)
            {
                if (statesCount - 1 - i < 0) return false;

                if (_stateMachine.PreviousStates[statesCount - 1 - i] is T) return true;
            }

            return false;
        }

        public bool HaveStateBeforeStateInHistory<T, TBefore>(int statesBack = 10)
        {
            int statesCount = _stateMachine.PreviousStates.Count;

            for (int i = 0; i < statesBack; i++)
            {
                if (statesCount - 1 - i < 0) return false;

                if (_stateMachine.PreviousStates[statesCount - 1 - i] is T) return true;
                if (_stateMachine.PreviousStates[statesCount - 1 - i] is TBefore) return false;
            }

            return false;
        }

        public bool IsGroundedState() => _stateMachine.CurrentState is GroundedState or SlopeSlidingState;

        private void At(IState from, IState to, Func<bool> condition) =>
            _stateMachine.AddTransition(from, to, condition);

        private void Any<T>(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);

    }
}