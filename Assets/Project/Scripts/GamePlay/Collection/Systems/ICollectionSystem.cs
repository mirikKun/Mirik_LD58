using Assets.Code.GamePlay.Player.Inventory.Items;
using Project.Scripts.GamePlay.Armaments.Configs;
using Project.Scripts.GamePlay.Collection.Configs;

namespace Project.Scripts.GamePlay.Collection.Systems
{
    public interface ICollectionSystem
    {
        bool TryAddStealArmamentAbility(ArmamentConfig armamentTriggerArmamentConfig);
        int GetCollectedItemsCount();
        int GetAllAvailableItemsCount();
        void Setup(AllCollectableAbilities  allCollectableAbilities);
        event System.Action<BaseAbilityItem> CollectionUpdated;
        void TryPickAbility(BaseAbilityItem abilityItem);
    }
}