using System.Collections.Generic;
using Assets.Code.GamePlay.Common.Entity;
using Project.Scripts.GamePlay.Common.GameplayStateMachine;
using Project.Scripts.GamePlay.Enemies.EnemyController.StateConfigs;
using Project.Scripts.GamePlay.Enemies.EnemyController.States;
using UnityEngine;

namespace Project.Scripts.GamePlay.Enemies.EnemyController.EnemyConfigs.BehaviourConfigs
{
    [CreateAssetMenu(menuName = "Enemy Configs/Skeleton Behavior Config", fileName = "SkeletonBehaviorConfig")]
    public class SkeletonBehaviorConfig:EnemyBehaviorConfig
    {
        [SerializeField] private IdleStateConfig _idleStateConfig;
        [SerializeField] private PatrollingStateConfig _patrollingStateConfig;
        [SerializeField] private ChasingStateConfig _chasingStateConfig;
        [SerializeField] private SimpleAttackStateConfig _simpleAttackStateConfig;
        [SerializeField] private AttackPreparingConfig _attackPreparingStateConfig;

        public override List<StateConfiguration> GetConfigurations(ActorEntity enemy)
        {
     
            
            List<StateConfiguration> configurations = new List<StateConfiguration>()
            {
                GetIdleConfiguration(enemy),
                GetPatrollingConfiguration(enemy),
                GetChaseConfiguration(enemy),
                GetAttackConfiguration(enemy),
               // GetAttackPreparationConfiguration(enemy)
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

        private StateConfiguration GetChaseConfiguration(ActorEntity enemy)
        {
            var state=new ChasingState(enemy,_chasingStateConfig);
            StateConfiguration configuration = new StateConfiguration
            {
                State = state,
                Transitions = new List<TransitionConfiguration>()
                {
                    TransitionConfiguration.GetConfiguration<IdleState, ChasingState>(state.DetectCharacter),
                    TransitionConfiguration.GetConfiguration<PatrollingState, ChasingState>(state.DetectCharacter),
                    TransitionConfiguration.GetConfiguration<ChasingState, IdleState>(state.LoseCharacter)
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
                    TransitionConfiguration.GetConfiguration<ChasingState, SimpleAttackState>(state.CanAttackCharacter),
                    TransitionConfiguration.GetConfiguration<SimpleAttackState, ChasingState>(state.AttackTimerFinished)
                }
            };
            return configuration;
        }
        
    }
}