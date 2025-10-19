using Project.Scripts.GamePlay.Enemies.EnemyController.Health;
using Project.Scripts.GamePlay.HUD;
using UnityEngine;

namespace Project.Scripts.GamePlay.Enemies.EnemyController.UI
{
    public class EnemyHealthUI:HealthUI
    {
        [SerializeField] private EnemyHealth _playerHealth;

        protected override void Start()
        {
            base.Start();
            _playerHealth.HealthChanged += SetHealth;
        }

        private void OnDestroy()
        {
            _playerHealth.HealthChanged -= SetHealth;
        }
    }
}