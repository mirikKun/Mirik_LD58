using System;

namespace Code.Gameplay.Common.Time
{
    public interface ITimeService
    {
        float DeltaTime { get; }
        float UnscaledDeltaTime { get; }

        float FixedDeltaTime { get; }
        float UnscaledFixedDeltaTime { get; }

        
        float TimeScale { get; }
        event Action<float> OnTimeScaleChanged;
        
        
        void SetTimeScale(float timeScale);
        void StopTime();
        void StartTime();
    }
}