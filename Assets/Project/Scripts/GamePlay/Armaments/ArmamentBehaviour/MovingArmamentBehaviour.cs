using Assets.Code.GamePlay.Armaments.ArmamentBehaviour.Abstract;
using Code.Gameplay.Common.Time;
using UnityEngine;

namespace Assets.Code.GamePlay.Armaments.ArmamentBehaviour
{
    public class MovingArmamentBehaviour :IArmamentBehaviour, IUpdateableArmament
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
        public void Tick(float deltaTime)
        {
            Move(deltaTime);
        }

        private void Move(float deltaTime)
        {
            _armament.transform.position += _direction * (_speed * deltaTime);
        }
    }
}