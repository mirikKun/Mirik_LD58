using Assets.Code.GamePlay.Common.Entity;
using UnityEngine;

namespace Project.Scripts.GamePlay.Common.Movement
{
    public interface IMovementForceApplier
    {
        void ApplyForce(Vector3 force);
        bool IsFlying { get; }
    }
}