using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Enemies.EnemyController.Detection;
using Assets.Code.GamePlay.Player.Controller;
using ImprovedTimers;
using ImprovedTimers.Project.Scripts.Utils.Timers;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController
{
    public class CharacterDetector : EntityComponent
    {
        [SerializeField] private float _innerDetectionSphereRadius = 5;

        [SerializeField] private float _detectionConeAngle = 60;
        [SerializeField] private float _detectionConeLength = 15;


        [SerializeField] private Transform _detectionOrigin;

        [SerializeField] private float _predictionTimeOffset = 1f;
        [SerializeField] private float _detectionCooldown = 1;
        [SerializeField] private bool _debugMode;

        [Header("Ally Detection")] [SerializeField]
        private float _allyDetectionRadius = 15;

        public Transform DetectedCharacter => _detectedCharacter.Get<PlayerController>().TargetTr;
        public Vector3 Pos=>DetectedCharacter.position;
        public bool DetectedByOther => _detectedByOther;
        private CountdownTimer _detectionTimer;
        private CountdownTimer _allyDetectionTimer;
        private IDetectionStrategy _detectionStrategy;
        private bool _detectedByOther;
        private ActorEntity _detectedCharacter;

        public void Setup(ActorEntity character)
        {
            _detectionTimer = new CountdownTimer(_detectionCooldown);
            _detectedCharacter = character;
            _detectionStrategy =
                new ConeDetectionStrategy(_detectionConeAngle, _detectionConeLength, _innerDetectionSphereRadius);
        }

        public void SetCharacterDetectByOther(bool detected)
        {
            _detectedByOther = detected;
        }

        public Vector3 GetCharacterPredictedPosition() =>
            DetectedCharacter.position + _detectedCharacter.Get<PlayerMover>().GetVelocity() * _predictionTimeOffset;
        public Vector3 GetCharacterPredictedOffset(float predictTime) =>
            _detectedCharacter.Get<PlayerMover>().GetVelocity() * predictTime;

        public bool CanAttackCharacter(float attackRange, float attackAngle)
        {
            var directionToCharacter = DetectedCharacter.position - _detectionOrigin.position;
            return directionToCharacter.magnitude <= attackRange
                // && Vector3.Angle(_detectionOrigin.forward, directionToCharacter) <= attackAngle
                ;
        }
        public float GetDistanceToCharacter() => Vector3.Distance(DetectedCharacter.position, _detectionOrigin.position);

        public bool IsNearAlly(EnemyEntity enemy) =>
            Vector3.Distance(enemy.transform.position, _detectionOrigin.position) <= _allyDetectionRadius;

        public bool CanDetectCharacter => _detectionTimer.IsRunning || _detectedByOther || DetectedByStrategy();

        public void SetDetectionStrategy(IDetectionStrategy detectionStrategy) =>
            _detectionStrategy = detectionStrategy;

        private bool DetectedByStrategy()
        {
            bool detectedByStrategy = _detectionStrategy.Execute(DetectedCharacter, _detectionOrigin, _detectionTimer);
            return detectedByStrategy;
        }

        private void OnDrawGizmos()
        {
            if (!_debugMode)
                return;
            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(_detectionOrigin.position, _innerDetectionSphereRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_detectionOrigin.position, _detectionConeLength);

            Vector3 leftConeDirection = Quaternion.Euler(0, -_detectionConeAngle / 2, 0) * _detectionOrigin.forward;
            Vector3 rightConeDirection = Quaternion.Euler(0, _detectionConeAngle / 2, 0) * _detectionOrigin.forward;

            Gizmos.DrawLine(_detectionOrigin.position,
                _detectionOrigin.position + leftConeDirection * _detectionConeLength);
            Gizmos.DrawLine(_detectionOrigin.position,
                _detectionOrigin.position + rightConeDirection * _detectionConeLength);
        }
    }
}