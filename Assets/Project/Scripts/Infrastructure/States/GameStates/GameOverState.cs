using Code.Gameplay.Windows;
using Code.Infrastructure.States.StateInfrastructure;

namespace Code.Infrastructure.States.GameStates
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