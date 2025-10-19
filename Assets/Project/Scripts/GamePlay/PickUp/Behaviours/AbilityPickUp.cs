using Assets.Code.GamePlay.Player.Inventory;
using Assets.Code.GamePlay.Player.Inventory.Items;
using FMODUnity;
using Project.Scripts.GamePlay.Collection.Systems;
using Project.Scripts.Sounds;
using UnityEngine;
using Zenject;

namespace Assets.Code.GamePlay.PickUp.Behaviours
{
    public class AbilityPickUp:MonoBehaviour,IPickUp
    {

        [SerializeField] private BaseAbilityItem _abilityItem;
        [SerializeField] private EventReference _pickUpSound;
        private ICollectionSystem _collectionSystem;
        private ISoundsSystem _soundsSystem;

        [Inject]
        private void Construct(ICollectionSystem collectionSystem,ISoundsSystem soundsSystem)
        {
            _collectionSystem = collectionSystem;
            _soundsSystem = soundsSystem;
        }
        
        
        public void PickUp()
        {
            _collectionSystem.TryPickAbility(_abilityItem);
            _soundsSystem.PlayOneShot(_pickUpSound,transform.position);
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