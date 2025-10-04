using System.Collections.Generic;
using System.Linq;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Common.GameBehaviour.Services;
using Assets.Code.GamePlay.Enemies.EnemyController.Enum;
using Assets.Code.GamePlay.Enemies.EnemyController.Mediator;
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

        [SerializeField] private EnemyEntity[] _enemies;

        [SerializeField] private float _combatStateCheckCooldown = 0.5f;
        private CountdownTimer _combatStateCheckTimer;
        private IUpdateService _updateService;


        [Inject]
        private void Construct(IUpdateService updateService)
        {
            _updateService = updateService;
        }

        private void Awake()
        {
            _enemies = FindObjectsByType<EnemyEntity>(FindObjectsSortMode.None);
            _combatStateCheckTimer = new CountdownTimer(_combatStateCheckCooldown);
            _combatStateCheckTimer.Start();
            foreach (var enemy in _enemies)
            {
                enemy.SetupEnemy(_enemyMediator);
                enemy.Get<CharacterDetector>().Setup(_characterController);

            }
        
        }
  
        private void Start()
        {
            _updateService.EnemiesUpdate.Register(this);
            _updateService.EnemiesFixedUpdate.Register(this);
        }

        private void OnDestroy()
        {
            _updateService.EnemiesUpdate.Unregister(this);
            _updateService.EnemiesFixedUpdate.Unregister(this);
        }

        public void GameUpdate(float deltaTime)
        {
            if (_combatStateCheckTimer.IsFinished)
                ConfigureAttackingEnemy();
            foreach (var enemy in _enemies)
            {
                enemy.Get<EnemyStatesContainer>().TickUpdate(deltaTime);
            }
        }
        public void GameFixedUpdate(float fixedDeltaTime)
        {
            foreach (var enemy in _enemies)
            {
                enemy.Get<EnemyStatesContainer>().FixedTickUpdate(fixedDeltaTime);
            }
        }
        

     

        private void ConfigureAttackingEnemy()
        {
            List<EnemyEntity> battleEnemies=new List<EnemyEntity>();

            foreach (var enemy in _enemies)
            {
                if (enemy.Get<EnemyCombat>().CombatData.HasDetectedCharacter)
                {
                    battleEnemies.Add(enemy);
                }
                else
                {
                    enemy.Get<EnemyCombat>().CombatData.CanAttack = false;
                }
            }

            for (var i = battleEnemies.ToList().Count - 1; i >= 0; i--)
            {
                var enemy = battleEnemies.ToList()[i];
                if (enemy.Get<EnemyCombat>().CombatLogicType == CombatLogicType.AlwaysAttack)
                {
                    enemy.Get<EnemyCombat>().CombatData.CanAttack = true;
                    battleEnemies.Remove(enemy);
                }
            }

            if(HasEnemyWithPriority(battleEnemies,out List<EnemyEntity> priorityEnemies))
            {
                EnemyEntity closestEnemy = FindClosestEnemy(priorityEnemies);

                foreach (var battleEnemy in battleEnemies)
                {
                    battleEnemy.Get<EnemyCombat>().CombatData.CanAttack = battleEnemy == closestEnemy;
                }
             
            }
            else
            {
                EnemyEntity closestEnemy = FindClosestEnemy(battleEnemies);
                foreach (var enemy in battleEnemies)
                {
                    enemy.Get<EnemyCombat>().CombatData.CanAttack = enemy == closestEnemy;
                }
            }
            

            _combatStateCheckTimer.Start();
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