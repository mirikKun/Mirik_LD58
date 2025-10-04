using Assets.Code.GamePlay.Common.GameBehaviour.Services;
using Code.Gameplay.Common.Time;
using Code.Infrastructure.States.StateInfrastructure;

namespace Code.Infrastructure.States.GameStates
{
    public class PauseState : IState
    {
        private readonly IUpdateService _updateService;
        private readonly ITimeService _timeService;

        public PauseState(IUpdateService updateService,ITimeService timeService)
        {
            _timeService = timeService;
            _updateService = updateService;
        }
       
        public void Exit()
        {
            _updateService.ResumeAll();
            _timeService.StartTime();

        }

        public void Enter()
        {
            _updateService.PauseAll();
            _timeService.StopTime();

        }
    }
}