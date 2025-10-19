using Project.Scripts.GamePlay.Common.Health;
using UnityEngine;

namespace Project.Scripts.GamePlay.Player.Health
{
    public class PlayerHealth:BaseHealth
    {
        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            Debug.Log($"Player took {damage} damage, current health: {Current}");
        }

        public void ResetHealth()
        {
            Current=Max;
        }
    }
}