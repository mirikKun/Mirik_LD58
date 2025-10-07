using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.GameplayStateMachine;
using Assets.Code.GamePlay.Physic.Raycast;
using Assets.Code.GamePlay.Player.Controller;
using Assets.Code.GamePlay.Player.PlayerStateMachine.StateConfigs;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.PlayerStateMachine.States
{
    public class GravityChangeJumpState:IState
    {
        private readonly ActorEntity _player;

        private readonly RaycastSensor _raycastSensor;

        private readonly GravityChangeMoveStateConfig _config;
        private bool _actionKeyIsPressed;
        private PlayerMover Mover => _player.Get<PlayerMover>();
        private PlayerEffects.PlayerEffects Effects => _player.Get<PlayerEffects.PlayerEffects>();
        private PlayerController PlayerController => _player.Get<PlayerController>();

        public GravityChangeJumpState(ActorEntity player, GravityChangeMoveStateConfig config)
        {
            _player = player;
            _config = config;
            
            PlayerController.Input.Action1 += HandleActionInput;
            _raycastSensor = new RaycastSensor(PlayerController.CameraTrY);
            _raycastSensor.CastLength=(_config.GravityChangeJumpMaxVerticalDistance+_config.GravityChangeJumpMaxHorizontalDistance);
            _raycastSensor.SetCastDirection(RaycastSensor.CastDirection.Forward);


        }
        private void HandleActionInput(bool isButtonPressed)
        {
            _actionKeyIsPressed = isButtonPressed;
        }

        public void OnEnter()
        {
           // Effects.TrajectoryEffects.StartLineDrawing(_raycastSensor.GetPosition(),_raycastSensor.GetNormal());
        }

        public void Update(float deltaTime)
        {
            _raycastSensor.CastAndCheck(PlayerController.CameraTrY.position);
            //Effects.TrajectoryEffects.DrawGrappleLine(_raycastSensor.GetPosition(),_raycastSensor.GetNormal(),_raycastSensor.GetNormal());
        }

        public void OnExit()
        {
            _actionKeyIsPressed = false;
            //Effects.TrajectoryEffects.ClearGrappleLine();
        }
        
        public bool GroundedToGravityJumpChangePreparing()=>_raycastSensor.CastAndCheck(PlayerController.CameraTrY.position)&&
                                                            Vector3.Angle(_raycastSensor.GetNormal(),Mover.Tr.up)>float.Epsilon&&
                                                            _actionKeyIsPressed;
        public bool GravityJumpChangePreparingToGrounded()=>!_actionKeyIsPressed;

    }
}