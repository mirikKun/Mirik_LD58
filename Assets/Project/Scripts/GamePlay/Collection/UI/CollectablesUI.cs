using System;
using Assets.Code.GamePlay.Player.Inventory.Items;
using Project.Scripts.GamePlay.Collection.Systems;
using TMPro;
using UnityEngine;
using Zenject;

namespace Project.Scripts.GamePlay.Collection.UI
{
    public class CollectablesUI:MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private CollectableUpdateEffect _collectableUpdateEffect;
        private ICollectionSystem _collectionSystem;
        private bool _initialized;

        [Inject]
        private void Construct(ICollectionSystem collectionSystem)
        {
            _collectionSystem = collectionSystem;
        }

        private void Start()
        {
            _collectionSystem.CollectionUpdated+=OnCollectionUpdated;
            UpdateCollectablesCount();
            _initialized = true;
        }

        private void OnDestroy()
        {
            _collectionSystem.CollectionUpdated-=OnCollectionUpdated;
        }

        private void OnCollectionUpdated(BaseAbilityItem baseAbilityItem)
        {
            UpdateCollectablesCount();
            if (_initialized)
            {
                _collectableUpdateEffect.PlayEffect();
            }
        }

        private void UpdateCollectablesCount()
        {
            int current=_collectionSystem.GetCollectedItemsCount();
            int max=_collectionSystem.GetAllAvailableItemsCount();
            _text.text = $"{current}/{max}";
        }
    }
}