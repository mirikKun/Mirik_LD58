using System;
using ImprovedTimers.Project.Scripts.Utils.Timers;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.GamePlay.Player.PlayerIndicators
{
    public class AbilityDurationIndicator:MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _fillImage;
        private CountdownTimer _timer;
        public event Action<AbilityDurationIndicator> TimerEnded;

        public void Init(Sprite sprite, CountdownTimer timer)
        {
            _timer = timer;
            _timer.OnTimerStop += OnTimerStoped;
            if(sprite)
            {
                _icon.sprite = sprite;
            }
        }

        private void OnTimerStoped()
        {
            _timer.OnTimerStop -= OnTimerStoped;
            TimerEnded?.Invoke(this);
        }

        public void UpdateProgress( )
        {
            _fillImage.fillAmount = _timer.Progress;
        }
    }
}