using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs;
using Assets.Code.GamePlay.GameplayStateMachine;
using ImprovedTimers;
using ImprovedTimers.Project.Scripts.Utils.Timers;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.States
{
    public class SimpleAttackState : IState
    {
        private readonly ActorEntity _enemy;
        private readonly SimpleAttackStateConfig _simpleAttackStateConfig;
        private readonly CountdownTimer _attackTimer;
        private readonly CountdownTimer _countdownTimer;
        private readonly CountdownTimer _rotationTimer;
        private EnemyMover Mover => _enemy.Get<EnemyMover>();
        private EnemyCombat Combat => _enemy.Get<EnemyCombat>();
        private EnemyAnimator Animator => _enemy.Get<EnemyAnimator>();
        private CharacterDetector Detector => _enemy.Get<CharacterDetector>();

        protected int Variation = 0;

        public SimpleAttackState(ActorEntity enemy, SimpleAttackStateConfig simpleAttackStateConfig)
        {
            _enemy = enemy;
            _simpleAttackStateConfig = simpleAttackStateConfig;
            _attackTimer = new CountdownTimer(simpleAttackStateConfig.AttackDuration);
            _countdownTimer = new CountdownTimer(simpleAttackStateConfig.AttackCooldown);
            _rotationTimer = new CountdownTimer(simpleAttackStateConfig.RotationDuration);
        }


        public void OnEnter()
        {
            Animator.OnAnimationEvent += OnAnimationEvent;
            _attackTimer.Start();
            Animator.StartAnimation(_simpleAttackStateConfig.AnimationHash);
            _rotationTimer.Start();
            //enemyController.Attack();
        }

        public void OnExit()
        {
            Animator.OnAnimationEvent -= OnAnimationEvent;

            _attackTimer.Stop();
            _countdownTimer.Start();
        }

        public void Update(float deltaTime)
        {
            if (Animator.CurrentAnimationHash != _simpleAttackStateConfig.AnimationHash || !_rotationTimer.IsFinished)
            {
                Vector3 targetDirection = Detector.Pos - Mover.Tr.position;
                Mover.RotateInDirection(targetDirection, deltaTime);
            }
        }

        private void OnAnimationEvent(string eventName)
        {
            if (_simpleAttackStateConfig.AttackId == eventName)
            {
                Vector3 target = Detector.Pos;
                Vector3 direction = Mover.Tr.forward;
                Combat.Attack(eventName, target, direction);
            }

            if (_simpleAttackStateConfig.ShowPreAttackMark)
            {
                Combat.ShowPreAttackMark(eventName);
            }
        }


        public static SimpleAttackState GetVariation(ActorEntity enemy,
            SimpleAttackStateConfig simpleAttackStateConfig, int variation)
        {
            switch (variation)
            {
                case 0:
                    return new SimpleAttackStateVariation1(enemy, simpleAttackStateConfig);
                case 1:
                    return new SimpleAttackStateVariation2(enemy, simpleAttackStateConfig);
                case 2:
                    return new SimpleAttackStateVariation3(enemy, simpleAttackStateConfig);
                default:
                    return new SimpleAttackState(enemy, simpleAttackStateConfig);
            }
        }

        public bool CanAttackCharacter() => Detector.CanAttackCharacter(_simpleAttackStateConfig.AttackRange,
            _simpleAttackStateConfig.AttackAngle);

        public bool AttackTimerFinished() => _attackTimer.IsFinished;
        public bool CooldownPassed() => _countdownTimer.IsFinished;
        public bool CanAttackAndCooldownPassed() => CanAttackCharacter() && CooldownPassed();
    }

    public class SimpleAttackStateVariation1 : SimpleAttackState
    {
        public SimpleAttackStateVariation1(ActorEntity enemy, SimpleAttackStateConfig simpleAttackStateConfig) : base(
            enemy, simpleAttackStateConfig)
        {
            Variation = 1;
        }
    }

    public class SimpleAttackStateVariation2 : SimpleAttackState
    {
        public SimpleAttackStateVariation2(ActorEntity enemy, SimpleAttackStateConfig simpleAttackStateConfig) : base(
            enemy, simpleAttackStateConfig)
        {
            Variation = 2;
        }
    }

    public class SimpleAttackStateVariation3 : SimpleAttackState
    {
        public SimpleAttackStateVariation3(ActorEntity enemy, SimpleAttackStateConfig simpleAttackStateConfig) : base(
            enemy, simpleAttackStateConfig)
        {
            Variation = 3;
        }
    }
}