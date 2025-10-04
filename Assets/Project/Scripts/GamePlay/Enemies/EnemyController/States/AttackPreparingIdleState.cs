using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs;
using Assets.Code.GamePlay.GameplayStateMachine;
using ImprovedTimers;
using ImprovedTimers.Project.Scripts.Utils.Timers;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.States
{
    public class AttackPreparingIdleState : IState
    {
        private readonly ActorEntity _enemy;
        private readonly AttackPreparingIdleConfig _config;
        private readonly CountdownTimer _idleTimer;
        private EnemyMover Mover => _enemy.Get<EnemyMover>();

        public AttackPreparingIdleState(ActorEntity enemy, AttackPreparingIdleConfig config)
        {
            _enemy = enemy;
            _config = config;
            _idleTimer = new CountdownTimer(_config.IdleDuration);
        }

        public void OnEnter()
        {
            _enemy.Get<EnemyAnimator>().StartAnimation(_config.AnimationHash);
            _idleTimer.Start();
        }

        public void Update(float deltaTime)
        {
            Vector3 targetDirection = _enemy.Get<CharacterDetector>().DetectedCharacter.position -
                                      Mover.Tr.position;
            targetDirection.y = 0;
            Mover.Tr.rotation = Quaternion.RotateTowards(Mover.Tr.rotation,
                Quaternion.LookRotation(targetDirection),
                deltaTime * Mover.RotationSpeed);
        }

        public bool TimerFinished() => _idleTimer.IsFinished;
    }
}