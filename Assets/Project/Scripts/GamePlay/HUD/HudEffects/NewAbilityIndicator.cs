using System;
using System.Collections.Generic;
using Assets.Code.GamePlay.Player.Inventory.Items;
using DG.Tweening;
using Project.Scripts.GamePlay.Collection.Systems;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Scripts.GamePlay.HUD.HudEffects
{
    public class NewAbilityIndicator : MonoBehaviour
    {
        [SerializeField] private Image _abilityImage;
        [SerializeField] private Transform _positionToFlyFrom;
        [SerializeField] private Transform _positionToFlyTo;
        [SerializeField] private Transform _holder;

        [SerializeField] private float _flyDuration = 1.3f;
        [SerializeField] private float _appearDuration = 0.3f;
        [SerializeField] private float _arcHeight = 100f;

        private ICollectionSystem _collectionSystem;
        private List<Image> _currentIndicators = new List<Image>();
        private Stack<Image> _availableIndicators = new Stack<Image>();


        [Inject]
        private void Construct(ICollectionSystem collectionSystem)
        {
            _collectionSystem = collectionSystem;
        }

        private void Start()
        {
            _collectionSystem.CollectionUpdated += OnCollectionUpdated;
        }

        private void OnCollectionUpdated(BaseAbilityItem abilityItem)
        {
            var indicator = GetNewIndicator();
            indicator.gameObject.SetActive(true);
            indicator.sprite = abilityItem.Icon;
            _currentIndicators.Add(indicator);
            PlayAnimation(indicator, _positionToFlyFrom.position, _positionToFlyTo.position);
        }

        public void PlayAnimation(Image icon, Vector3 from, Vector3 to)
        {
            Transform indicator = icon.transform;
            indicator.position = from;
            indicator.localScale = Vector3.zero;

            Vector3 midPoint = (from + to) / 2f;
            Vector3 direction = (-to + from).normalized;
            Vector3 perpendicular = new Vector3(-direction.y, direction.x, 0f);
            midPoint += perpendicular * _arcHeight;

            Vector3[] path = new Vector3[] { from, midPoint, to };

            Sequence sequence = DOTween.Sequence();

            sequence.Append(indicator.DOScale(1f, _appearDuration).SetEase(Ease.OutBack));

            sequence.Append(indicator.DOPath(path, _flyDuration, PathType.CatmullRom)
                .SetEase(Ease.InOutQuad));

            sequence.Join(indicator.DOScale(0f, _flyDuration).SetEase(Ease.InQuad));
            sequence.onComplete += () => OnArrived(icon);
        }

        private void OnArrived(Image icon)
        {
            _currentIndicators.Remove(icon);
            _availableIndicators.Push(icon);
            icon.gameObject.SetActive(false);
        }

        private Image GetNewIndicator()
        {
            if (_availableIndicators.Count > 0)
            {
                Image indicator = _availableIndicators.Pop();
                return indicator;
            }

            return Instantiate(_abilityImage, _holder);
        }
    }
}