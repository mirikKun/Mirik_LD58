using System;
using Assets.Code.GamePlay.Abilities.Enums;
using Assets.Code.GamePlay.Player.Inventory.Items;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.Inventory.General
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