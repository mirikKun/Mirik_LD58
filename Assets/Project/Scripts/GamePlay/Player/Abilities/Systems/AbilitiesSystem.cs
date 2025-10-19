using System;
using System.Collections.Generic;
using Project.Scripts.GamePlay.Common.Input;
using Project.Scripts.GamePlay.Player.Abilities.Configs;
using Project.Scripts.GamePlay.Player.Abilities.Enums;
using Project.Scripts.GamePlay.Player.Abilities.General;
using Project.Scripts.GamePlay.Player.Inventory.General;
using Project.Scripts.GamePlay.Player.Inventory.Systems;

namespace Project.Scripts.GamePlay.Player.Abilities.Systems
{
    public class AbilitiesSystem : IAbilitiesSystem
    {
        private List<AbilityInstance> _abilities;
        private IInventorySystem _inventorySystem;
        private readonly IInputReader _inputReader;
        private PlayerStartAbilities _playerStartAbilities;
        public List<AbilityInstance> Abilities => _abilities;
        public event Action AbilitiesListChanged;


        private AbilitiesSystem(IInventorySystem inventorySystem, IInputReader inputReader)
        {
            _inventorySystem = inventorySystem;
            _inputReader = inputReader;
        }

        public void Setup(PlayerStartAbilities playerStartAbilities)
        {
            _playerStartAbilities = playerStartAbilities;
            SetupStates();
            _inventorySystem.ActiveInventoryChanged += SetupStates;
        }


        private void SetupStates()
        {
            if (_abilities != null)
            { 
                foreach (var ability in _abilities)
                {
                    DisconnectFromInput(ability);
                }
            }
            
            _abilities = new List<AbilityInstance>();
            foreach (BaseAbilityConfig  baseAbilityConfig in _playerStartAbilities.BaseAbilityConfigs)
            {
                AbilityInstance abilityInstance = new AbilityInstance(AbilitySlotKey.None, baseAbilityConfig);
                _abilities.Add(abilityInstance);
            }

            var inventoryAbilityConfigs = _inventorySystem.ActiveAbilities;
            foreach (AbilitySlot stateConfig in inventoryAbilityConfigs)
            {
                AbilityInstance abilityInstance = new AbilityInstance(stateConfig.SlotKey, stateConfig.EquippedAbility.AbilityConfig);
                _abilities.Add(abilityInstance);
                ConnectToInput(abilityInstance);
            }

            AbilitiesListChanged?.Invoke();
        }

        private void ConnectToInput(AbilityInstance abilityInstance)
        {
            switch (abilityInstance.SlotKey)
            {
                case AbilitySlotKey.SpaceAction:
                    _inputReader.Jump += abilityInstance.OnKeyInput;
                    break;
                case AbilitySlotKey.CtrlAction:
                    _inputReader.Crouch += abilityInstance.OnKeyInput;
                    break;
                case AbilitySlotKey.ShiftAction:
                    _inputReader.Dash += abilityInstance.OnKeyInput;
                    break;
                case AbilitySlotKey.AbilityAction:
                    _inputReader.Action1 += abilityInstance.OnKeyInput;
                    break;
                case AbilitySlotKey.AbilityAction2:
                    _inputReader.Action2 += abilityInstance.OnKeyInput;
                    break;
                case AbilitySlotKey.AbilityAction3:
                    _inputReader.Action3 += abilityInstance.OnKeyInput;
                    break;
                case AbilitySlotKey.MouseLeft:
                    _inputReader.Attack += abilityInstance.OnKeyInput;      
                    break;
                case AbilitySlotKey.MouseRight:
                    _inputReader.AttackAlt += abilityInstance.OnKeyInput;
                    break;
            }
        }

        private void DisconnectFromInput(AbilityInstance abilityInstance)
        {
            switch (abilityInstance.SlotKey)
            {
                case AbilitySlotKey.SpaceAction:
                    _inputReader.Jump -= abilityInstance.OnKeyInput;
                    break;
                case AbilitySlotKey.CtrlAction:
                    _inputReader.Crouch -= abilityInstance.OnKeyInput;
                    break;
                case AbilitySlotKey.ShiftAction:
                    _inputReader.Dash -= abilityInstance.OnKeyInput;
                    break;
                case AbilitySlotKey.AbilityAction:
                    _inputReader.Action1 -= abilityInstance.OnKeyInput;
                    break;
                case AbilitySlotKey.AbilityAction2:
                    _inputReader.Action2 -= abilityInstance.OnKeyInput;
                    break;
                case AbilitySlotKey.AbilityAction3:
                    _inputReader.Action3 -= abilityInstance.OnKeyInput;
                    break;
                case AbilitySlotKey.MouseLeft:
                    _inputReader.Attack -= abilityInstance.OnKeyInput;      
                    break;
                case AbilitySlotKey.MouseRight:
                    _inputReader.AttackAlt -= abilityInstance.OnKeyInput;
                    break;
            
            }
        }
    }
}