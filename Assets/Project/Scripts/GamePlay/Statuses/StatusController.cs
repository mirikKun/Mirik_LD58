using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Code.GamePlay.Common.Entity;

namespace Project.Scripts.GamePlay.Statuses
{
    public class StatusController : EntityComponent
    {
        private Dictionary<StatusType, Status> _statuses = new Dictionary<StatusType, Status>();
        public event Action<Status> StatusAdded;
        public event Action<Status> StatusRemoved;


        public void AddStatus(Status status)
        {
            _statuses.Add(status.StatusType, status);
            status.MarkedForRemoval = false;
            StatusAdded?.Invoke(status);
            status.OnDispose += _ =>
            {
                _statuses.Remove(status.StatusType);
            };
        }

        public bool TryGetStatus(StatusType statusType, out Status status)
        {
            return _statuses.TryGetValue(statusType, out status);
        }

        public void RemoveStatus(StatusType statusType)
        {
            if (_statuses.TryGetValue(statusType, out Status status))
            {
                status.CountdownTimer.Stop();

                status.MarkedForRemoval = true;
            }
        }

        public void Tick(float deltaTime)
        {
            // foreach (var status in _statuses.Values)
            // {
            //     status.Update(deltaTime);
            // }

            foreach (var status in _statuses.Values.ToList())
            {
                if (status.MarkedForRemoval)
                {
                    StatusRemoved?.Invoke(status);
                    status.Dispose();
                }
            }
        }
    }
}