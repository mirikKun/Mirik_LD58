using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.GamePlay.Player.PlayerEffects
{
    public class EnemyIndicationEffectUI : MonoBehaviour
    {
        [SerializeField] private Image _indicatorPrefab;
        [SerializeField] private Transform _indicatorsParent;
        [SerializeField] private float _indicatorRadius = 100f;
        [SerializeField] private Color _normalColor = Color.white;
        [SerializeField] private Color _attackingColor = Color.red;
        [SerializeField] private float _minAngleToShow = 60f;
        [SerializeField] private float _angleFadeRange = 15f;
        [SerializeField] private float _maxDistance = 50f;
        [SerializeField] private int _initialPoolSize = 10;

        private List<Image> _indicatorPool = new List<Image>();
        private int _activeCount = 0;

        private void Awake()
        {
            InitializePool();
        }

        private void InitializePool()
        {
            for (int i = 0; i < _initialPoolSize; i++)
            {
                CreatePoolItem();
            }
        }

        private Image CreatePoolItem()
        {
            Image indicator = Instantiate(_indicatorPrefab, _indicatorsParent);
            indicator.gameObject.SetActive(false);
            _indicatorPool.Add(indicator);
            return indicator;
        }

        public void UpdateEnemyIndicators(List<EnemyIndication.Indicator> indicators)
        {
            int requiredCount = indicators.Count;

            while (_indicatorPool.Count < requiredCount)
            {
                CreatePoolItem();
            }

            for (int i = 0; i < _indicatorPool.Count; i++)
            {
                bool isNeeded = i < requiredCount;
                if (_indicatorPool[i].gameObject.activeSelf != isNeeded)
                {
                    _indicatorPool[i].gameObject.SetActive(isNeeded);
                }
            }

            for (int i = 0; i < requiredCount; i++)
            {
                UpdateIndicator(_indicatorPool[i], indicators[i]);
            }

            _activeCount = requiredCount;
        }

        private void UpdateIndicator(Image indicator, EnemyIndication.Indicator data)
        {
            float normalizedAngle = NormalizeAngle(data.Angle);
            float absAngle = Mathf.Abs(normalizedAngle);
            Vector2 position = CalculatePositionOnCircle(normalizedAngle);
            indicator.rectTransform.anchoredPosition = position;
            indicator.rectTransform.rotation = Quaternion.Euler(0, 0, -normalizedAngle);

            Color indicatorColor = data.IsAttacking ? _attackingColor : _normalColor;

            float distanceScale = Mathf.Lerp(1.0f, 0.4f, data.Distance / _maxDistance);
            float distanceAlpha = Mathf.Lerp(1.0f, 0.2f, data.Distance / _maxDistance);

            float angleAlpha = 1f;
            if (absAngle < _minAngleToShow - _angleFadeRange)
            {
                angleAlpha = 0f;
            }
            else if (absAngle < _minAngleToShow)
            {
                angleAlpha = 1 - (_minAngleToShow - absAngle) / _angleFadeRange;
            }

            angleAlpha = Mathf.Clamp01(angleAlpha);

            indicatorColor.a = distanceAlpha * angleAlpha;
            indicator.color = indicatorColor;

            indicator.rectTransform.localScale = new Vector3(distanceScale, distanceScale, 1);

            indicator.gameObject.SetActive(angleAlpha > 0);
        }

        private float NormalizeAngle(float angle)
        {
            return angle;
        }

        private Vector2 CalculatePositionOnCircle(float angle)
        {
            float radians = angle * Mathf.Deg2Rad;
            float x = Mathf.Sin(radians) * _indicatorRadius;
            float y = Mathf.Cos(radians) * _indicatorRadius;
            return new Vector2(x, y);
        }

        private void OnDestroy()
        {
            ClearPool();
        }

        private void ClearPool()
        {
            foreach (var indicator in _indicatorPool)
            {
                if (indicator != null)
                {
                    Destroy(indicator.gameObject);
                }
            }

            _indicatorPool.Clear();
            _activeCount = 0;
        }
    }
}