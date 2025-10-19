using System;
using Project.Scripts.GamePlay.Player.Abilities.Enums;
using Project.Scripts.GamePlay.Player.Inventory.Configs;
using UnityEngine;

namespace Project.Scripts.GamePlay.Player.Inventory.General
{
    [Serializable]
    public class AbilitySlot
    {
        [field:SerializeField] public AbilitySlotKey SlotKey { get; private set; }
        [field:SerializeField] public AbilitySlotType SlotType { get; private set; }

        [field:SerializeField] public BaseAbilityItem EquippedAbility { get; private set; }

        public AbilitySlot(AbilitySlotKey slotKey, IAbilityItem equippedAbility)
        {
            SlotKey = slotKey;
            EquippedAbility = equippedAbility as BaseAbilityItem;
        }
    }
}