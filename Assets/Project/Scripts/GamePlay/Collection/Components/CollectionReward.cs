using System;
using Project.Scripts.GamePlay.Collection.Systems;
using UnityEngine;
using Zenject;

namespace Project.Scripts.GamePlay.Collection.Components
{
    public class CollectionReward:MonoBehaviour
    {
        [SerializeField] private GameObject _objectToShow;
        private ICollectionSystem _collectionSystem;

        [Inject]
        private void Construct(ICollectionSystem collectionSystem)
        {
            _collectionSystem = collectionSystem;
        }

        private void Start()
        {
            _collectionSystem.CollectionUpdated += OnCollectionUpdated;
        }

        private void OnDestroy()
        {
            _collectionSystem.CollectionUpdated -= OnCollectionUpdated;
        }

        private void OnCollectionUpdated()
        {
            if (_collectionSystem.GetAllAvailableItemsCount() <= _collectionSystem.GetCollectedItemsCount())
            {
                _objectToShow.SetActive(true);
            }
        }
    }
}