using Assets.Code.GamePlay.Player.Inventory.Enums;
using Assets.Code.GamePlay.Player.Inventory.General;
using Assets.Code.GamePlay.Player.Inventory.UI.Core;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.Inventory.UI
{
    public class InactiveAbilitySlotUI:InactiveSlotUI
    {
        public override SlotType SlotType => SlotType.Ability;
        public override bool SameType(InventorySlotUI otherSlot)
        {
            return otherSlot.SlotType == SlotType;
        }
    }
}