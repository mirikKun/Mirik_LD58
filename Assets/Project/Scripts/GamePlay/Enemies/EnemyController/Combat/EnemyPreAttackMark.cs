using System;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.Combat
{
    [Serializable]
    public class EnemyPreAttackMark
    {
        [SerializeField] private string _markId;
        [SerializeField] private ParticleSystem _particleSystem;
        
        public string MarkId => _markId;
        public void Execute()
        {
            _particleSystem.Play();
        }
    }
}