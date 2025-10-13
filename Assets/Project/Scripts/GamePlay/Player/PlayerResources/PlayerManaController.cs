using System;
using Assets.Code.GamePlay.Common.Entity;
using UnityEngine;

namespace Project.Scripts.GamePlay.Player.PlayerResources
{
    public class PlayerManaController : EntityComponent
    {
        [SerializeField] private float _maxMana = 100;
        [SerializeField] private float _manaRestoreRate = 15;

        private float _currentMana;
        private bool _manaWasSpent;
        public float CurrentMana => _currentMana;
        public event Action<float> ManaChanged;
        public event Action ManaEnded;

   
        public void Start()
        {
            _currentMana = _maxMana;
            ManaChanged?.Invoke(_currentMana / _maxMana);
        }

     

        public void SpendMana(float spentMana)
        {
            _manaWasSpent = true;
            _currentMana -= spentMana;
            if (_currentMana < 0)
            {
                _currentMana = 0;
                ManaEnded?.Invoke();
            }

            ManaChanged?.Invoke(_currentMana / _maxMana);
        }

        public void Tick(float deltaTime)
        {
            if (!_manaWasSpent)
            {
                _currentMana += _manaRestoreRate * deltaTime;
                if (_currentMana > _maxMana) _currentMana = _maxMana;
                ManaChanged?.Invoke(_currentMana / _maxMana);
            }

            _manaWasSpent = false;
        }
    }
}