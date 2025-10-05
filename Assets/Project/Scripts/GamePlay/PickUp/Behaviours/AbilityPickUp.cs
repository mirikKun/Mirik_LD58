using Assets.Code.GamePlay.Player.Inventory;
using Assets.Code.GamePlay.Player.Inventory.Items;
using Project.Scripts.GamePlay.Collection.Systems;
using UnityEngine;
using Zenject;

namespace Assets.Code.GamePlay.PickUp.Behaviours
{
    public class AbilityPickUp:MonoBehaviour,IPickUp
    {

        [SerializeField] private BaseAbilityItem _abilityItem;
        private ICollectionSystem _collectionSystem;

        [Inject]
        private void Construct(ICollectionSystem collectionSystem)
        {
            _collectionSystem = collectionSystem;
        }
        
        
        public void PickUp()
        {
            _collectionSystem.TryPickAbility(_abilityItem);
            Destroy(gameObject);
        }

        public void HighLight()
        {
            //throw new System.NotImplementedException();
        }

        public void UnHighLight()
        {
            //throw new System.NotImplementedException();
        }
    }
}