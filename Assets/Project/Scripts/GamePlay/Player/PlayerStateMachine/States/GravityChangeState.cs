using Assets.Code.GamePlay.Abilities.General;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.GameplayStateMachine;
using Assets.Code.GamePlay.Physic.Raycast;
using Assets.Code.GamePlay.Player.Controller;
using Assets.Code.GamePlay.Player.PlayerStateMachine.StateConfigs;
using ImprovedTimers;
using ImprovedTimers.Project.Scripts.Utils.Timers;
using Project.Scripts.GamePlay.HUD.HudEffects;
using Project.Scripts.GamePlay.Player.Indication;
using Project.Scripts.GamePlay.Statuses;
using Project.Scripts.Sounds;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.PlayerStateMachine.States
{
    public class GravityChangeState : IState
    {
        private readonly ActorEntity _player;
        private readonly GravityChangeMoveStateConfig _config;
        private readonly RaycastSensor _raycastNearSensor;
        private readonly RaycastSensor _raycastFarSensor;
        private readonly CountdownTimer _gravityChangeTimer;
        private CountdownTimer _gravityFullChangeTimer;

        private PlayerMover Mover => _player.Get<PlayerMover>();
        private StatusController StatusController => _player.Get<StatusController>();
        private PlayerController PlayerController => _player.Get<PlayerController>();


        private Vector3 _gravityDirection;
        private Vector3 _lastGravityDirection;

        private Quaternion _startRotation;
        private Quaternion _changeRotation;

        private bool _actionKeyIsPressed;
        private float _angleTreashold = 0.01f;

        private bool _actionKeyPressedDown;
        private bool _actionKeyPressedUp;
        private bool _wrongGravity;


        public GravityChangeState(ActorEntity player, GravityChangeMoveStateConfig config,
            AbilityInstance abilitiesInstance)
        {
            _player = player;
            _config = config;

            _gravityChangeTimer = new CountdownTimer(_config.ChangingDuration);
            _gravityFullChangeTimer = new CountdownTimer(_config.GravityChangeFullDuration);

            abilitiesInstance.OnAbilityInput += HandleActionInput;

            _raycastNearSensor = new RaycastSensor(PlayerController.CameraTrY);
            _raycastNearSensor.CastLength = (_config.RaycastNearDistance);
            _raycastNearSensor.SetCastDirection(RaycastSensor.CastDirection.Forward);

            _raycastFarSensor = new RaycastSensor(PlayerController.CameraTrY);
            _raycastFarSensor.CastLength = (_config.GravityChangeJumpMaxVerticalDistance +
                                            _config.GravityChangeJumpMaxHorizontalDistance);
            _raycastFarSensor.SetCastDirection(RaycastSensor.CastDirection.Forward);
            _player.Get<AbilitiesIndicationController>().RangeIndication.EquipAbilityWithRange(RangeIndicationType.GravityChange,_raycastNearSensor);
        }

        public void Dispose()
        {
            //_controller.Input.Action1 -= HandleActionInput;
            _player.Get<AbilitiesIndicationController>().RangeIndication.UnequipAbilityWithRange(RangeIndicationType.GravityChange);
        }

        private void HandleActionInput(bool isButtonPressed)
        {
            _actionKeyPressedUp = false;
            _actionKeyPressedDown = false;


            if (_actionKeyIsPressed && !isButtonPressed)
            {
                _actionKeyPressedUp = true;
            }
            else if (!_actionKeyIsPressed && isButtonPressed)
            {
                _actionKeyPressedDown = true;
            }

            _actionKeyIsPressed = isButtonPressed;
        }

        public void OnEnter()
        {
            Mover.SetMomentum(Vector3.zero);
            _player.Get<SoundSource>().PlaySound(_config.Sound);

            _startRotation = Mover.Tr.rotation;
            _changeRotation = Quaternion.FromToRotation(Mover.Tr.up, _raycastNearSensor.GetNormal());
            _gravityChangeTimer.Start();
            _actionKeyIsPressed = false;


            if (!_wrongGravity || (_actionKeyIsPressed))
            {
                _gravityFullChangeTimer.Start();
                StatusController.AddStatus(new Status(StatusType.GravityChanged, _config.GravityChangeFullDuration));

                _wrongGravity = true;


                Mover.SetMomentum(Vector3.zero);
                _lastGravityDirection = Mover.Tr.up;
                _startRotation = Mover.Tr.rotation;
                _changeRotation = Quaternion.FromToRotation(Mover.Tr.up, _raycastNearSensor.GetNormal());
                _gravityChangeTimer.Start();
                _actionKeyIsPressed = false;
            }
            else
            {
                StatusController.RemoveStatus(StatusType.GravityChanged);

                _wrongGravity = false;
                Mover.SetMomentum(Vector3.zero);
                _lastGravityDirection = Vector3.up;
                _startRotation = Mover.Tr.rotation;
                _changeRotation = Quaternion.FromToRotation(Mover.Tr.up, _lastGravityDirection);
                _gravityChangeTimer.Start();
            }
        }

        public void OnExit()
        {
            Mover.Tr.rotation = _changeRotation * _startRotation;
            _actionKeyPressedUp = false;
            _actionKeyPressedDown = false;
        }

        public void Update(float deltaTime)
        {
            Mover.Tr.rotation = Quaternion.Lerp(_changeRotation * _startRotation, _startRotation,
                _gravityChangeTimer.Progress);
        }

        // private bool CanDoGravityJump()
        // {
        //     bool canJump=!_raycastNearSensor.CastAndCheck(_controller.CameraTrY.position)&&
        //                  Vector3.Angle(_raycastNearSensor.GetNormal(),_controller.Tr.up)>_angleTreashold&&
        //                  
        //                  
        //     
        // }
        private bool IsActionPressed()
        {
            bool actionKeyPressedUp = _actionKeyPressedUp;
          
            _actionKeyPressedUp = false;
            return actionKeyPressedUp;
        }

        public bool CanGravityChange() => IsActionPressed()&&
                                          (_wrongGravity||_raycastNearSensor.CastAndCheck(PlayerController.CameraTrY.position) &&
                                           Vector3.Angle(_raycastNearSensor.GetNormal(), Mover.Tr.up) >
                                           _angleTreashold);

        public bool GravityChangeDurationEnded() => _wrongGravity && _gravityFullChangeTimer.IsFinished;

        public bool GravityChangeToGrounded() => _gravityChangeTimer.IsFinished && Mover.IsGrounded();
        public bool GravityChangeToFalling() => _gravityChangeTimer.IsFinished;
    }
}