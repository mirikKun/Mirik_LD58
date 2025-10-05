using System.Collections.Generic;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Enemies.EnemyController.EnemyConfigs;
using Assets.Code.GamePlay.GameplayStateMachine;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController
{

    public class EnemyStatesContainer:EntityComponent
    {

        [SerializeField] private EnemyBehaviorConfig _enemyBehaviorConfig;
        
        private StateMachine _stateMachine;


        private void Start()
        {
            SetupStateMachine();
            if(Entity.TryGet<EnemyCombat>(out var combat))
            {
                combat.Init();
            }
        }



        private void SetupStateMachine()
        {
            _stateMachine = new StateMachine();
            StateMachineFactory factory = new StateMachineFactory(_stateMachine);
            List<StateConfiguration> configurations = _enemyBehaviorConfig.GetConfigurations(Entity);
            factory.SetupStateMachine(configurations,configurations[0].State.GetType());

        }

        public void TickUpdate(float deltaTime)
        {
            _stateMachine.Update(deltaTime);
        }

        public void FixedTickUpdate(float deltaTime) => _stateMachine.FixedUpdate(deltaTime);


        
        public int StatesInHistory<T>(int statesBack = 6)
        {
            int statesCount = _stateMachine.PreviousStates.Count;
            int count = 0;
            for (int i = 0; i < statesBack; i++)
            {
                if (statesCount - 1 - i < 0) return count;
                
                if(_stateMachine.PreviousStates[statesCount - 1 - i] is T) count++;
            }
            return count;
        }
    }
}