using Project.Scripts.Infrastructure.Loading;
using Project.Scripts.Infrastructure.States.StateInfrastructure;
using Project.Scripts.Infrastructure.States.StateMachine;

namespace Project.Scripts.Infrastructure.States.GameStates
{
    public class LoadingMainMenuScreenState : IState
    {
        private const string MainMenuSceneName = "MainMenu";
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;

        public LoadingMainMenuScreenState(IGameStateMachine stateMachine, ISceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.LoadScene(MainMenuSceneName, EnterHomeScreenState);
        }

        private void EnterHomeScreenState()
        {
            _stateMachine.Enter<MainMenuScreenState>();
        }

        public void Exit()
        {
        }
    }
}