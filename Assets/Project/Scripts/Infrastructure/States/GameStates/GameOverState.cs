using Project.Scripts.GamePlay.Windows;
using Project.Scripts.Infrastructure.States.StateInfrastructure;

namespace Project.Scripts.Infrastructure.States.GameStates
{
    public class GameOverState : IState
    {
        private readonly IWindowService _windowService;

        public GameOverState(IWindowService windowService)
        {
            _windowService = windowService;
        }

        public void Enter()
        {
          
        }

        public void Exit()
        {
        }
    }
}