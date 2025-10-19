using System;
using System.Collections.Generic;
using Project.Scripts.GamePlay.Player.Inventory.General;

namespace Project.Scripts.GamePlay.Player.Inventory.Systems
{
    public interface IInventorySystem
    {
        void SetupInventory(List<AbilitySlot> activeAbilities, List<IAbilityItem> inactiveAbilities);

        void OnActiveInventoryChanged();
        void AddItem(IInventoryItem item);

        void SetActiveAbility(AbilitySlot newActiveAbility);
        void RemoveActiveAbility(IAbilityItem activeAbility);
        void SetInactiveAbility(IAbilityItem activeAbility);
        void RemoveInactiveAbility(IAbilityItem inactiveAbility);
        List<AbilitySlot> ActiveAbilities { get; }
        List<IAbilityItem> InactiveAbilities { get; }
        event Action ActiveInventoryChanged;

    }
}