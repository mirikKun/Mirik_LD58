using System;
using ImprovedTimers.Project.Scripts.Utils.Timers;

namespace Project.Scripts.GamePlay.Statuses
{
    public class Status: IDisposable
    {
        private readonly CountdownTimer _timer;
        private StatusType _statusType;
        public bool MarkedForRemoval { get; set; }

        public event Action<Status> OnDispose;
        public StatusType StatusType=> _statusType;
        public CountdownTimer CountdownTimer=> _timer;
        public void Dispose()
        {
            OnDispose?.Invoke(this);
        }
        public Status(StatusType statusType,float duration=0)
        {
            _statusType = statusType;
            if (duration <= 0) return;
            _timer = new CountdownTimer(duration);
            _timer.OnTimerStop += () => MarkedForRemoval = true;
            _timer.Start();
        }
        public void Update(float deltaTime) => _timer?.Tick(deltaTime);

    }
}