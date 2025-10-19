using Project.Scripts.GamePlay.Player.Health;
using UnityEngine;

namespace Project.Scripts.GamePlay.HUD
{
    public class PlayerHealthUI : HealthUI
    {
        [SerializeField] private PlayerHealth _playerHealth;

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