using Assets.Code.Common.Utils;
using Assets.Code.GamePlay.Abilities.General;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Player.Controller;
using Assets.Code.GamePlay.Player.PlayerStateMachine.StateConfigs;
using Assets.Code.GamePlay.Player.PlayerStateMachine.States.AbstractStates;
using ImprovedTimers;
using ImprovedTimers.Project.Scripts.Utils.Timers;
using Project.Scripts.Utils;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.PlayerStateMachine.States {
    public class JumpingState : BaseAirState {
        private readonly JumpMoveStateConfig _config;
        private readonly CountdownTimer _jumpTimer;


        private bool _jumpKeyIsPressed; 
        private bool _jumpKeyWasPressed; 
        private bool _jumpKeyWasLetGo;
        private bool _jumpInputIsLocked; 

        
        public JumpingState(ActorEntity player, JumpMoveStateConfig config, AbilityInstance abilitiesInstance) : base(player)
        {
            _config = config;
   
            _jumpTimer = new CountdownTimer(_config.JumpDuration);
            abilitiesInstance.OnAbilityInput += HandleJumpKeyInput;
        }

        public override void OnEnter() {
            Mover.OnGroundContactLost();
            OnJumpStart();
        }

        public override void OnExit()
        {
            _jumpInputIsLocked = false;
            ResetJumpKeys();
        }

        public override void FixedUpdate(float fixedDeltaTime) {
            
            
            Vector3 momentum = Mover.GetMomentum();
            Vector3 horizontalMomentum = momentum -VectorMath.ExtractDotVector(momentum, Mover.Tr.up);
            horizontalMomentum = AdjustHorizontalAirMomentum( fixedDeltaTime,horizontalMomentum, Mover.CalculateMovementVelocity());

            
            float friction = Mover.AirFriction;
            horizontalMomentum = Vector3.MoveTowards(horizontalMomentum, Vector3.zero, friction * fixedDeltaTime);
            momentum = horizontalMomentum;
            
            momentum = VectorMath.RemoveDotVector(momentum, Mover.Tr.up);
            momentum += Mover.Tr.up * _config.JumpSpeed;
            
            Mover.SetMomentum(momentum);

            ResetJumpKeys();
        }
        private void HandleJumpKeyInput(bool isButtonPressed)
        {
            if (!_jumpKeyIsPressed && isButtonPressed)
            {
                _jumpKeyWasPressed = true;
            }

            if (_jumpKeyIsPressed && !isButtonPressed)
            {
                _jumpKeyWasLetGo = true;
                _jumpInputIsLocked = false;
            }

            _jumpKeyIsPressed = isButtonPressed;
        }
        private void ResetJumpKeys()
        {
            _jumpKeyWasLetGo = false;
            _jumpKeyWasPressed = false;
        }

        public void OnJumpStart()
        {
            Vector3 momentum = Mover.GetMomentum();


            momentum += Mover.Tr.up * _config.JumpSpeed;
            _jumpTimer.Start();
            _jumpInputIsLocked = true;

            Mover.SetMomentum(momentum);
        }
        
        public bool GroundedToJumping()=>(_jumpKeyIsPressed ) && !_jumpInputIsLocked;
        public bool JumpingToRising() => _jumpTimer.IsFinished || _jumpKeyWasLetGo;
        public bool JumpingToFalling() => _player.Get<CeilingDetector>().HitCeiling();

    }
}