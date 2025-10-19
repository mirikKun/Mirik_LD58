using Project.Scripts.GamePlay.Common.Health;
using UnityEngine;

namespace Project.Scripts.GamePlay.Enemies.EnemyController.Health
{
    public class EnemyHealth:BaseHealth
    {
        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            Debug.Log($"Enemy took {damage} damage, current health: {Current}");
        }
    }
}