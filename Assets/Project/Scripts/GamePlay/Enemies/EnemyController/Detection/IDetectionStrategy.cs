using ImprovedTimers;
using ImprovedTimers.Project.Scripts.Utils.Timers;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.Detection
{
    public interface IDetectionStrategy
    {
        bool Execute(Transform character,Transform detector,CountdownTimer timer);
    }
    public class ConeDetectionStrategy:IDetectionStrategy
    {
        private readonly float _detectionAngle;
        private readonly float _detectionRadius;
        private  float _innerDetectionRadius;

        public ConeDetectionStrategy(float detectionAngle,float detectionRadius,float innerDetectionRadius)
        {
            _detectionAngle = detectionAngle;
            _detectionRadius = detectionRadius;
            _innerDetectionRadius = innerDetectionRadius;
        }
   
        public bool Execute(Transform character,Transform detector,CountdownTimer timer)
        {
            if (timer.IsRunning) return false;
            var directionToCharacter = character.position - detector.position;
            var angleToCharacter = Vector3.Angle(detector.forward,directionToCharacter);
            
            
            if ((!(angleToCharacter < _detectionAngle / 2) || !(directionToCharacter.magnitude < _detectionRadius))
                &&!(directionToCharacter.magnitude<_innerDetectionRadius)) return false;
            
            timer.Start();
            return true;
            
        }
    }
}