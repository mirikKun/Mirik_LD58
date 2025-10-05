using System;
using Assets.Code;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Physic.ColliderLogic;
using Assets.Code.GamePlay.Player.Controller;
using Project.Scripts.GamePlay.Collection.Systems;
using UnityEngine;
using Zenject;

namespace Project.Scripts.GamePlay.Player.StealSystem
{
    public class StealingController : EntityComponent
    {
        [SerializeField] private float _maxMana = 100;
        [SerializeField] private float _manaRestoreRate = 15;
        [SerializeField] private float _manaSpendRate = 10;
        [SerializeField] private StealingEffect _stealingEffect;
        [SerializeField] private ParryTrigger _parryTrigger;
        private IInputReader Input => Entity.Get<PlayerController>().Input;
        private bool _keyIsPressed;
        private float _currentMana;
        private ICollectionSystem _collectionSystem;
        public float CurrentMana => _currentMana;
        public event Action<float> ManaChanged;

        [Inject]
        private void Construct(ICollectionSystem collectionSystem)
        {
            _collectionSystem = collectionSystem;
        }
        public void Start()
        {
            Input.AttackAlt += HandleKeyInput;
            _currentMana = _maxMana;
            ManaChanged?.Invoke(_currentMana / _maxMana);
            _parryTrigger.OnHitEvent += OnParryTriggered;
        }

        private void OnParryTriggered(IAttackTrigger attackTrigger)
        {
            if (attackTrigger is ArmamentTrigger armamentTrigger  && armamentTrigger.ArmamentConfig!=null)
            {
                _collectionSystem.TryAddStealArmamentAbility(armamentTrigger.ArmamentConfig);
                armamentTrigger.Dismiss();
            }
        }

        public void Tick(float deltaTime)
        {
            _stealingEffect.Tick(deltaTime);
            if (_keyIsPressed && _currentMana > 0)
            {
                _currentMana -= _manaSpendRate * deltaTime;
                if (_currentMana < 0)
                {
                    StopStealing();
                    _currentMana = 0;
                }

                ManaChanged?.Invoke(_currentMana / _maxMana);
            }
            else if (!_keyIsPressed)
            {
                _currentMana += _manaRestoreRate * deltaTime;
                if (_currentMana > _maxMana) _currentMana = _maxMana;
                ManaChanged?.Invoke(_currentMana / _maxMana);

            }
        }

        private void StartStealing()
        {
            _stealingEffect.StartStealingEffect();
            _parryTrigger.gameObject.SetActive(true);

        }

        private void StopStealing()
        {
            _stealingEffect.StopStealingEffect();
            _parryTrigger.gameObject.SetActive(false);

        }
        private void HandleKeyInput(bool isButtonPressed)
        {
            _keyIsPressed = isButtonPressed;
            if (isButtonPressed)
            {
                StartStealing();
            }
            else
            {
                StopStealing();
            }
        }
    }
}