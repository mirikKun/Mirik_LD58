using Assets.Code.GamePlay.Common.Entity;
using ImprovedTimers.Project.Scripts.Utils.Timers;
using Project.Scripts.GamePlay.Common.GameplayStateMachine;
using Project.Scripts.GamePlay.Enemies.EnemyController.Mediator;
using Project.Scripts.GamePlay.Enemies.EnemyController.StateConfigs;
using UnityEngine;

namespace Project.Scripts.GamePlay.Enemies.EnemyController.States
{
    public class ChasingState:IState
    {
        private readonly EnemyEntity _enemy;
        private readonly CountdownTimer _chasingReturnTimer;
        private readonly ChasingStateConfig _chasingStateConfig;

        private Transform _targetTransform;
        private EnemyMover Mover => _enemy.Get<EnemyMover>();
        private EnemyCombat Combat => _enemy.Get<EnemyCombat>();
        private EnemyAnimator Animator => _enemy.Get<EnemyAnimator>();
        private CharacterDetector Detector => _enemy.Get<CharacterDetector>();
        public ChasingState(ActorEntity enemy,ChasingStateConfig chasingStateConfig)
        {
            _enemy = enemy as EnemyEntity;
            _chasingStateConfig = chasingStateConfig;
            _chasingReturnTimer = new CountdownTimer(chasingStateConfig.ChasingDuration);
        }
        public void OnEnter()
        {
            _targetTransform = Detector.DetectedCharacter;
            Mover.SetDestination(_targetTransform.position,_chasingStateConfig.MovingSpeed);
            _chasingReturnTimer.Start();
            Animator.StartAnimation(_chasingStateConfig.AnimationHash);
            
            if(!Detector.DetectedByOther)
                _enemy.Send(new EnemyDetectPayload.Builder(_enemy).WithContent(true).Build(),Detector.IsNearAlly);
        }
        public void OnExit()
        {
            Mover.StopAgent();

            _chasingReturnTimer.Stop();
            if(LoseCharacter())
            {
                _enemy.Send(new EnemyDetectPayload.Builder(_enemy).WithContent(false).Build(),Detector.IsNearAlly);
            }
        }
        public void Update(float deltaTime)
        {
            Mover.SetDestination(_targetTransform.position,_chasingStateConfig.MovingSpeed);

            if (Detector.CanDetectCharacter)
            {
                _chasingReturnTimer.Start();
            }
        }
        
        public bool DetectCharacter() => Detector.CanDetectCharacter||!_chasingReturnTimer.IsFinished;
        public bool LoseCharacter() => !DetectCharacter();
        
    }
}