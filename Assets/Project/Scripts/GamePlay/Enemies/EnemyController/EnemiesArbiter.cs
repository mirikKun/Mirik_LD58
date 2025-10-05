using System.Collections.Generic;
using System.Linq;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Common.GameBehaviour.Services;
using Assets.Code.GamePlay.Enemies.EnemyController.Enum;
using Assets.Code.GamePlay.Enemies.EnemyController.Mediator;
using Assets.Code.GamePlay.Health;
using ImprovedTimers;
using ImprovedTimers.Project.Scripts.Utils.Timers;
using UnityEngine;
using Zenject;

namespace Assets.Code.GamePlay.Enemies.EnemyController
{
    public class EnemiesArbiter : MonoBehaviour,IGameUpdateable,IGameFixedUpdateable

    {
        [SerializeField] private ActorEntity _characterController;
        [SerializeField] private EnemyMediator _enemyMediator;

        [SerializeField] private List<EnemyEntity> _enemies;

        private IUpdateService _updateService;


        [Inject]
        private void Construct(IUpdateService updateService)
        {
            _updateService = updateService;
        }

        private void Start()
        {
            _enemies = FindObjectsByType<EnemyEntity>(FindObjectsSortMode.None).ToList();
            foreach (var enemy in _enemies)
            {
                enemy.SetupEnemy(_enemyMediator);
                enemy.Get<CharacterDetector>().Setup(_characterController);
                enemy.Get<IHealth>().Died += OnEnemyDied;
            }
            _updateService.EnemiesUpdate.Register(this);
            _updateService.EnemiesFixedUpdate.Register(this);
        }

        private void OnEnemyDied(BaseEntity enemy)
        {
            _enemies.Remove(enemy as EnemyEntity);
            Destroy(enemy.gameObject);

        }


        private void OnDestroy()
        {
            _updateService.EnemiesUpdate.Unregister(this);
            _updateService.EnemiesFixedUpdate.Unregister(this);
        }

        public void GameUpdate(float deltaTime)
        {
            // if (_combatStateCheckTimer.IsFinished)
            //     ConfigureAttackingEnemy();
            foreach (var enemy in _enemies)
            {
                enemy.Get<EnemyStatesContainer>().TickUpdate(deltaTime);
            }
        }
        public void GameFixedUpdate(float fixedDeltaTime)
        {
            foreach (var enemy in _enemies)
            {
                if(enemy.TryGet(out EnemyRigidbodyMover enemyRigidbodyMover)) enemyRigidbodyMover.FixedTick(fixedDeltaTime);
                enemy.Get<EnemyStatesContainer>().FixedTickUpdate(fixedDeltaTime);
            }
        }
        

        private bool HasEnemyWithPriority(List<EnemyEntity> battleEnemies, out List<EnemyEntity> enemies)
        {
            enemies = battleEnemies.Where(enemy => enemy.Get<EnemyCombat>().CombatLogicType == CombatLogicType.PriorityBased).ToList();
            return enemies.Count > 0;
        }


        private EnemyEntity FindClosestEnemy(List<EnemyEntity> enemies)
        {
            float minDistance = float.MaxValue;
            EnemyEntity closestEnemy = null;
            foreach (var enemy in enemies)
            {
                float distance = Vector3.Distance(_characterController.transform.position, enemy.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestEnemy = enemy;
                }
            }

            return closestEnemy;
        }
        
    }
}