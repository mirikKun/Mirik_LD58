using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs;
using Assets.Code.GamePlay.GameplayStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Code.GamePlay.Enemies.EnemyController.States
{
    public class OnslaughtAttackState:IState
    {
        private readonly ActorEntity _enemy;
        private readonly OnslaughtAttackConfig _config;
        private Vector3 _targetPosition;
        private float _passedDistance;
        
        private EnemyMover Mover => _enemy.Get<EnemyMover>();
        private EnemyCombat Combat => _enemy.Get<EnemyCombat>();

        
        public OnslaughtAttackState(ActorEntity enemy, OnslaughtAttackConfig config)
        {
            _enemy = enemy;
            _config = config;
        }
        
        public void OnEnter()
        {
            _enemy.Get<EnemyAnimator>().StartAnimation(_config.AnimationHash,0);
            Vector3 targetPosition = GetTargetPosition();
            Mover.SetDestination(targetPosition,_config.Speed);
            Combat.Attack(_config.AttackId);
            
            
        }

       public void OnExit()
        {
            Mover.StopAgent();
            Combat.StopAttack(_config.AttackId);

        }
        public Vector3 GetTargetPosition()
        {
     
            
            Vector3 startPos = Mover.Tr.position;
            //Vector3 characterPosition = _enemyController.EnemyMover.GetPathPoints()[1];
            Vector3 characterPosition = _enemy.Get<CharacterDetector>().DetectedCharacter.position;
            Vector3 direction = (characterPosition - startPos).normalized;
            float range = _config.Range;
    
            Vector3 targetPoint = startPos + direction * range;
    
            NavMeshHit hit;
            if (NavMesh.Raycast(startPos, targetPoint, out hit, NavMesh.AllAreas))
            {
                return hit.position;
            }
            else
            {
                NavMeshHit finalHit;
                if (NavMesh.SamplePosition(targetPoint, out finalHit, 5.0f, NavMesh.AllAreas))
                {
                    return finalHit.position;
                }
                else
                {
                    // Если целевая точка не на NavMesh, найдем ближайшую точку на NavMesh
                    NavMesh.FindClosestEdge(targetPoint, out finalHit, NavMesh.AllAreas);
                    return finalHit.position;
                }
            }
        }

        public bool CanAttack()
        {
            return true;
        }
        public bool HasReachedDestination()
        {
            return Mover.HasReachedDestination();
        }
    }
}