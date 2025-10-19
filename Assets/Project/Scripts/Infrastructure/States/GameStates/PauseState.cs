using Project.Scripts.GamePlay.Common.GameBehaviour.Services;
using Project.Scripts.GamePlay.Common.Time;
using Project.Scripts.Infrastructure.States.StateInfrastructure;

namespace Project.Scripts.Infrastructure.States.GameStates
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