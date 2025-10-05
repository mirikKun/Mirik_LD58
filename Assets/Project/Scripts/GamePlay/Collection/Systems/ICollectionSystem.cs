using Assets.Code.GamePlay.Armaments;
using Assets.Code.GamePlay.Player.Inventory.Items;
using Project.Scripts.GamePlay.Collection.Configs;

namespace Project.Scripts.GamePlay.Collection.Systems
{
    public interface ICollectionSystem
    {
        void TryAddStealArmamentAbility(ArmamentConfig armamentTriggerArmamentConfig);
        int GetCollectedItemsCount();
        int GetAllAvailableItemsCount();
        void Setup(AllCollectableAbilities  allCollectableAbilities);
        event System.Action CollectionUpdated;
        void TryPickAbility(BaseAbilityItem abilityItem);
    }
}