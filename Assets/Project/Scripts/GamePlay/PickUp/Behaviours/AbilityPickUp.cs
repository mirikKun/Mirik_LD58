using Assets.Code.GamePlay.Player.Inventory;
using Assets.Code.GamePlay.Player.Inventory.Items;
using UnityEngine;
using Zenject;

namespace Assets.Code.GamePlay.PickUp.Behaviours
{
    public class AbilityPickUp:MonoBehaviour,IPickUp
    {

        [SerializeField] private BaseAbilityItem _abilityItem;
        private IInventorySystem _inventorySystem;

        [Inject]
        private void Construct(IInventorySystem inventorySystem)
        {
            _inventorySystem = inventorySystem;
        }
        
        
        public void PickUp()
        {
            _inventorySystem.AddItem(_abilityItem);
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