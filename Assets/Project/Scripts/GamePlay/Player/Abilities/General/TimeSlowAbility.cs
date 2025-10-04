using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Player.Abilities.Configs;
using Assets.Code.GamePlay.Player.Controller;
using Code.Gameplay.Common.Time;
using UnityEngine;
using Zenject;

namespace Assets.Code.GamePlay.Abilities.General
{
    public class TimeSlowAbility:IAbility
    {
        private TimeSlowingAbilityConfig _config;
        private ITimeService _timeService;

        [Inject]
        private void Construct(ITimeService timeService)
        {
            _timeService = timeService;
        }
        public void SetConfig(TimeSlowingAbilityConfig config)
        {
            _config = config;
        }

        public void Init(ActorEntity caster)
        {
        }

        public void OnInput(bool pressed)
        {
            if (pressed)
            {
                Execute();
            }
        }

        public async void Execute()
        {
            float elapsedTime = 0f;
            while (elapsedTime < _config.Duration)
            {
                elapsedTime += _timeService.UnscaledDeltaTime;
                float timeScale = _config.TimeSlowCurve.Evaluate(elapsedTime / _config.Duration);
                _timeService.SetTimeScale(timeScale);
                await Awaitable.NextFrameAsync();
            }
            _timeService.SetTimeScale(1f); 
        }
    }
}