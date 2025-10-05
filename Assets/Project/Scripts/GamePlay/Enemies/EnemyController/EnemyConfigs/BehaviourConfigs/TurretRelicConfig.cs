using System.Collections.Generic;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs;
using Assets.Code.GamePlay.Enemies.EnemyController.States;
using Assets.Code.GamePlay.GameplayStateMachine;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.EnemyConfigs
{
    [CreateAssetMenu(fileName = "TurretRelicConfig", menuName = "Enemy Configs/TurretRelicConfig")]
    public class TurretRelicConfig: EnemyBehaviorConfig
    {
            [SerializeField] private IdleStateConfig _idleStateConfig;
        [SerializeField] private ArmamentSpawnStateConfig _armamentSpawnStateConfig;
        [SerializeField] private KnockbackStateConfig _knockbackStateConfig;

        public override List<StateConfiguration> GetConfigurations(ActorEntity enemyController)
        {
            List<StateConfiguration> configurations = new List<StateConfiguration>()
            {
                GetIdleConfiguration(enemyController),
                GetArmamentSpawnConfiguration(enemyController),
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
                    TransitionConfiguration.GetConfiguration<IdleState, ArmamentSpawnState>(state.CanAttackAndCooldownPassed),
                    TransitionConfiguration.GetConfiguration<ArmamentSpawnState, IdleState>(state.AttackTimerFinished),
                }
            };
            return configuration;
        }
    }
}