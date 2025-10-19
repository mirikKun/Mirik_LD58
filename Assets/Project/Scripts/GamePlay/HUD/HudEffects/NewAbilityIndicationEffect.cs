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
    public class NewAbilityIndicationEffect : MonoBehaviour
    {
        [SerializeField] private NewAbilityIndicator _newAbilityIndicator;
        [SerializeField] private Transform _positionToFlyFrom;
        [SerializeField] private Transform _positionToFlyTo;
        [SerializeField] private Transform _holder;

        [SerializeField] private float _flyDuration = 1.3f;
        [SerializeField] private float _appearDuration = 0.3f;
        [SerializeField] private float _arcHeight = 100f;
        [Header("Circles")]
        private ICollectionSystem _collectionSystem;
        private List<NewAbilityIndicator> _currentIndicators = new List<NewAbilityIndicator>();
        private Stack<NewAbilityIndicator> _availableIndicators = new Stack<NewAbilityIndicator>();


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
            indicator.AbilityImage.sprite = abilityItem.Icon;
            _currentIndicators.Add(indicator);
            PlayAnimation(indicator, _positionToFlyFrom.position, _positionToFlyTo.position);
        }

        public void PlayAnimation(NewAbilityIndicator newAbilityIndicator, Vector3 from, Vector3 to)
        {
            newAbilityIndicator.Root.position = from;
            newAbilityIndicator.AbilityImage.transform.localScale = Vector3.zero;

            Vector3 midPoint = (from + to) / 2f;
            Vector3 direction = (-to + from).normalized;
            Vector3 perpendicular = new Vector3(-direction.y, direction.x, 0f);
            midPoint += perpendicular * _arcHeight;

            Vector3[] path = new Vector3[] { from, midPoint, to };

            foreach (var circleEffect in newAbilityIndicator.CircleEffects)
            {
                Sequence circleSequence = DOTween.Sequence();
                circleSequence.AppendInterval(circleEffect.Delay);
                circleEffect.CircleImage.transform.localScale = Vector3.zero;
                circleSequence.Append(circleEffect.CircleImage.transform.DOScale(circleEffect.MaxScale, circleEffect.Duration).SetEase(circleEffect.Curve));
                
            }

            Sequence sequence = DOTween.Sequence();

            sequence.Append(newAbilityIndicator.AbilityImage.transform.DOScale(1f, _appearDuration).SetEase(Ease.OutBack));

            sequence.Append(newAbilityIndicator.Root.DOPath(path, _flyDuration, PathType.CatmullRom)
                .SetEase(Ease.InOutQuad));

            sequence.Join(newAbilityIndicator.AbilityImage.transform.DOScale(0f, _flyDuration).SetEase(Ease.InQuad));
            sequence.onComplete += () => OnArrived(newAbilityIndicator);
        }

        private void OnArrived(NewAbilityIndicator indicator)
        {
            _currentIndicators.Remove(indicator);
            _availableIndicators.Push(indicator);
            indicator.gameObject.SetActive(false);
        }

        private NewAbilityIndicator GetNewIndicator()
        {
            if (_availableIndicators.Count > 0)
            {
                NewAbilityIndicator indicator = _availableIndicators.Pop();
                return indicator;
            }

            return Instantiate(_newAbilityIndicator, _holder);
        }
    }

    [Serializable]
    public class CircleEffect
    {
        public Image CircleImage;

        public float Delay;
        public float Duration;
        public float MaxScale;

        public AnimationCurve Curve;
    }
}