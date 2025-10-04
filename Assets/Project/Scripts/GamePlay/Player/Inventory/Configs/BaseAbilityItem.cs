using Assets.Code.GamePlay.Abilities.Enums;
using Assets.Code.GamePlay.Player.Abilities.Configs;
using Assets.Code.GamePlay.Player.Inventory.General;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.Inventory.Items
{
    [CreateAssetMenu(fileName = "BaseAbilityItem", menuName = "Configs/Inventory/BaseAbilityItem", order = 1)]
    public class BaseAbilityItem: BaseItem, IAbilityItem
    {
        [SerializeField] private AbilitySlotType _slotType;
        
        [SerializeField] private BaseAbilityConfig  _abilityConfig;
        public BaseAbilityConfig AbilityConfig=>_abilityConfig;
        public AbilitySlotType SlotType => _slotType;
    }
}