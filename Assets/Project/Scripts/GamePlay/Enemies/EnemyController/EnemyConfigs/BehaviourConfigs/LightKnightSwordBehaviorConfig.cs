using System.Collections.Generic;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Enemies.EnemyController.AttacksSet;
using Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs;
using Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs.StateSetConfigs;
using Assets.Code.GamePlay.Enemies.EnemyController.States;
using Assets.Code.GamePlay.GameplayStateMachine;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.EnemyConfigs
{
    [CreateAssetMenu(menuName = "Enemy Configs/Light Knight Sword Behavior Config", fileName = "LightKnightSwordBehaviorConfig")]
    public class LightKnightSwordBehaviorConfig : EnemyBehaviorConfig
    {
        [SerializeField] private IdleStateConfig _idleStateConfig;
        [SerializeField] private PatrollingStateConfig _patrollingStateConfig;
        [SerializeField] private ChasingStateConfig _chasingStateConfig;
        [SerializeField] private SimpleAttackStateConfig _simpleAttackStateConfig;

        [SerializeField] private SimpleAttacksSetConfig _simpleAttacksSetConfig;
        
        [SerializeField] private AttackPreparingConfig _attackPreparingStateConfig;
        [SerializeField] private AttackPreparingIdleConfig _attackPreparingIdleStateConfig;
        [SerializeField] private JumpAttackStateConfig _jumpAttackStateConfig;
        public override List<StateConfiguration> GetConfigurations(ActorEntity enemy)
        {
            List<StateConfiguration> configurations = new List<StateConfiguration>()
            {
                GetIdleConfiguration(enemy),
                GetPatrollingConfiguration(enemy),
                GetChaseConfiguration(enemy),
                GetAttackPreparationIdleConfiguration(enemy),
                //GetAttackConfiguration(enemy),
                GetJumpAttackConfiguration(enemy),

                 GetAttackPreparationConfiguration(enemy)
            };
            configurations.AddRange(GetAttacksSetConfiguration(enemy));
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
        
        private StateConfiguration GetAttackPreparationConfiguration(ActorEntity enemy)
        {
            var state = new AttackPreparingState(enemy, _attackPreparingStateConfig);
            StateConfiguration configuration = new StateConfiguration
            {
                State = state,
                Transitions = new List<TransitionConfiguration>()
                {
                    TransitionConfiguration.GetConfiguration<ChasingState, AttackPreparingState>(state.WaitingToAttack),
                    TransitionConfiguration.GetConfiguration<AttackPreparingState, ChasingState>(state.CanAttack),
                    TransitionConfiguration.GetConfiguration<AttackPreparingState, ChasingState>(state.OutOfRange),
                    TransitionConfiguration.GetConfiguration<AttackPreparingState, AttackPreparingIdleState>(state.TimerFinished,0.5f),

                }
            };
            return configuration;
        }
        private StateConfiguration GetAttackPreparationIdleConfiguration(ActorEntity enemy)
        {
            var state = new AttackPreparingIdleState(enemy, _attackPreparingIdleStateConfig);
            StateConfiguration configuration = new StateConfiguration
            {
                State = state,
                Transitions = new List<TransitionConfiguration>()
                {
                    TransitionConfiguration.GetConfiguration<AttackPreparingIdleState, AttackPreparingState>(state.TimerFinished,0.5f),
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
        private StateConfiguration GetJumpAttackConfiguration(ActorEntity enemy)
        {
            var state=new JumpAttackState(enemy,_jumpAttackStateConfig);
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
        private List<StateConfiguration> GetAttacksSetConfiguration(ActorEntity enemy)
        {
            List<StateConfiguration> configurations = new List<StateConfiguration>();
            
            SimpleAttackStateOrderer orderer = new SimpleAttackStateOrderer(_simpleAttacksSetConfig);
            for (int i = 0; i < _simpleAttacksSetConfig.StateConfigs.Length; i++)
            {
                var state = SimpleAttackState.GetVariation(enemy, _simpleAttacksSetConfig.StateConfigs[i],i);
                var stateIndex = i;
                StateConfiguration configuration = new StateConfiguration
                {
                    State = state,
                    Transitions = new List<TransitionConfiguration>()
                    {
                        TransitionConfiguration.GetConfiguration<ChasingState>(state,state.CanAttackCharacter,()=>orderer.AttackChosen(stateIndex)),
                        TransitionConfiguration.GetConfiguration<ChasingState>(state,state.AttackTimerFinished)
                    }
                };
                configurations.Add(configuration);
            }
       
            return configurations;
        }
        
    }

 


}