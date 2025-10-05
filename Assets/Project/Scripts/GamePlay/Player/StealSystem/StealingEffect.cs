using System;
using UnityEngine;

namespace Project.Scripts.GamePlay.Player.StealSystem
{
    public class StealingEffect:MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particles;
        [SerializeField] private Transform _book;
        [SerializeField] private Vector3 _stealingRotation;
        [SerializeField] private float _bookRotationSpeed = 5f;

        private Vector3 _startRotation;
        private Vector3 _targetRotation;

        private void Start()
        {
            _startRotation = _book.localEulerAngles;
            _targetRotation=_startRotation;

        }

        public void Tick(float deltaTime)
        {
            _book.localEulerAngles = Vector3.Lerp(_book.localEulerAngles, _targetRotation, deltaTime * _bookRotationSpeed);
        }
        public void StartStealingEffect()
        {
            _particles.Play();
            _targetRotation=_stealingRotation;
        }
        public void StopStealingEffect()
        {
            _particles.Stop();
            _targetRotation=_startRotation;
        }
    }
}