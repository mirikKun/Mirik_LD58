using System.Collections.Generic;
using Assets.Code.GamePlay.Common.Entity;
using Project.Scripts.GamePlay.Common.GameplayStateMachine;
using Project.Scripts.GamePlay.Enemies.EnemyController.StateConfigs;
using Project.Scripts.GamePlay.Enemies.EnemyController.States;
using UnityEngine;

namespace Project.Scripts.GamePlay.Enemies.EnemyController.EnemyConfigs.BehaviourConfigs
{
    [CreateAssetMenu(fileName = "FlamingRelicConfig", menuName = "Enemy Configs/FlamingRelicConfig")]
    public class FlamingRelicConfig : EnemyBehaviorConfig
    {
        [SerializeField] private IdleStateConfig _idleStateConfig;
        [SerializeField] private ArmamentSpawnStateConfig _armamentSpawnStateConfig;
        [SerializeField] private PointsFlyPatrollingStateConfig _pointsFlyPatrollingStateConfig;
        [SerializeField] private KnockbackStateConfig _knockbackStateConfig;
        [SerializeField] private GravityFallStunStateConfig  _gravityFallStunStateConfig;

        public override List<StateConfiguration> GetConfigurations(ActorEntity enemyController)
        {
            List<StateConfiguration> configurations = new List<StateConfiguration>()
            {
                GetIdleConfiguration(enemyController),
                GetPatrollingConfiguration(enemyController),
                GetArmamentSpawnConfiguration(enemyController),
                GetFallStunConfiguration(enemyController),
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
                    TransitionConfiguration.GetConfiguration<IdleState, PointsFlyPatrollingState>(state.TimerFinished),

                }
            };
            return configuration;
        }
        private StateConfiguration GetPatrollingConfiguration(ActorEntity enemy)
        {
            var state = new PointsFlyPatrollingState(enemy, _pointsFlyPatrollingStateConfig);
            StateConfiguration configuration = new StateConfiguration
            {
                State = state,
                Transitions = new List<TransitionConfiguration>()
                {
                    TransitionConfiguration.GetConfiguration<PointsFlyPatrollingState, IdleState>(state.HasReachedDestination)
                }
            };
            return configuration;
        }
        private StateConfiguration GetArmamentSpawnConfiguration(ActorEntity enemy)
        {

            var state = new ArmamentSpawnState(enemy, _armamentSpawnStateConfig);
            StateConfiguration configuration = new StateConfiguration
            {
                State = state,
                Transitions = new List<TransitionConfiguration>()
                {
                    TransitionConfiguration.GetConfiguration<PointsFlyPatrollingState, ArmamentSpawnState>(state.CanAttackAndCooldownPassed),
                    TransitionConfiguration.GetConfiguration<IdleState, ArmamentSpawnState>(state.CanAttackAndCooldownPassed),
                    TransitionConfiguration.GetConfiguration<ArmamentSpawnState, PointsFlyPatrollingState>(state.AttackTimerFinished),
                }
            };
            return configuration;
        }
        private StateConfiguration GetFallStunConfiguration(ActorEntity enemy)
        {

            var state = new GravityFallStunState(enemy, _gravityFallStunStateConfig);
            StateConfiguration configuration = new StateConfiguration
            {
                State = state,
                Transitions = new List<TransitionConfiguration>()
                {
                    TransitionConfiguration.GetConfiguration<PointsFlyPatrollingState, GravityFallStunState>(state.IsForced),
                    TransitionConfiguration.GetConfiguration<IdleState, GravityFallStunState>(state.IsForced),
                    TransitionConfiguration.GetConfiguration<ArmamentSpawnState, GravityFallStunState>(state.IsForced),
                    TransitionConfiguration.GetConfiguration<GravityFallStunState, PointsFlyPatrollingState>(state.StunTimerFinished),
                }
            };
            return configuration;
        }
    }
}