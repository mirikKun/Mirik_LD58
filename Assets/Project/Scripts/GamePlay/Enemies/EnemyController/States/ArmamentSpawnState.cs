using Assets.Code.GamePlay.Abilities.General;
using Assets.Code.GamePlay.Armaments;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Enemies.EnemyController.StateConfigs;
using Assets.Code.GamePlay.GameplayStateMachine;
using ImprovedTimers;
using ImprovedTimers.Project.Scripts.Utils.Timers;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.States
{
    public class ArmamentSpawnState: IState
    {
        
        private readonly ArmamentSpawnStateConfig _config;
        private readonly ActorEntity _enemy;
        private readonly CountdownTimer _attackTimer;
        private readonly CountdownTimer _reloadTimer;
        private readonly CountdownTimer _rotationTimer;
        
        private EnemyRigidbodyMover Mover => _enemy.Get<EnemyRigidbodyMover>();
        private CharacterDetector Detector => _enemy.Get<CharacterDetector>();

        public ArmamentSpawnState(ActorEntity enemy, ArmamentSpawnStateConfig armamentSpawnState)
        {
            _enemy = enemy;
            _config = armamentSpawnState;
            _attackTimer = new CountdownTimer(armamentSpawnState.AttackDuration);
            _reloadTimer = new CountdownTimer(armamentSpawnState.ReloadDuration);
            _rotationTimer = new CountdownTimer(armamentSpawnState.RotationDuration);
        }
        
        public void OnEnter()
        {
            _attackTimer.Start();
            _rotationTimer.Start();
            //enemyController.Attack();
        }
        public void OnExit()
        {

            _attackTimer.Stop();
            _reloadTimer.Start();
            IAbility ability = _enemy.Get<EnemyAbilityCaster>().CreateAbility(_config.ArmamentSpawnAbilityConfig);
            ability.Init(_enemy);
            ability.Execute();
            
        }
        public void Update(float deltaTime)
        {
            if (!_rotationTimer.IsFinished)
            {
                Vector3 targetDirection = Detector.Pos - Mover.Tr.position;
                Mover.Tr.forward= Vector3.Slerp(Mover.Tr.forward, targetDirection.normalized, _config.RotationSpeed * deltaTime);

            }
        }
        public bool AttackTimerFinished() => _attackTimer.IsFinished;
        public bool CooldownPassed() => _reloadTimer.IsFinished;
        public bool CanAttackAndCooldownPassed() => CanAttackCharacter() && CooldownPassed();
        public bool CanAttackCharacter() => Detector.CanAttackCharacter(_config.AttackRange);
    }
}