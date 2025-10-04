using Code.Infrastructure.Loading;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using Code.Progress.Data;
using Code.Progress.Provider;

namespace Code.Infrastructure.States.GameStates
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