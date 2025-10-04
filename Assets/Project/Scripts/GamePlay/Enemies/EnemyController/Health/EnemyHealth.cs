using System;
using Assets.Code.GamePlay.Health;
using Assets.Code.GamePlay.Physic.ColliderLogic;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.Health
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