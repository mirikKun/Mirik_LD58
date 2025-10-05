using System.Collections.Generic;
using Assets.Code.GamePlay.Armaments;
using Assets.Code.GamePlay.Player.Inventory;
using Assets.Code.GamePlay.Player.Inventory.Items;
using Project.Scripts.GamePlay.Collection.Configs;
using Project.Scripts.GamePlay.Player.Abilities.Configs;

namespace Project.Scripts.GamePlay.Collection.Systems
{
    public class CollectionSystem : ICollectionSystem
    {
        private List<BaseAbilityItem> _allAbilities=new List<BaseAbilityItem>();
        private List<BaseAbilityItem> _collectedAbilities=new List<BaseAbilityItem>();
        private readonly IInventorySystem _inventorySystem;


        private CollectionSystem(IInventorySystem inventorySystem)
        {
            _inventorySystem = inventorySystem;
        }
        public void Setup(AllCollectableAbilities  allCollectableAbilities)
        {
            _allAbilities=allCollectableAbilities.CollectableAbilities;
        }
        public void TryAddStealArmamentAbility(ArmamentConfig armamentTriggerArmamentConfig)
        {
            foreach (var collectedAbility in _collectedAbilities)
            {
                if (collectedAbility.AbilityConfig is ArmamentSpawnAbilityConfig armamentSpawnAbilityConfig &&
                    armamentSpawnAbilityConfig.ArmamentType == armamentTriggerArmamentConfig.Type)
                {
                    
                    return;
                }
            }
            
            
            foreach (var ability in _allAbilities)
            {
                if (ability.AbilityConfig is ArmamentSpawnAbilityConfig armamentSpawnAbilityConfig &&
                    armamentSpawnAbilityConfig.ArmamentType == armamentTriggerArmamentConfig.Type)
                {
                    _inventorySystem.AddItem(ability);
                    _collectedAbilities.Add(ability);
                }
            }
            
        }

        public int GetCollectedItemsCount()
        {
            throw new System.NotImplementedException();
        }

        public int GetAllAvailableItemsCount()
        {
            return _allAbilities.Count;
        }
    }
}