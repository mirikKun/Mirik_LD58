using System.Linq;
using Assets.Code.GamePlay.Common.Entity;
using ImprovedTimers.Project.Scripts.Utils.Timers;
using Project.Scripts.GamePlay.Common.GameplayStateMachine;
using Project.Scripts.GamePlay.Enemies.EnemyController.Enum;
using Project.Scripts.GamePlay.Enemies.EnemyController.StateConfigs;
using Project.Scripts.Utils;
using UnityEngine;

namespace Project.Scripts.GamePlay.Enemies.EnemyController.States
{
    public class GravityFallStunState : IState
    {
        private readonly ActorEntity _enemy;
        private readonly GravityFallStunStateConfig _config;
        private readonly CountdownTimer _stunTimer;
        private bool _forced;
        private EnemyMovementType _lastType;

        private RigidbodyEnemyMover Mover => _enemy.Get<RigidbodyEnemyMover>();

        public GravityFallStunState(ActorEntity enemy, GravityFallStunStateConfig config)
        {
            _enemy = enemy;
            _config = config;
            _stunTimer = new CountdownTimer(config.StunDuration);
            _enemy.Get<RigidbodyEnemyMover>().Forced += OnForced;
        }

        public void Dispose()
        {
            _enemy.Get<RigidbodyEnemyMover>().Forced -= OnForced;
        }


        public void OnEnter()
        {
            _stunTimer.Start();
            _forced = false;
            if (Mover.EnemyMovementType == EnemyMovementType.PhysicsWithGravity)
                return;

            _lastType = Mover.EnemyMovementType;
            Mover.SetMovementType(EnemyMovementType.PhysicsWithGravity);
        }


        public void OnExit()
        {
            _stunTimer.Stop();
            Mover.SetMovementType(_lastType);
        }

      
        public bool IsForced()
        {
            return _forced;
        }

        public bool StunTimerFinished()
        {
            return _stunTimer.IsFinished;
        }

        private void OnForced()
        {
            _forced = true;
        }
        
    
    }
}