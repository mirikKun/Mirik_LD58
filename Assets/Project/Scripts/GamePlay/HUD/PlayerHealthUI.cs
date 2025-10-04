using System;
using Assets.Code.GamePlay.Player.Health;
using UnityEngine;

namespace Assets.Code.GamePlay.HUD
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