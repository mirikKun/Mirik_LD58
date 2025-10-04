using System.Collections.Generic;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs;
using Assets.Code.GamePlay.Enemies.EnemyController.States;
using Assets.Code.GamePlay.GameplayStateMachine;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.EnemyConfigs
{
    [CreateAssetMenu(menuName = "Enemy Configs/Light Knight Bow Behavior Config", fileName = "LightKnightBowBehaviorConfig")]

    public class LightKnightBowBehaviorConfig : EnemyBehaviorConfig
    {
        [SerializeField] private IdleStateConfig _idleStateConfig;
        [SerializeField] private PatrollingStateConfig _patrollingStateConfig;
        [SerializeField] private SimpleAttackStateConfig _simpleAttackStateConfig;

        public override List<StateConfiguration> GetConfigurations(ActorEntity enemyController)
        {
            List<StateConfiguration> configurations = new List<StateConfiguration>()
            {
                GetIdleConfiguration(enemyController),
                GetPatrollingConfiguration(enemyController),
                GetAttackConfiguration(enemyController),
            };
            return configurations;
        }
        
        private StateConfiguration GetIdleConfiguration(ActorEntity enemy)
        {

            var state = new IdleState(enemy, _idleStateConfig);
            StateConfiguration configuration = new StateConfiguration
            {
                State = state,
                Transitions = new List<TransitionConfiguration>()
                {
                    TransitionConfiguration.GetConfiguration<IdleState, PatrollingState>(state.TimerFinished),

                }
            };
            return configuration;
        }
        private StateConfiguration GetPatrollingConfiguration(ActorEntity enemy)
        {
            var state = new PatrollingState(enemy, _patrollingStateConfig);
            StateConfiguration configuration = new StateConfiguration
            {
                State = state,
                Transitions = new List<TransitionConfiguration>()
                {
                    TransitionConfiguration.GetConfiguration<PatrollingState, IdleState>(enemy.Get<EnemyMover>().HasReachedDestination)
                }
            };
            return configuration;
        }
        private StateConfiguration GetAttackConfiguration(ActorEntity enemy)
        {
            var state=new SimpleAttackState(enemy,_simpleAttackStateConfig);
            StateConfiguration configuration = new StateConfiguration
            {
                State = state,
                Transitions = new List<TransitionConfiguration>()
                {
                    TransitionConfiguration.GetConfiguration<IdleState, SimpleAttackState>(state.CanAttackAndCooldownPassed),
                    TransitionConfiguration.GetConfiguration<PatrollingState, SimpleAttackState>(state.CanAttackAndCooldownPassed),
                    TransitionConfiguration.GetConfiguration<SimpleAttackState, PatrollingState>(state.AttackTimerFinished)
                }
            };
            return configuration;
        }     
    }
}