using Project.Scripts.GamePlay.Player.Inventory.Enums;
using Project.Scripts.GamePlay.Player.Inventory.UI.Core;

namespace Project.Scripts.GamePlay.Player.Inventory.UI
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