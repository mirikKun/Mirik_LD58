using Project.Scripts.GamePlay.Armaments.ArmamentBehaviour.Abstract;
using Project.Scripts.GamePlay.Common.Time;
using UnityEngine;

namespace Project.Scripts.GamePlay.Armaments.ArmamentBehaviour
{
    public class MovingArmamentBehaviour :IArmamentBehaviour, IFixedUpdateableArmament,IOnDestroyableBehaviour,IOnDismissableBehaviour
    {
        private float _speed;
        private Vector3 _direction;
        private readonly ITimeService _timeService;

        private float _currentLifeTime;
        private Armament _armament;

        public MovingArmamentBehaviour(float speed, Vector3 direction, ITimeService timeService)
        {
            _direction = direction;
            _timeService = timeService;
            _speed = speed;
            _timeService.OnTimeScaleChanged += OnTimeScaleChanged;
        }

        private void OnTimeScaleChanged(float timeScale)
        {
            _armament.Rigidbody.linearVelocity = _direction * (_speed * timeScale);

        }

        public void InitArmament(Armament armament)
        {
            _armament = armament;
           

        }
        public void FixedTick(float deltaTime)
        {
            Move(deltaTime);
        }

        private void Move(float deltaTime)
        {
            _armament.Rigidbody.linearVelocity = _direction * ((_speed) * (deltaTime/Time.fixedDeltaTime));
            
        }

        public void OnDestroy()
        {
            _timeService.OnTimeScaleChanged -= OnTimeScaleChanged;

        }

        public void OnDismissed()
        {
            _timeService.OnTimeScaleChanged -= OnTimeScaleChanged;
        }
    }
}