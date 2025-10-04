using Assets.Code.GamePlay.Abilities.Enums;
using Assets.Code.GamePlay.Player.Abilities.Configs;

namespace Assets.Code.GamePlay.Player.Inventory.General
{
    public interface IAbilityItem:IInventoryItem
    {
        public BaseAbilityConfig AbilityConfig { get;  }
        public AbilitySlotType SlotType { get;   }
    }
}