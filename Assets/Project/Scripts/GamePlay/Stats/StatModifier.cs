using System;
using Assets.Code.GamePlay.Stats;
using ImprovedTimers.Project.Scripts.Utils.Timers;

namespace Project.Scripts.GamePlay.Stats
{
    public abstract class StatModifier : IDisposable
    {
        public bool MarkedForRemoval { get; set; }

        public event Action<StatModifier> OnDispose = delegate { };
        public CountdownTimer CountdownTimer => _timer;
        public StatType StatType => _statType;
        private readonly CountdownTimer _timer;
        private readonly StatType _statType;

        protected StatModifier(StatType statType, float duration)
        {
            _statType = statType;
            if (duration <= 0) return;

            _timer = new CountdownTimer(duration);
            _timer.OnTimerStop += () => MarkedForRemoval = true;
            _timer.Start();
        }

        public void Update(float deltaTime) => _timer?.Tick(deltaTime);

        public abstract void Handle(object sender, Query query);

        public void Dispose()
        {
            OnDispose.Invoke(this);
        }
    }
}