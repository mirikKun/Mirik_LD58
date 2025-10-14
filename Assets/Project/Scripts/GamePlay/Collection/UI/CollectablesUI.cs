using System;
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
            OnCollectionUpdated();
            _initialized = true;
        }

        private void OnDestroy()
        {
            _collectionSystem.CollectionUpdated-=OnCollectionUpdated;
        }

        private void OnCollectionUpdated()
        {
            int current=_collectionSystem.GetCollectedItemsCount();
            int max=_collectionSystem.GetAllAvailableItemsCount();
            _text.text = $"{current}/{max}";
            if (_initialized)
            {
                _collectableUpdateEffect.PlayEffect();
            }
        }
    }
}