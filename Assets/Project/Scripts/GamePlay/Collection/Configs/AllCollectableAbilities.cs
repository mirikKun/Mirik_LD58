using System.Collections.Generic;
using Project.Scripts.GamePlay.Player.Inventory.Configs;
using UnityEngine;

namespace Project.Scripts.GamePlay.Collection.Configs
{
    [CreateAssetMenu(fileName = "AllCollectableAbilities", menuName = "Configs/Collection/AllCollectableAbilities")]
    public class AllCollectableAbilities:ScriptableObject
    {
        [field:SerializeField] public List<BaseAbilityItem> CollectableAbilities { get; private set; } = new List<BaseAbilityItem>();


    }
}