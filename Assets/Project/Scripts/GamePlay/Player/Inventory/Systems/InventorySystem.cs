using System;
using System.Collections.Generic;
using Assets.Code.GamePlay.Player.Inventory.General;
using Code.Gameplay.StaticData;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.Inventory
{
    public class InventorySystem : IInventorySystem
    {
        private List<AbilitySlot> _activeAbilityItems;
        private List<IAbilityItem> _inactiveAbilityItems;

        public List<AbilitySlot> ActiveAbilities => _activeAbilityItems;
        public List<IAbilityItem> InactiveAbilities=> _inactiveAbilityItems;
        public event Action ActiveInventoryChanged;


        public void SetupInventory(List<AbilitySlot> activeAbilities, List<IAbilityItem> inactiveAbilities)
        {
            _activeAbilityItems = activeAbilities;
            _inactiveAbilityItems = inactiveAbilities;
        }

        public void OnActiveInventoryChanged()
        {
            ActiveInventoryChanged?.Invoke();
        }

        public void AddItem(IInventoryItem item)
        {
            if (item is IAbilityItem abilityItem)
            {
                _inactiveAbilityItems.Add(abilityItem);
            }
        }

        public void SetActiveAbility(AbilitySlot newActiveAbility)
        {
            if(newActiveAbility==null || newActiveAbility.EquippedAbility==null)
                return;
            _activeAbilityItems.Add(newActiveAbility);
        }

        public void SetInactiveAbility(IAbilityItem activeAbility)
        {
            if(activeAbility==null)
                return;
            _inactiveAbilityItems.Add(activeAbility);
        }

        public void RemoveActiveAbility(IAbilityItem activeAbility)
        {
            if (activeAbility == null)
                return;
            _activeAbilityItems.Remove(_activeAbilityItems.Find(x=>x.EquippedAbility==activeAbility));
        }
        public void RemoveInactiveAbility(IAbilityItem inactiveAbility)
        {
            if (inactiveAbility == null)
                return;
            _inactiveAbilityItems.Remove(inactiveAbility);
        }
        
    }
}