using Assets.Code.GamePlay.Common.GameBehaviour.Services;
using Assets.Code.Infrastructure.States.StateInfrastructure;
using Code.Gameplay.Common.Time;

using Code.Infrastructure.States.StateInfrastructure;

namespace Code.Infrastructure.States.GameStates
{
    public class GameLoopState : IState, IUpdateable,IFixedUpdateable,ILateUpdateable
    {

        private readonly ITimeService _timeService;
        private readonly IUpdateService _updateService;

        public GameLoopState(ITimeService timeService,IUpdateService updateService)
        {
            _timeService = timeService;
            _updateService = updateService;
        }

        public void Enter()
        {
            
        }

        public void Update()
        {
            _updateService.UpdateAll(_timeService.DeltaTime);
            
        }

        public void FixedUpdate()
        {
            _updateService.FixedUpdateAll(_timeService.FixedDeltaTime);

        }

        public void LateUpdate()
        {
            _updateService.LateUpdateAll(_timeService.DeltaTime);
        }

        public void Exit()
        {
        }
    }
}