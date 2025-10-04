using Assets.Code.Common.Utils;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Player.Controller;
using Assets.Code.GamePlay.Player.PlayerStateMachine.States.AbstractStates;
using Project.Scripts.Utils;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.PlayerStateMachine.States
{
    public class FallingState : BaseAirState 
    {
        private Vector3 _fallStartPosition;

        public FallingState(ActorEntity player) : base(player)
        {
        }

        public override void OnEnter() 
        {
            _fallStartPosition = Mover.Tr.position;
        }
        public override void OnExit() 
        {
            Vector3 fallingDistance =   _fallStartPosition-Mover.Tr.position;
            float fallingHeight = Vector3.Dot(fallingDistance, Mover.Tr.up);
            _player.Get<PlayerEffects.PlayerEffects>().CameraMovingEffects.StartFallEffect(fallingHeight);
        }
        public override void FixedUpdate(float fixedDeltaTime) 
        {

            Vector3 momentum = Mover.GetMomentum();
            Vector3 verticalMomentum = VectorMath.ExtractDotVector(momentum, Mover.Tr.up);
            Vector3 horizontalMomentum = momentum - verticalMomentum;
            verticalMomentum -= Mover.Tr.up * (Mover.Gravity * fixedDeltaTime);
            
            horizontalMomentum = AdjustHorizontalAirMomentum(fixedDeltaTime, horizontalMomentum, Mover.CalculateMovementVelocity());
            
            float friction = Mover.AirFriction;
            horizontalMomentum = Vector3.MoveTowards(horizontalMomentum, Vector3.zero, friction * fixedDeltaTime);
            momentum = horizontalMomentum + verticalMomentum;
            
            Mover.SetMomentum(momentum);
        }

        public bool FallingToRising() => Mover.IsRising();
        public bool FallingToGrounded() => Mover.IsGrounded() && !Mover.IsGroundTooSteep();
        public bool FallingToSliding() => Mover.IsGrounded() && Mover.IsGroundTooSteep();

    }
}