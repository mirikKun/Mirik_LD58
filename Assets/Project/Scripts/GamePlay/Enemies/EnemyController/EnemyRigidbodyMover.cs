using Assets.Code.GamePlay.Common.Entity;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController
{
    public class EnemyRigidbodyMover : EntityComponent
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private bool _canBeMoved = true;
        
        public void SetMomentum(Vector3 momentum)
        {
            if (!_canBeMoved) return;
            _rigidbody.AddForce(momentum, ForceMode.VelocityChange);
        }
        {
            if (!_canBeMoved) return;
            _rigidbody.velocity = direction * speed;
        }

        
    }
}