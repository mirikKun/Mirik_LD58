using System;
using ImprovedTimers;
using ImprovedTimers.Project.Scripts.Utils.Timers;

namespace Code.Gameplay.Common.Time
{
    public class UnityTimeService : ITimeService
    {
        private bool _paused;
        private float _currentTimeScale=1;
        
        public float DeltaTime => UnityEngine.Time.deltaTime* _currentTimeScale;
        public float UnscaledDeltaTime=> UnityEngine.Time.deltaTime;
        
        public float FixedDeltaTime=>  UnityEngine.Time.fixedDeltaTime * _currentTimeScale;
        public float UnscaledFixedDeltaTime => UnityEngine.Time.fixedDeltaTime;
        
        public float TimeScale => _currentTimeScale;
        public event Action<float> OnTimeScaleChanged;


        public void SetTimeScale(float timeScale)
        {
            _currentTimeScale = timeScale;
            TimerManager.SetTimeMultiplier(timeScale);
            OnTimeScaleChanged?.Invoke(timeScale);
        }

        public void StopTime()
        {
            SetTimeScale(0);
            _paused = true;
        }

        public void StartTime()
        {
            SetTimeScale(1);
            _paused = false;
        }
    }
}