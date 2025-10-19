using Assets.Code.GamePlay.Common.Entity;
using Project.Scripts.GamePlay.Common.GameplayStateMachine;
using Project.Scripts.GamePlay.Enemies.EnemyController.StateConfigs;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts.GamePlay.Enemies.EnemyController.States
{
    public class PatrollingState:IState
    {
        private readonly ActorEntity _enemy;
        private readonly PatrollingStateConfig _patrollingConfig;
        private Vector3 _startPosition;
        private EnemyMover Mover => _enemy.Get<EnemyMover>();
        private EnemyCombat Combat => _enemy.Get<EnemyCombat>();
        public PatrollingState(ActorEntity enemy, PatrollingStateConfig patrollingConfig)
        {
            _enemy = enemy;
            _patrollingConfig = patrollingConfig;
            _startPosition = Mover.Tr.position;
        }

        public void OnEnter()
        {
            Combat.CombatData.HasDetectedCharacter = false;
            _enemy.Get<EnemyAnimator>().StartAnimation(_patrollingConfig.AnimationHash);
            
            var randomDirection = Random.insideUnitSphere*_patrollingConfig.Range;
            randomDirection+=_startPosition;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, _patrollingConfig.Range, 1);
                
            var finalPosition = hit.position;
            Mover.SetDestination(finalPosition, _patrollingConfig.Speed.RandomValueInRange);
        }

        public void OnExit()
        {
            Mover.StopAgent();
            if (_enemy.Get<CharacterDetector>().CanDetectCharacter)
            {
                Combat.CombatData.HasDetectedCharacter = true;

            }
        }
    }
}