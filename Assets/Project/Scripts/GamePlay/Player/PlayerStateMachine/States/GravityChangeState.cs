using Assets.Code.GamePlay.Abilities.General;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.GameplayStateMachine;
using Assets.Code.GamePlay.Physic.Raycast;
using Assets.Code.GamePlay.Player.Controller;
using Assets.Code.GamePlay.Player.PlayerStateMachine.StateConfigs;
using ImprovedTimers;
using ImprovedTimers.Project.Scripts.Utils.Timers;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.PlayerStateMachine.States
{
    public class GravityChangeState:IState
    {
        private readonly ActorEntity _player;
        private readonly GravityChangeMoveStateConfig _config;
        private readonly RaycastSensor _raycastNearSensor;
        private readonly RaycastSensor _raycastFarSensor;
        private readonly CountdownTimer _gravityChangeTimer;
        private PlayerMover Mover => _player.Get<PlayerMover>();
        private PlayerController PlayerController => _player.Get<PlayerController>();


        private Vector3 _gravityDirection;
        private Quaternion _startRotation;
        private Quaternion _changeRotation;

        private bool _actionKeyIsPressed;
        private float _angleTreashold = 0.01f;

        private bool _actionKeyPressedDown;
        private bool _actionKeyPressedUp;


        public GravityChangeState(ActorEntity player, GravityChangeMoveStateConfig config,
            AbilityInstance abilitiesInstance)
        {
            _player= player;
            _config = config;

            _gravityChangeTimer = new CountdownTimer(_config.ChangingDuration);
            abilitiesInstance.OnAbilityInput += HandleActionInput;

            _raycastNearSensor = new RaycastSensor(PlayerController.CameraTrY);
            _raycastNearSensor.CastLength=(_config.RaycastNearDistance);
            _raycastNearSensor.SetCastDirection(RaycastSensor.CastDirection.Forward);
            
            _raycastFarSensor = new RaycastSensor(PlayerController.CameraTrY);
            _raycastFarSensor.CastLength=(_config.GravityChangeJumpMaxVerticalDistance+_config.GravityChangeJumpMaxHorizontalDistance);
            _raycastFarSensor.SetCastDirection(RaycastSensor.CastDirection.Forward);
        }
        public void Dispose()
        {
            //_controller.Input.Action1 -= HandleActionInput;
        }

        private void HandleActionInput(bool isButtonPressed)
        {
            _actionKeyPressedUp = false;
            _actionKeyPressedDown = false;


            if(_actionKeyIsPressed&& !isButtonPressed)
            {
                _actionKeyPressedUp = true;
                
                
            }
            else if(!_actionKeyIsPressed && isButtonPressed)
            {
                _actionKeyPressedDown = true;
            }
            _actionKeyIsPressed = isButtonPressed;
            
            
        }

        public void OnEnter()
        {
            Mover.SetMomentum(Vector3.zero);
    
            _startRotation = Mover.Tr.rotation;
            _changeRotation=Quaternion.FromToRotation(Mover.Tr.up, _raycastNearSensor.GetNormal());
            _gravityChangeTimer.Start();
            _actionKeyIsPressed = false;

        }

        public void OnExit()
        {
            Mover.Tr.rotation = _changeRotation * _startRotation;
            _actionKeyPressedUp = false;
            _actionKeyPressedDown = false;
        }

        public void Update(float deltaTime)
        {
            Mover.Tr.rotation = Quaternion.Lerp(_changeRotation * _startRotation,_startRotation,_gravityChangeTimer.Progress);
        }

        // private bool CanDoGravityJump()
        // {
        //     bool canJump=!_raycastNearSensor.CastAndCheck(_controller.CameraTrY.position)&&
        //                  Vector3.Angle(_raycastNearSensor.GetNormal(),_controller.Tr.up)>_angleTreashold&&
        //                  
        //                  
        //     
        // }
        
        public bool GroundedToGravityChange()=>_raycastNearSensor.CastAndCheck(PlayerController.CameraTrY.position)&&
                                               Vector3.Angle(_raycastNearSensor.GetNormal(),Mover.Tr.up)>_angleTreashold&&
                                               _actionKeyPressedUp;
        public bool GravityChangeToGrounded()=>_gravityChangeTimer.IsFinished&&Mover.IsGrounded();
        public bool GravityChangeToFalling() => _gravityChangeTimer.IsFinished;

    }
}