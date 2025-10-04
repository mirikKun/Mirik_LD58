using System.Collections.Generic;
using Assets.Code.GamePlay.Player.Inventory.General;
using Assets.Code.GamePlay.Player.Inventory.Items;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.Inventory.Configs
{
    [CreateAssetMenu(fileName = "PlayerStartInventory", menuName = "Configs/Inventory/Player Start Inventory")]
    public class PlayerStartInventory:ScriptableObject
    {
        [field:SerializeField] public List<AbilitySlot> ActiveAbilities { get; private set; } = new List<AbilitySlot>();
        [field:SerializeField] public List<BaseAbilityItem> InactiveAbilities { get; private set; } = new List<BaseAbilityItem>();
    }
}