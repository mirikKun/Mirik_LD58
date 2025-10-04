using Assets.Code.GamePlay.Enemies.EnemyController.Health;
using Assets.Code.GamePlay.HUD;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.UI
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