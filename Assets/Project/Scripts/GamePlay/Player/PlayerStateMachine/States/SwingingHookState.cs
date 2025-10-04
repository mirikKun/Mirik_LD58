using Assets.Code.Common.Utils;
using Assets.Code.GamePlay.Abilities.General;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.GameplayStateMachine;
using Assets.Code.GamePlay.Physic.Raycast;
using Assets.Code.GamePlay.Player.Controller;
using Assets.Code.GamePlay.Player.PlayerStateMachine.StateConfigs;
using Assets.Code.GamePlay.Player.PlayerStateMachine.States.AbstractStates;
using ImprovedTimers;
using ImprovedTimers.Project.Scripts.Utils.Timers;
using Project.Scripts.Utils;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.PlayerStateMachine.States
{
    public class SwingingHookState : IState
    {



        private readonly ActorEntity _player;

        private readonly SwingingHookMoveStateConfig _config;
        private RaycastSensor _raycastSensor;
        private CountdownTimer _hookTimer;
        private CountdownTimer _preparingTimer;
        private Vector3 _swingingPoint;

        private float _distance;
        private bool _actionKeyIsPressed;
        private bool _preparingStarted;
        private bool _preparingFinished;

        private PlayerMover Mover => _player.Get<PlayerMover>();
        private PlayerController PlayerController => _player.Get<PlayerController>();
        private PlayerEffects.PlayerEffects Effects => _player.Get<PlayerEffects.PlayerEffects>();

        public SwingingHookState(ActorEntity player, SwingingHookMoveStateConfig config,
            AbilityInstance abilitiesInstance)
        {
            _player = player;
            _config = config;


            abilitiesInstance.OnAbilityInput += HandleActionInput;


            _raycastSensor = new RaycastSensor(PlayerController.CameraTrY);
            _raycastSensor.CastLength = (_config.SwingingMaxDistance);
            _raycastSensor.SetCastDirection(RaycastSensor.CastDirection.Forward);

            _hookTimer = new CountdownTimer(_config.SwingingDuration);
            _preparingTimer = new CountdownTimer(_config.PreparingDuration);
        }

        public void Dispose()
        {
            //_controller.Input.Action3 -= HandleActionInput;
        }

        private void HandleActionInput(bool isButtonPressed)
        {
            _actionKeyIsPressed = isButtonPressed;
            if(!_preparingStarted&&_actionKeyIsPressed&& (CanGrapple))
            {
                _swingingPoint = _raycastSensor.GetPosition();

                _preparingTimer.Start();
                Effects.HookEffects.StartLineDrawing(_swingingPoint,_preparingTimer);
                _preparingStarted = true;
                
            }
            if(_preparingStarted&&!_actionKeyIsPressed)
            {
                _preparingTimer.Stop();
                Effects.HookEffects.ClearGrappleLine();
                _preparingStarted = false;
            }
        }

        public void OnEnter()
        {
            Mover.OnGroundContactLost();
            OnSwingingHookStart();
        }

        public void OnExit()
        {
            _actionKeyIsPressed = false;
            float momentumMagnitude = Mover.GetMomentum().magnitude;
            
            Mover.SetMomentum(Mover.GetMomentum() *_config.SwingingExitSpeedMultiplier );
            _preparingTimer.Stop();
            Effects.HookEffects.ClearGrappleLine();

        }

        private void OnSwingingHookStart()
        {
            _hookTimer.Start();


            Vector3 momentum = Mover.GetMomentum();
            Vector3 hookDirection = _swingingPoint - Mover.Tr.position;
            momentum = VectorMath.RemoveDotVector(momentum, -hookDirection);
            //momentum=Vector3.zero;
            momentum+=hookDirection.normalized*_config.StartSwingMomentum;
            momentum = Vector3.ClampMagnitude(momentum, _config.MaxSwingingSpeed);
            Mover.SetMomentum(momentum);
        }

        public void Update(float deltaTime)
        {
            Effects.HookEffects.DrawGrappleLine(_raycastSensor.GetPosition(), 1);
        }
        
        public void FixedUpdate(float fixedDeltaTime)
        {
            Vector3 momentum = Mover.GetMomentum();
            float friction = Mover.AirFriction;
            momentum = Vector3.MoveTowards(momentum, Vector3.zero, friction * fixedDeltaTime);
            Vector3 hookDirection = _swingingPoint - Mover.Tr.position;
            _distance=hookDirection.magnitude;
            
            Vector3 swingingDirectionOrbital = -GetPerpendicularInPlane(hookDirection, -PlayerController.CameraTrY.forward).normalized; 
            Vector3 swingingDirectionFroward = VectorMath.RemoveDotVector(PlayerController.CameraTrY.forward, -hookDirection.normalized);
            float aligning =_config.SwingingDirectionLerpCurve.Evaluate(VectorMath.GetDotProduct(PlayerController.CameraTrY.forward, hookDirection)) ;
 
            Vector3 swingingDirection=Vector3.Lerp(swingingDirectionFroward,swingingDirectionOrbital, aligning);
            float additionalSpeed = Mathf.Lerp( _config.GrapplingSpeed ,_config.SwingingSpeed,aligning);
            momentum = AdjustMaxMomentum(momentum, swingingDirection.normalized, additionalSpeed,fixedDeltaTime);
            momentum= VectorMath.RemoveDotVector(momentum, -hookDirection.normalized);
            Mover.SetMomentum(momentum);
        }
        
        Vector3 GetPerpendicularInPlane(Vector3 a, Vector3 b)
        {
            Vector3 normal = Vector3.Cross(a, b);
            Vector3 vector = Vector3.Cross(normal, a).normalized;
    
            if (Vector3.Dot(vector, b) < 0)
            {
                vector = -vector;
            }
    
            return vector;
        }
        private Vector3 AdjustMaxMomentum(Vector3 momentum, Vector3 addingMomentum, float speed,float fixedDeltaTime)
        {
            if (momentum.magnitude > _config.MaxSwingingSpeed)
            {
                if (VectorMath.GetDotProduct(addingMomentum, momentum.normalized) > 0f)
                {
                    addingMomentum = VectorMath.RemoveDotVector(addingMomentum, momentum.normalized).normalized;
                }

                momentum += speed * (fixedDeltaTime) * addingMomentum;
            }
            else
            {
                momentum += speed * (fixedDeltaTime) * addingMomentum;
            }
            momentum = Vector3.ClampMagnitude(momentum, _config.MaxSwingingSpeed);

            return momentum;
        }

        private bool CanGrapple =>
            _raycastSensor.CastAndCheck(PlayerController.CameraTrY.position) &&
            _raycastSensor.GetDistance() > _config.SwingingMinDistance;


        public bool GroundedToSwingingHook() => _preparingTimer.IsFinished&&_preparingStarted&&_actionKeyIsPressed;

        public bool AirToSwingingHook() => _preparingTimer.IsFinished&&_preparingStarted&&_actionKeyIsPressed  &&
                                           !_player.Get<PlayerStateMachineContainer>().HaveStateBeforeStateInHistory<GrapplingHookState, IGroundState>();

        public bool SwingingHookToRising() => (!_actionKeyIsPressed || _hookTimer.IsFinished||_distance <= _config.SwingingMinDistance) && Mover.IsRising();

        public bool SwingingHookToFalling() =>
            (!_actionKeyIsPressed || _hookTimer.IsFinished||_distance <= _config.SwingingMinDistance) && Mover.IsFalling();
    }
}