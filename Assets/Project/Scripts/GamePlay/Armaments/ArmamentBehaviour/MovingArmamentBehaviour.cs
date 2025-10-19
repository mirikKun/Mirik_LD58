using Project.Scripts.GamePlay.Armaments.ArmamentBehaviour.Abstract;
using UnityEngine;

namespace Project.Scripts.GamePlay.Armaments.ArmamentBehaviour
{
    public class MovingArmamentBehaviour :IArmamentBehaviour, IFixedUpdateableArmament
    {
        private float _speed;
        private Vector3 _direction;

        private float _currentLifeTime;
        private Armament _armament;

        public MovingArmamentBehaviour(  float speed, Vector3 direction)
        {
            _direction = direction;
            _speed = speed;
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
            _armament.Rigidbody.linearVelocity = (_direction * (_speed));
            
        }
    }
}