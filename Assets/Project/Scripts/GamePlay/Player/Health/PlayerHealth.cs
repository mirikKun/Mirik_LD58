using Assets.Code.GamePlay.Health;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.Health
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