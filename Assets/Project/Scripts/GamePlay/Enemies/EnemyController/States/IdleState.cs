using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs;
using Assets.Code.GamePlay.GameplayStateMachine;
using ImprovedTimers;
using ImprovedTimers.Project.Scripts.Utils.Timers;

namespace Assets.Code.GamePlay.Enemies.EnemyController.States
{
    public class IdleState:IState
    {
        private readonly ActorEntity _enemy;
        private readonly IdleStateConfig _idleConfig;
        private readonly CountdownTimer _idleTimer;
        public IdleState(ActorEntity enemy, IdleStateConfig idleConfig)
        {
            _enemy = enemy;
            _idleConfig = idleConfig;
            _idleTimer = new CountdownTimer(_idleConfig.IdleDuration);
        }

        public void OnEnter()
        {
            if(_enemy.TryGet<EnemyAnimator>(out var animator))
                animator.StartAnimation(_idleConfig.AnimationHash);
            _idleTimer.Start();
        }
        public void OnExit()
        {
            if (_enemy.Get<CharacterDetector>().CanDetectCharacter)
            {
               // _enemy.Get<EnemyCombat>().CombatData.HasDetectedCharacter = true;

            }
        }
        public bool TimerFinished() => _idleTimer.IsFinished;
    }
}