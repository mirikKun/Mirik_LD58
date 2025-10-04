using Assets.Code.GamePlay.Abilities.General;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.GameplayStateMachine;
using Assets.Code.GamePlay.Player.Controller;
using Assets.Code.GamePlay.Player.PlayerStateMachine.StateConfigs;
using Assets.Code.GamePlay.Player.PlayerStateMachine.States.AbstractStates;
using ImprovedTimers;
using ImprovedTimers.Project.Scripts.Utils.Timers;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.PlayerStateMachine.States
{
    public class DashState:IState
    {
        protected readonly ActorEntity _player;
        private readonly DashBaseStateConfig _config;

        private Vector3 _dashDirection;
        private readonly CountdownTimer _dashTimer;
        private bool _jumpKeyIsPressed;
        private PlayerMover Mover => _player.Get<PlayerMover>();
        private PlayerController PlayerController => _player.Get<PlayerController>();
        private PlayerEffects.PlayerEffects Effects => _player.Get<PlayerEffects.PlayerEffects>();


        public DashState(ActorEntity player, DashBaseStateConfig config, AbilityInstance abilitiesInstance)
        {
            _player = player;
            _config = config;
            _dashTimer = new CountdownTimer(_config.DashDuration);
            abilitiesInstance.OnAbilityInput += HandleKeyInput;
        }

        private void HandleKeyInput(bool isButtonPressed)
        {
            _jumpKeyIsPressed = isButtonPressed;
        }

        public virtual void OnEnter()
        {
            Mover.OnGroundContactLost();
            
            _dashDirection = 
                PlayerController.GetInputMovementDirection().magnitude>0?
                Vector3.ProjectOnPlane(PlayerController.GetInputMovementDirection(), PlayerController.CameraTrY.up).normalized:
                PlayerController.CameraTrY.forward;
            Mover.SetMomentum(Vector3.zero);
            _dashTimer.Start();
            _jumpKeyIsPressed = false;
            
            Effects.CameraMovingEffects.SetTargetFOV(_config.UpdatedFov);

            Effects.TimeSlowEffect.PlayCurve();
        }

        public virtual void OnExit()
        {
            Mover.SetMomentum(_dashDirection * _config.DashExitSpeed);
            Effects.CameraMovingEffects.ResetFOV();

        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            Mover.SetMomentum(_dashDirection * _config.DashSpeed);
            
            
        }

        public bool GroundToDash() => _jumpKeyIsPressed;

        public bool AirToToDash() => _jumpKeyIsPressed&&_player.Get<PlayerStateMachineContainer>().HaveStateBeforeStateInHistory<IGroundState,DashState>();
        
        // public bool DashToRising() => (_dashTimer.IsFinished )&&_controller.IsRising();
        // public bool DashToFalling() => (_dashTimer.IsFinished || _controller.HitCeiling());
        
        public bool EndOfDash() => (_dashTimer.IsFinished || _player.Get<CeilingDetector>().HitCeiling());
        public bool WallClingingToDash() => _jumpKeyIsPressed;
        
        
    }
    public class DashLongState : DashState
    {
        public DashLongState(ActorEntity player, DashBaseStateConfig config, AbilityInstance abilitiesInstance) : base(player, config, abilitiesInstance){ }
    }
    public class DashEvadeState : DashState
    {
        public DashEvadeState(ActorEntity player, DashBaseStateConfig config, AbilityInstance abilitiesInstance) : base(player, config, abilitiesInstance){ }
    }
}