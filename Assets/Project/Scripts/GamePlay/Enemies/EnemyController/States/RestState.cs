using Assets.Code.GamePlay.Common.Entity;
using ImprovedTimers.Project.Scripts.Utils.Timers;
using Project.Scripts.GamePlay.Common.GameplayStateMachine;
using Project.Scripts.GamePlay.Enemies.EnemyController.StateConfigs;

namespace Project.Scripts.GamePlay.Enemies.EnemyController.States
{
    public class RestState:IState
    {
        private readonly ActorEntity _enemy;
        private readonly RestStateConfig _config;
        private readonly CountdownTimer _idleTimer;
        public RestState(ActorEntity enemy, RestStateConfig config)
        {
            _enemy = enemy;
            _config = config;
            _idleTimer = new CountdownTimer(_config.RestDuration);
        }

        public void OnEnter()
        {
            _enemy.Get<EnemyAnimator>().StartAnimation(_config.AnimationHash);
            _idleTimer.Start();
        }
        public bool TimerFinished() => _idleTimer.IsFinished;
    }
}