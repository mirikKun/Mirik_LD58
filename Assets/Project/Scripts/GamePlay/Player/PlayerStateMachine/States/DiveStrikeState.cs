using Assets.Code.Common.Utils;
using Assets.Code.GamePlay.Abilities.General;
using Assets.Code.GamePlay.Armaments;
using Assets.Code.GamePlay.Armaments.ArmamentBehaviour;
using Assets.Code.GamePlay.Armaments.Projectiles;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.GameplayStateMachine;
using Assets.Code.GamePlay.Player.Controller;
using Assets.Code.GamePlay.Player.PlayerStateMachine.StateConfigs;
using ImprovedTimers;
using ImprovedTimers.Project.Scripts.Utils.Timers;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.PlayerStateMachine.States
{
    public class DiveStrikeState : IState
    {
        private readonly ActorEntity _player;
        private readonly DiveStrikeMoveStateConfig _config;
        private CountdownTimer _diveStrikeTimer;

        private Vector3 _diveDirection;
        private Vector3 _startPosition;

        private bool _actionKeyIsPressed;

        private PlayerMover Mover => _player.Get<PlayerMover>();

        public DiveStrikeState(ActorEntity player, DiveStrikeMoveStateConfig config, AbilityInstance abilitiesInstance)
        {
            abilitiesInstance.OnAbilityInput += HandleActionInput;

            _player = player;
            _config = config;
        }

        private void HandleActionInput(bool isButtonPressed)
        {
            _actionKeyIsPressed = isButtonPressed;
        }

        public void OnEnter()
        {
            _diveDirection = -Mover.Tr.up;
            Vector3 momentum = _diveDirection * _config.DiveStrikeSpeed;
            Mover.SetMomentum(momentum);
            _startPosition = Mover.Tr.position;
        }

        public void OnExit()
        {
            float fallingHeight = _startPosition.y - Mover.Tr.position.y;
            _player.Get<PlayerEffects.PlayerEffects>().CameraMovingEffects.StartFallEffect(fallingHeight * 2);
            _player.Get<ArmamentsHolder>().CreateArmament(_config.ArmamentConfig)
                .With(new LifetimeArmamentBehaviour(_config.ArmamentConfig.Duration))
                .StartBehaviours();
        }

   
        public bool FallingToDiveStrike() => _actionKeyIsPressed;
        public bool RisingToDiveStrike() => _actionKeyIsPressed;
        public bool DiveStrikeToGrounded() => Mover.IsGrounded();
    }
}