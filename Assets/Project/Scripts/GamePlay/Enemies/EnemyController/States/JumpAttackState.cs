using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs;
using Assets.Code.GamePlay.GameplayStateMachine;
using ImprovedTimers;
using ImprovedTimers.Project.Scripts.Utils.Timers;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.States
{
    public class JumpAttackState : IState
    {
        private readonly ActorEntity _enemy;
        private readonly JumpAttackStateConfig _config;
        private readonly CountdownTimer _preparingTimer;
        private readonly CountdownTimer _jumpTimer;
        private readonly CountdownTimer _attackTimer;

        private Vector3 _startPoint;
        private Vector3 _endPoint;

        private bool _jumpStarted;
        private bool _jumpEnded;
        private EnemyMover Mover => _enemy.Get<EnemyMover>();
        private EnemyCombat Combat => _enemy.Get<EnemyCombat>();
        private EnemyAnimator Animator => _enemy.Get<EnemyAnimator>();
        private CharacterDetector Detector => _enemy.Get<CharacterDetector>();


        public JumpAttackState(ActorEntity enemy, JumpAttackStateConfig config)
        {
            _enemy = enemy;
            _config = config;
            _preparingTimer = new CountdownTimer(_config.JumpDelay);
            _jumpTimer = new CountdownTimer(_config.JumpDuration);
            _attackTimer = new CountdownTimer(_config.AttackDuration);
        }

        public void OnEnter()
        {
            Animator.OnAnimationEvent += OnAnimationEvent;

            Mover.DisableAgent();

            _jumpStarted = false;
            _jumpEnded = false;

            _startPoint = Mover.Tr.position;
            Vector3 targetPosition = Detector.Pos;
            Vector3 direction = (targetPosition - _startPoint).normalized;
            _endPoint = Mover.GetClosestPointOnNavMesh(
                targetPosition - direction * _config.TargetDistanceToPlayer);

            if (float.IsNaN(_endPoint.x))
            {
                Debug.LogError("End point is NaN "+_endPoint+" near "+(targetPosition - direction * _config.TargetDistanceToPlayer));
                _jumpEnded = true;
                _endPoint = _startPoint;
                return;
            }
            Animator.StartAnimation(_config.JumpStartAnimationHash);
            _preparingTimer.Start();

        }

        public void OnExit()
        {
            Animator.OnAnimationEvent -= OnAnimationEvent;

        }

        public void Update(float deltaTime)
        {
            if (!_preparingTimer.IsFinished && !_jumpStarted)
            {
                Vector3 targetDirection = Detector.DetectedCharacter.position - Mover.Tr.position;
                Mover.RotateInDirection(targetDirection,deltaTime);
            }

            if (_preparingTimer.IsFinished&&!_jumpStarted )
            {
                Vector3 targetDirection = Detector.DetectedCharacter.position - Mover.Tr.position;

                Mover.SetRotationByDirection(targetDirection);
                _jumpTimer.Start();
                _jumpStarted = true;

            }

            if (_jumpStarted && !_jumpEnded)
            {
                ProceedJump();
            }

            if (_jumpTimer.IsFinished && _jumpStarted&&!_jumpEnded)
            {
                Animator.StartAnimation(_config.JumpEndAnimationHash);
                _attackTimer.Start();
                _jumpEnded = true;
                Mover.EnableAgent();

               // _enemyController.Combat.Attack(_config.AttackId);
            }
        }
        private void OnAnimationEvent(string eventName)
        {
            if(_config.AttackId==eventName)
            {
                Vector3 target=Detector.Pos;
                Vector3 direction = Mover.Tr.forward;
                Combat.Attack(eventName,target,direction);
                Combat.ShowPreAttackMark(eventName);
            }
        }


        private void ProceedJump()
        {
            Vector3 newPosition = Vector3.Lerp(_startPoint, _endPoint,
                _config.JumpSpeedCurve.Evaluate(1-_jumpTimer.Progress));
            newPosition.y += _config.JumpHeightCurve.Evaluate(1-_jumpTimer.Progress) * _config.JumpHeight;
            Mover.SetPosition(newPosition);
        }


        public void FixedUpdate(float fixedDeltaTime)
        {
        }

        public bool CanAttack() => Combat.CombatData.CanAttack &&
                                   Detector.CanAttackCharacter(_config.JumpAttackRange, 0) &&
                                   Detector.GetDistanceToCharacter() >
                                   _config.JumpAttackMinRange;

        public bool Finished() => _jumpTimer.IsFinished && _attackTimer.IsFinished && _preparingTimer.IsFinished&&_jumpEnded;
    }
}