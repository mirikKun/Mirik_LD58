using System.Collections.Generic;
using Project.Scripts.GamePlay.Player.Inventory.General;
using UnityEngine;

namespace Project.Scripts.GamePlay.Player.Inventory.Configs
{
    [CreateAssetMenu(fileName = "PlayerStartInventory", menuName = "Configs/Inventory/Player Start Inventory")]
    public class PlayerStartInventory:ScriptableObject
    {
        [field:SerializeField] public List<AbilitySlot> ActiveAbilities { get; private set; } = new List<AbilitySlot>();
        [field:SerializeField] public List<BaseAbilityItem> InactiveAbilities { get; private set; } = new List<BaseAbilityItem>();
    }
}