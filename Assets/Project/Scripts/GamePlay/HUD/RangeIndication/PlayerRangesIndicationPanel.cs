using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.GamePlay.Common.Physic.Raycast;
using Project.Scripts.GamePlay.Player.Indication;
using UnityEngine;

namespace Project.Scripts.GamePlay.HUD.RangeIndication
{
    public class PlayerRangesIndicationPanel:MonoBehaviour
    {
        [SerializeField]
        private Transform _indicatorsTransform;
        [SerializeField] private RangeIndicator _rangeIndicatorPrefab;
        [SerializeField] private AbilitiesIndicationController _abilitiesIndication;
        [SerializeField] private RangeIndicationData[] _rangeIndicatorsData;

        
        private List<RangeIndicator> _currentIndicators = new List<RangeIndicator>();
        private Stack<RangeIndicator> _availableIndicators = new Stack<RangeIndicator>();

        private void Start()
        {
            _abilitiesIndication.RangeIndication.AbilityWithRangeEquipped+=OnAbilityWithRangeEquipped;
            _abilitiesIndication.RangeIndication.AbilityWithRangeUnequipped+=OnAbilityWithRangeUnequipped;
        }

        private void OnDestroy()
        {
            
            _abilitiesIndication.RangeIndication.AbilityWithRangeEquipped-=OnAbilityWithRangeEquipped;
            _abilitiesIndication.RangeIndication.AbilityWithRangeUnequipped-=OnAbilityWithRangeUnequipped;
        }

        private void OnAbilityWithRangeEquipped(RangeIndicationType type, RaycastSensor raycastSensor)
        {
            var indicator = GetNewIndicator();
            indicator.gameObject.SetActive(true);

            _currentIndicators.Add(indicator);
            indicator.Init(GetIndicationData(type), raycastSensor);
        }

        private void Update()
        {
            foreach (var indicator in _currentIndicators)
            {
                indicator.Tick();
            }

            
        }

        private void OnAbilityWithRangeUnequipped(RangeIndicationType type)
        {
            var indicator = GetCurrentIndicator(type);
            _currentIndicators.Remove(indicator);
            _availableIndicators.Push(indicator);
            indicator.gameObject.SetActive(false);

        }
        private RangeIndicationData GetIndicationData(RangeIndicationType type)
        {
            foreach (var data in _rangeIndicatorsData)
            {
                if (data.Type == type)
                {
                    return data;
                }
            }

            return null;
        }
        private RangeIndicator GetCurrentIndicator(RangeIndicationType type)
        {
            return _currentIndicators.FirstOrDefault(x => x.Type == type);
        }
        private RangeIndicator GetNewIndicator()
        {
            if (_availableIndicators.Count > 0)
            {
                RangeIndicator abilityDurationIndicator = _availableIndicators.Pop();
                return abilityDurationIndicator;
            }

            return Instantiate(_rangeIndicatorPrefab, _indicatorsTransform);
        }
    }

    [Serializable]
    public class RangeIndicationData
    {
        public RangeIndicationType Type;
        public Sprite Icon;
        public Vector2 Size = new Vector2(100,100);
    }
}