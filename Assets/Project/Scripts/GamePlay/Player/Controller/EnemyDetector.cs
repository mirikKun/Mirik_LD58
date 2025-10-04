using System.Collections.Generic;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Enemies.EnemyController;
using Assets.Code.GamePlay.Player.PlayerEffects;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.Controller
{
    public class EnemyDetector:EntityComponent
    {
        [SerializeField] private EnemyIndicationEffectUI _enemyIndicationEffectUI;

        [SerializeField] private Transform _indicationTransform;
        private List<EnemyEntity> _detectedEnemies=new List<EnemyEntity>();
        public List<EnemyEntity> DetectedEnemies => _detectedEnemies;

        public void Tick(float deltaTime)
        {
            List<EnemyIndication.Indicator> enemyIndications = EnemyIndication.GetIndicators(DetectedEnemies,_indicationTransform);
            _enemyIndicationEffectUI.UpdateEnemyIndicators(enemyIndications);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyEntity enemyController))
            {
                _detectedEnemies.Add(enemyController);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.TryGetComponent(out EnemyEntity enemyController))
            {
                _detectedEnemies.Remove(enemyController);
            }
        }
    }
}