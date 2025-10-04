using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs;
using Assets.Code.GamePlay.GameplayStateMachine;
using ImprovedTimers;
using ImprovedTimers.Project.Scripts.Utils.Timers;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.States
{
    public class AttackPreparingState : IState
    {
        private Vector3 _characterPosition;
        private Vector3 _characterPredictedPosition;
        private readonly CountdownTimer _directionChangeTimer;
        private int _walkAroundDirection;
        private float _speed;
        private float _range;
        private float _maxRange;

        private readonly ActorEntity _enemy;
        private readonly AttackPreparingConfig _attackPreparingConfig;
        private EnemyMover Mover => _enemy.Get<EnemyMover>();
        private EnemyCombat Combat => _enemy.Get<EnemyCombat>();
        private EnemyAnimator Animator => _enemy.Get<EnemyAnimator>();
        private CharacterDetector Detector => _enemy.Get<CharacterDetector>();


        public AttackPreparingState(ActorEntity enemy, AttackPreparingConfig attackPreparingConfig)
        {
            _enemy = enemy;
            _attackPreparingConfig = attackPreparingConfig;
            _directionChangeTimer = new CountdownTimer(_attackPreparingConfig.ReturnDuration);

            GetStatsWithRandom();
        }

        private void GetStatsWithRandom()
        {
            _speed = _attackPreparingConfig.Speed.RandomValueInRange;
            _range = _attackPreparingConfig.Range.RandomValueInRange;
            _maxRange = _attackPreparingConfig.MaxRange.RandomValueInRange;
        }

        public void OnEnter()
        {
            Animator.StartAnimation(_attackPreparingConfig.AnimationHash);
            _directionChangeTimer.Start();
            _walkAroundDirection = Random.Range(0, 2) * 2 - 1;
            GetStatsWithRandom();
            Vector3 newDestination = GetPerpendicularPositionFromLeft(Mover.Tr.position, _range, _maxRange);
            Mover.SetDestination(newDestination, _attackPreparingConfig.Speed.RandomValueInRange);

            Animator.SetHeadTarget(Detector.DetectedCharacter);
        }

        public void OnExit()
        {
            Mover.StopAgent();
            _directionChangeTimer.Stop();
            Animator.ClearHeadTarget();
        }

        public void Update(float deltaTime)
        {
            //if(_enemyController.EnemyMover.HasReachedDestination())
            {
                Vector3 newDestination = GetPerpendicularPositionFromLeft(Mover.Tr.position, _range, _maxRange);
                Mover.SetDestination(newDestination, _speed);
            }
            if (_directionChangeTimer.IsFinished)
            {
                _directionChangeTimer.Start();
            }
        }

        public Vector3 GetPerpendicularPositionFromLeft(Vector3 enemyPosition, float minDistance, float maxDistance)
        {
            Transform detectedCharacter = Detector.DetectedCharacter;
            Vector3 toEnemy = enemyPosition - detectedCharacter.position;
            toEnemy.y = 0;
            float enemyDistanceProgress = (toEnemy.magnitude - minDistance) / (maxDistance - minDistance);

            toEnemy.Normalize();
            Vector3 perpendicularDirection;

            if (_walkAroundDirection > 0)
            {
                perpendicularDirection = Vector3.Cross(Vector3.up, toEnemy).normalized;
            }
            else
            {
                perpendicularDirection = Vector3.Cross(toEnemy, Vector3.up).normalized;
            }

            Vector3 targetPosition = Vector3.Lerp(enemyPosition, detectedCharacter.position, enemyDistanceProgress);
            targetPosition += perpendicularDirection * minDistance;

            return targetPosition;
        }

        public bool CanAttack() => Combat.CombatData.CanAttack;

        public bool WaitingToAttack() => !Combat.CombatData.CanAttack && (Mover.Tr.position - Detector.DetectedCharacter.position).magnitude < _range;

        public bool OutOfRange() => (Mover.Tr.position - Detector.DetectedCharacter.position).magnitude > _maxRange;
        public bool TimerFinished() => _directionChangeTimer.IsFinished;
    }
}