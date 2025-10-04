using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.GamePlay.HUD
{
    public class HealthUI:MonoBehaviour
    {
        [SerializeField] private Image _healthBar;

        protected virtual void Start()
        {
            _healthBar.fillAmount=1;
        }

        public void SetHealth(float healthPercentage)
        {
            _healthBar.fillAmount = Mathf.Clamp01(healthPercentage);
        }
       
    }
}