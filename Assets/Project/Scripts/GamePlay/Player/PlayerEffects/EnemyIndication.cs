using System.Collections.Generic;
using Assets.Code.GamePlay.Enemies.EnemyController;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.PlayerEffects
{
    public class EnemyIndication
    {
        public static List<Indicator> GetIndicators(List<EnemyEntity> enemies, Transform characterTransform)
        {
            List<Indicator> indicators = new List<Indicator>();
            foreach (var enemyController in enemies)
            {
                var enemyPosition = enemyController.GetPosition();
                var distance = Vector3.Distance(characterTransform.position, enemyPosition);
                var direction = enemyPosition - characterTransform.position;
                var characterForward = characterTransform.forward;
                characterForward.y = 0;
                direction.y = 0;
                var angle = Vector3.SignedAngle(characterForward, direction,Vector3.up);
                indicators.Add(new Indicator
                {
                    IsAttacking = enemyController.Get<EnemyCombat>().CombatData.CanAttack,
                    Distance = distance,
                    Angle = angle
                });
            }

            return indicators;
        }

        public class Indicator
        {
            public bool IsAttacking;
            public float Distance;
            public float Angle;
        }
    }
}