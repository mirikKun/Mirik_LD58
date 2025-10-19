using Project.Scripts.Infrastructure.Loading;
using Project.Scripts.Infrastructure.Progress.Data;
using Project.Scripts.Infrastructure.Progress.Provider;
using Project.Scripts.Infrastructure.States.StateInfrastructure;
using Project.Scripts.Infrastructure.States.StateMachine;

namespace Project.Scripts.Infrastructure.States.GameStates
{
    public class InitializeProgressState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IProgressProvider _progressProvider;

        public InitializeProgressState(
            IGameStateMachine stateMachine,
            IProgressProvider progressProvider)
        {
            _stateMachine = stateMachine;
            _progressProvider = progressProvider;
        }

        public void Enter()
        {
            InitializeProgress();

            //_stateMachine.Enter<LoadingMainMenuScreenState>();
            _stateMachine.Enter<LoadingGameplayState,string>(Scenes.GamePlay.ToString());
        }

        private void InitializeProgress()
        {
            if (_progressProvider.HasProgress())
            {
                _progressProvider.LoadProgress();
            }
            else
            {
                CreateNewProgress();

            }
        }

        private void CreateNewProgress()
        {
            _progressProvider.SetProgressData(new ProgressData());
        }

        public void Exit()
        {
        }
    }
}