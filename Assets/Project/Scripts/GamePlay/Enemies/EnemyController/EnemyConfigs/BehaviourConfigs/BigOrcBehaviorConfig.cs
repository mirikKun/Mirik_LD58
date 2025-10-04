using System.Collections.Generic;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs;
using Assets.Code.GamePlay.Enemies.EnemyController.States;
using Assets.Code.GamePlay.GameplayStateMachine;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.EnemyConfigs
{
    [CreateAssetMenu(menuName = "Enemy Configs/Big Orc Behavior Config", fileName = "BigOrcBehaviorConfig")]
    public class BigOrcBehaviorConfig : EnemyBehaviorConfig
    {
        [SerializeField] private IdleStateConfig _idleStateConfig;
        [SerializeField] private PatrollingStateConfig _patrollingStateConfig;
        [SerializeField] private ChasingStateConfig _chasingStateConfig;
        [SerializeField] private SimpleAttackStateConfig _simpleAttackStateConfig;
        [SerializeField] private JumpAttackStateConfig _jumpAttackStateConfig;
        [SerializeField] private OnslaughtAttackConfig _onslaughtAttackConfig;
        [SerializeField] private RestStateConfig _restStateConfig;
        [SerializeField] private RoarAttackStateConfig _roarAttackStateConfig;

        public override List<StateConfiguration> GetConfigurations(ActorEntity enemyController)
        {
            List<StateConfiguration> configurations = new List<StateConfiguration>()
            {
                GetIdleConfiguration(enemyController),
                GetPatrollingConfiguration(enemyController),
                GetChaseConfiguration(enemyController),
                GetAttackConfiguration(enemyController),
                GetJumpAttackConfiguration(enemyController),
                
                GetRoarAttackConfiguration(enemyController),
                GetOnslaughtAttackConfiguration(enemyController),
                GetRestConfiguration(enemyController)
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

        private StateConfiguration GetChaseConfiguration(ActorEntity enemy)
        {
            var state = new ChasingState(enemy, _chasingStateConfig);
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

        private StateConfiguration GetAttackConfiguration(ActorEntity enemy)
        {
            var state = new SimpleAttackState(enemy, _simpleAttackStateConfig);
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

        private StateConfiguration GetJumpAttackConfiguration(ActorEntity enemy)
        {
            var state = new JumpAttackState(enemy, _jumpAttackStateConfig);
            StateConfiguration configuration = new StateConfiguration
            {
                State = state,
                Transitions = new List<TransitionConfiguration>()
                {
                    TransitionConfiguration.GetConfiguration<ChasingState, JumpAttackState>(state.CanAttack),
                    TransitionConfiguration.GetConfiguration<JumpAttackState, ChasingState>(state.Finished)
                }
            };
            return configuration;
        }
        private StateConfiguration GetRoarAttackConfiguration(ActorEntity enemy)
        {
            var state = new RoarAttackState(enemy, _roarAttackStateConfig);
            StateConfiguration configuration = new StateConfiguration
            {
                State = state,
                Transitions = new List<TransitionConfiguration>()
                {
                    TransitionConfiguration.GetConfiguration<ChasingState, RoarAttackState>(()=>enemy.Get<EnemyStatesContainer>().StatesInHistory<SimpleAttackState>(6)>=2),
                    TransitionConfiguration.GetConfiguration<RoarAttackState, OnslaughtAttackState>(state.AttackTimerFinished),
                    //TransitionConfiguration.GetConfiguration<JumpAttackState, ChasingState>(state.Finished)
                }
            };
            return configuration;
        }

        private StateConfiguration GetOnslaughtAttackConfiguration(ActorEntity enemy)
        {
            var state = new OnslaughtAttackState(enemy, _onslaughtAttackConfig);
            StateConfiguration configuration = new StateConfiguration
            {
                State = state,
                Transitions = new List<TransitionConfiguration>()
                {
                    TransitionConfiguration.GetConfiguration<OnslaughtAttackState, RestState>(state.HasReachedDestination),
                    //TransitionConfiguration.GetConfiguration<JumpAttackState, ChasingState>(state.Finished)
                }
            };
            return configuration;
        }

        private StateConfiguration GetRestConfiguration(ActorEntity enemy)
        {
            var state = new RestState(enemy, _restStateConfig);
            StateConfiguration configuration = new StateConfiguration
            {
                State = state,
                Transitions = new List<TransitionConfiguration>()
                {
                    TransitionConfiguration.GetConfiguration<RestState, IdleState>(state.TimerFinished),
                    //TransitionConfiguration.GetConfiguration<JumpAttackState, ChasingState>(state.Finished)
                }
            };
            return configuration;
        }
    }
}