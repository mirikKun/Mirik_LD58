using System;
using System.Collections.Generic;
using Assets.Code.GamePlay.Stats;
using Code.Gameplay.StaticData;
using ImprovedTimers.Project.Scripts.Utils.Timers;
using Project.Scripts.GamePlay.Stats;
using Project.Scripts.GamePlay.Statuses;
using UnityEngine;
using Zenject;

namespace Project.Scripts.GamePlay.Player.PlayerIndicators
{
    public class PlayerAbilitiesPanel : MonoBehaviour
    {
        [SerializeField] private StatusController _statusController;
        [SerializeField] private StatsController _statsController;

        [Header("Indication")] [SerializeField]
        private Transform _indicatorsTransform;

        [SerializeField] private AbilityDurationIndicator _abilityDurationIndicatorPrefab;
        
        private List<AbilityDurationIndicator> _currentIndicators = new List<AbilityDurationIndicator>();
        private Stack<AbilityDurationIndicator> _availableIndicators = new Stack<AbilityDurationIndicator>();
       
        private StatusIconsConfig _statusIcons;
        private StatIconsConfig _statIcons;
        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
         
        }
        private void Start()
        {
            _statusController.StatusAdded += OnStatusAdded;
            _statsController.Mediator.StatModifierAdded += OnStatModifierAdded;
            _statusIcons = _staticDataService.GetStatusIconsConfig();
            _statIcons = _staticDataService.GetStatIconsConfig();
        }

        private void OnDestroy()
        {
            _statusController.StatusAdded -= OnStatusAdded;
            _statsController.Mediator.StatModifierAdded -= OnStatModifierAdded;
        }

        private void OnStatModifierAdded(StatModifier modifier)
        {
            StartDurationIndication(modifier.CountdownTimer, _statIcons.GetIcon(modifier.StatType));
        }

        private void OnStatusAdded(Status status)
        {
            StartDurationIndication(status.CountdownTimer, _statusIcons.GetIcon(status.StatusType));

        }
        
        public void StartDurationIndication(CountdownTimer timer, Sprite sprite)
        {
            var indicator = GetNewIndicator();
            indicator.gameObject.SetActive(true);

            _currentIndicators.Add(indicator);
            indicator.Init(sprite, timer);
            indicator.TimerEnded += OnTimerStop;
        }

        private void Update()
        {
            foreach (var indicator in _currentIndicators)
            {
                indicator.UpdateProgress();
            }
        }

        private void OnTimerStop(AbilityDurationIndicator abilityDurationIndicator)
        {
            _currentIndicators.Remove(abilityDurationIndicator);
            _availableIndicators.Push(abilityDurationIndicator);
            abilityDurationIndicator.gameObject.SetActive(false);
        }

        private AbilityDurationIndicator GetNewIndicator()
        {
            if (_availableIndicators.Count > 0)
            {
                AbilityDurationIndicator abilityDurationIndicator = _availableIndicators.Pop();
                return abilityDurationIndicator;
            }

            return Instantiate(_abilityDurationIndicatorPrefab, _indicatorsTransform);
        }
    }
}