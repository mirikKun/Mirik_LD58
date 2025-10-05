using Assets.Code.GamePlay.Armaments;
using Project.Scripts.GamePlay.Collection.Configs;

namespace Project.Scripts.GamePlay.Collection.Systems
{
    public interface ICollectionSystem
    {
        void TryAddStealArmamentAbility(ArmamentConfig armamentTriggerArmamentConfig);
        int GetCollectedItemsCount();
        int GetAllAvailableItemsCount();
        void Setup(AllCollectableAbilities  allCollectableAbilities);
    }
}