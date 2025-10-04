using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs;
using Assets.Code.GamePlay.GameplayStateMachine;
using ImprovedTimers;
using ImprovedTimers.Project.Scripts.Utils.Timers;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.States
{
    public class RoarAttackState:IState
    {
        private readonly ActorEntity _enemy;
        private readonly RoarAttackStateConfig _config;
        private readonly CountdownTimer _attackTimer;
        private readonly CountdownTimer _stateTimer;
        private EnemyMover Mover => _enemy.Get<EnemyMover>();
        private EnemyCombat Combat => _enemy.Get<EnemyCombat>();
        private EnemyAnimator Animator => _enemy.Get<EnemyAnimator>();

        
        public RoarAttackState(ActorEntity enemy, RoarAttackStateConfig config)
        {
            _enemy = enemy;
            _config = config;
            _attackTimer = new CountdownTimer(_config.AttackDuration);
            _stateTimer = new CountdownTimer(_config.StateDuration);
        }
        public void OnEnter()
        {
            _attackTimer.Start();
            _stateTimer.Start();
            Animator.StartAnimation(_config.AnimationHash);
            Animator.OnAnimationEvent += OnAnimationEvent;

            
            _attackTimer.OnTimerStop += StopAttack;
            //_enemyController.Attack();
        }

        private void OnAnimationEvent(string eventName)
        {
            if(_config.AttackId==eventName)
            {
                Combat.Attack(_config.AttackId);
                Combat.ShowPreAttackMark(eventName);

            }
        }

        private void StopAttack()
        {
            Combat.StopAttack(_config.AttackId);
        }
        
        public void OnExit()
        {
            _attackTimer.Stop();
            _attackTimer.OnTimerStop -= StopAttack;

        }
        public void Update(float deltaTime)
        {
            if(_config.Rotate)
            {
                Vector3 targetDirection = _enemy.Get<CharacterDetector>().DetectedCharacter.position - Mover.Tr.position;
                Mover.RotateInDirection(targetDirection,deltaTime);
            }
        }
        public bool AttackTimerFinished() => _stateTimer.IsFinished;

    }
}