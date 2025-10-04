using Assets.Code.GamePlay.Abilities.Enums;
using Assets.Code.GamePlay.Player.Inventory.Enums;
using Assets.Code.GamePlay.Player.Inventory.General;
using Assets.Code.GamePlay.Player.Inventory.UI.Core;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.Inventory.UI
{
    public class ActiveAbilitySlotUI:ActiveSlotUI
    {


        [SerializeField] private AbilitySlotKey _abilitySlotKey;
        [SerializeField] private AbilitySlotType _abilitySlotType;
        public override SlotType SlotType => SlotType.Ability;

        public override bool SameType(InventorySlotUI otherSlot)
        {
            return otherSlot.SlotType == SlotType&&
                   (otherSlot is ActiveAbilitySlotUI activeAbilitySlotUI && activeAbilitySlotUI.AbilitySlotType == _abilitySlotType)||
                     (otherSlot is InactiveSlotUI inactiveSlotUI &&inactiveSlotUI.Item is IAbilityItem abilityItem && abilityItem.SlotType == _abilitySlotType)
                   
                   ;
        }
        public AbilitySlotKey AbilitySlotKey => _abilitySlotKey;
        public AbilitySlotType AbilitySlotType => _abilitySlotType;

  
    }
}