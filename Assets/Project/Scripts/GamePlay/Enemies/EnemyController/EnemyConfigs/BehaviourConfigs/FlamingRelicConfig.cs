using System.Collections.Generic;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs;
using Assets.Code.GamePlay.Enemies.EnemyController.States;
using Assets.Code.GamePlay.GameplayStateMachine;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.EnemyConfigs
{
    [CreateAssetMenu(fileName = "FlamingRelicConfig", menuName = "Enemy Configs  public override List<StateConfiguration> GetConfigurations(ActorEntity enemyController)\n        {\n            List<StateConfiguration> configurations = new List<StateConfiguration>()\n            {\n                GetIdleConfiguration(enemyController),\n                GetPatrollingConfiguration(enemyController),\n                GetAttackConfiguration(enemyController),\n            };\n            return configurations;\n        }/FlamingRelicConfig")]
    public class FlamingRelicConfig : EnemyBehaviorConfig
    {
        [SerializeField] private IdleStateConfig _idleStateConfig;
        [SerializeField] private ArmamentSpawnStateConfig _armamentSpawnStateConfig;
        [SerializeField] private PointsFlyPatrollingStateConfig _pointsFlyPatrollingStateConfig;
        [SerializeField] private KnockbackStateConfig _knockbackStateConfig;

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
                    TransitionConfiguration.GetConfiguration<IdleState, PointsFlyPatrollingState>(state.TimerFinished),

                }
            };
            return configuration;
        }
    }
}